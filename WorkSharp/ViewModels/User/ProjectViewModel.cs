using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AutoMapper.Configuration.Annotations;
using MoreLinq.Extensions;

namespace WorkSharp.ViewModels.User
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Info { get; set; }
        public UserViewModel Creator { get; set; }
        public IReadOnlyCollection<UserViewModel> Admins { get; set; }
        public IReadOnlyCollection<TaskBoardViewModel> TaskBoardViewModels { get; set; } 
        public IReadOnlyCollection<TeamViewModel> TeamViewModels { get; set; } = new List<TeamViewModel>();
        [Ignore]
        [NotMapped]
        public IReadOnlyCollection<UserViewModel> Members => TeamViewModels.SelectMany(team => team.Members).DistinctBy(m=>m.Id).ToList().AsReadOnly();
    }
}
