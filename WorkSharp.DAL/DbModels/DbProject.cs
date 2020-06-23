using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.DbModels
{
    public class DbProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Guid Creator { get; set; }
    }
}
