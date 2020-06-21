using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class Profile
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
