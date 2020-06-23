using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbProfile
    {
        public Guid Id { get; set; }
        public DbUser User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
