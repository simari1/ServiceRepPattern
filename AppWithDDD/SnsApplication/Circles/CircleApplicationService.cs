using AppWithDDD.Infrastructure.Circles.Repositories;
using AppWithDDD.SnsApplication.Circles.Create;
using AppWithDDD.SnsApplication.Circles.GetRecommend;
using AppWithDDD.SnsApplication.Circles.Invite;
using AppWithDDD.SnsApplication.Circles.Join;
using AppWithDDD.SnsDomain.Models.CircleInvitation;
using AppWithDDD.SnsDomain.Models.Circles;
using AppWithDDD.SnsDomain.Models.Circles.Exceptioins;
using AppWithDDD.SnsDomain.Models.Circles.Factories;
using AppWithDDD.SnsDomain.Models.Circles.Services;
using AppWithDDD.SnsDomain.Models.Circles.Specification;
using AppWithDDD.SnsDomain.Models.Users;
using AppWithDDD.SnsDomain.Models.Users.Exceptions;
using System.Transactions;

namespace AppWithDDD.SnsApplication.Circles
{
    /// <summary>
    /// CircleのDomain Objectを利用したユースケース、シナリオを実施する
    /// </summary>
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;
        private readonly ICircleInvitationRepository circleInvitationRepository;
        private readonly DateTime now;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            ICircleInvitationRepository circleInvitationRepository,
            CircleService circleService,
            IUserRepository userRepository,
            DateTime now)
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.circleInvitationRepository = circleInvitationRepository;
            this.circleService = circleService;
            this.userRepository = userRepository;
            this.now = now;
        }

        public void Create(CircleCreateCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var ownerId = new UserId(command.UserId);
                var owner = userRepository.Find(ownerId);
                if (owner == null)
                {
                    throw new UserNotFoundException(ownerId, "サークルのオーナーとなるユーザが見つかりませんでした。");
                }

                var name = new CircleName(command.Name);
                var circle = circleFactory.Create(name, owner);
                if (circleService.Exists(circle))
                {
                    throw new CanNotRegisterCircleException(circle, "サークルは既に存在しています。");
                }

                circleRepository.Save(circle);
                transaction.Complete();
            }
        }

        public void Join(CircleJoinCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var circleId = new CircleId(command.CircleId);
                var circle = circleRepository.Find(circleId);

                //Specificationを利用する
                var circleFullSpecification = new CircleFullSpecification(userRepository);
                if (circleFullSpecification.IsSatisfiedBy(circle))
                {
                    throw new CircleFullException(circleId);
                }

                var memberId = new UserId(command.UserId);
                var member = userRepository.Find(memberId);
                if (member == null)
                {
                    throw new UserNotFoundException(memberId, "ユーザが見つかりませんでした。");
                }

                //BAD
                //// サークルのオーナーを含めて３０名か確認
                //if (circle.Members.Count >= 29)
                //{
                //    throw new CircleFullException(id);
                //}
                //// メンバーを追加する
                //circle.Members.Add(member);

                //GOOD
                circle.Join(member);

                circleRepository.Save(circle);
                transaction.Complete();
            }
        }

        public void Invite(CircleInviteCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var fromUserId = new UserId(command.FromUserId);
                var fromUser = userRepository.Find(fromUserId);
                if (fromUser == null)
                {
                    throw new UserNotFoundException(fromUserId, "招待元ユーザが見つかりませんでした。");
                }

                var invitedUserId = new UserId(command.InvitedUserId);
                var invitedUser = userRepository.Find(invitedUserId);
                if (invitedUser == null)
                {
                    throw new UserNotFoundException(invitedUserId, "招待先ユーザが見つかりませんでした。");
                }

                var circleId = new CircleId(command.CircleId);
                var circle = circleRepository.Find(circleId);
                if (circle == null)
                {
                    throw new CircleNotFoundException(circleId, "サークルが見つかりませんでした。");
                }

                // サークルのオーナーを含めて３０名か確認
                if (circle.IsFull())
                {
                    throw new CircleFullException(circleId);
                }

                var circleInvitation = new CircleInvitation(circle, fromUser, invitedUser);
                circleInvitationRepository.Save(circleInvitation);
                transaction.Complete();
            }
        }

        public CircleGetRecommendResult GetRecommend(CircleGetRecommendRequest request)
        {
            var recommendCircleSpec = new CircleRecommendSpecification(now);
            var circles = circleRepository.FindAll();
            var recommendCircles = circles
                .Where(recommendCircleSpec.IsSatisfiedBy)
                .Take(10)
                .ToList();
            return new CircleGetRecommendResult(recommendCircles);
        }
    }

}
