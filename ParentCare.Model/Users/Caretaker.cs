using System;
using System.Collections.Generic;
using System.Text;

namespace ParentCare.Model.Users
{
    public class Caretaker
    {
        public Caretaker(string email, string phone, string name, int elderId)
        {
            Email = email;
            Phone = phone;
            Name = name;
            ElderId = elderId;
            CreateDateUtc = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int ElderId { get; set; }
        public User Elder { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }
}
