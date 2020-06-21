using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.Relations
{
    public class ProfileTeam
    {
        public Guid MemberId { get; set; }
        public Guid TeamId { get; set; }
    }
}
