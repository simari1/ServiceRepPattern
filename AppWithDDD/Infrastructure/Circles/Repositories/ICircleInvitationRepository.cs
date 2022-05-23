using AppWithDDD.SnsDomain.Models.CircleInvitation;

namespace AppWithDDD.Infrastructure.Circles.Repositories
{
    public interface ICircleInvitationRepository
    {
        void Save(CircleInvitation circleInvitation);
    }
}
