using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
