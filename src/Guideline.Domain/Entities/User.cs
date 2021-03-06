﻿using System;

namespace Guideline.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTimeOffset Created { get; set; }

        public User()
        {
            if (Id == new Guid())
                Id = Guid.NewGuid();

            Created = DateTimeOffset.Now;
        }
    }
}
