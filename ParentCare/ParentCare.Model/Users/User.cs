using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentCare.Model.Users
{
    public class User : IdentityUser
    {
        public User(string username, string name) : base(username)
        {
            Name = name;
            CreateDateUtc = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public Role Role { get; set; }
    }
}
