using Kuvio.Kernel.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParentCare.Model.Users
{
    public partial class Role : ValueObject<Role>
    {
        private Role(string value, string text) { Value = value; Text = text; }

        public string Value { get; private set; }
        public string Text { get; private set; }

        public static Role Parent { get { return new Role("Parent", "Parent"); } }
        public static Role Caretaker { get { return new Role("Caretaker", "Caretaker"); } }

        public static List<Role> GetAll()
        {
            var list = new List<Role>();
            list.Add(Parent);
            list.Add(Caretaker);
            return list;
        }

        public static Role Get(string value)
        {
            var list = GetAll();
            return list.First(y => y.Value == value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
