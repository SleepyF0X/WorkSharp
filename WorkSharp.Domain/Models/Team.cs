using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.Domain.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
        private List<Profile> _members;
        public IReadOnlyCollection<Profile> Members => _members.AsReadOnly();

    }
}
