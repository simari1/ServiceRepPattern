﻿using System;

namespace AppWithDDD.SnsDomain.Models.Users
{
    public class User
    {
        public User(UserId id, UserName name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
        }

        public UserId Id { get; }
        public UserName Name { get; private set; }
        public bool IsPremium { get; private set; }

        public void ChangeName(UserName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
        }
    }
}
