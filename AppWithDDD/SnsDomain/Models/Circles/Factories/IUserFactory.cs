using AppWithDDD.SnsDomain.Models.Users;

namespace AppWithDDD.SnsDomain.Models.Circles.Factories
{
    public interface IUserFactory
    {
        User Create(UserName name);
    }

}
