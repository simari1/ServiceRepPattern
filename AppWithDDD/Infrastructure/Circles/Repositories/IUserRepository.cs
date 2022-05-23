using AppWithDDD.SnsDomain.Models.Users;
using System.Collections.Generic;

namespace AppWithDDD.Infrastructure.Circles.Repositories
{
    public interface IUserRepository
    {
        User Find(UserId id);
        User Find(UserName name);
        List<User> FindAll();
        void Save(User user);
        void Delete(User user);
        List<User> Find(List<UserId> members);
    }
}
