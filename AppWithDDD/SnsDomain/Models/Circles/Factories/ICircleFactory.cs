using AppWithDDD.SnsDomain.Models.Circles;
using AppWithDDD.SnsDomain.Models.Users;

namespace AppWithDDD.SnsDomain.Models.Circles.Factories
{
    public interface ICircleFactory
    {
        Circle Create(CircleName name, User owner);
    }
}
