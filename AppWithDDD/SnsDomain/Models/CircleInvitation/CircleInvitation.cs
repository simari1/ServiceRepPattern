using AppWithDDD.SnsDomain.Models.Circles;
using AppWithDDD.SnsDomain.Models.Users;

namespace AppWithDDD.SnsDomain.Models.CircleInvitation
{
    public class CircleInvitation
    {
        public CircleInvitation(Circle circle, User fromUser, User invitedUser)
        {
            Circle = circle;
            FromUser = fromUser;
            InvitedUser = invitedUser;
        }

        public Circle Circle { get; }
        public User FromUser { get; }
        public User InvitedUser { get; }
    }
}
