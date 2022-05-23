using System;
using AppWithDDD.SnsDomain.Models.Users;

namespace AppWithDDD.SnsDomain.Models.Users.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(UserId id)
        {
            Id = id.Value;
        }

        public UserNotFoundException(UserId id, string message) : base(message)
        {
            Id = id.Value;
        }

        public string Id { get; }
    }
}
