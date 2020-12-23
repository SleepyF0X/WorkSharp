using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WorkSharp.DAL.DbModels;
using WorkSharp.ViewModels;
using WorkSharp.ViewModels.Authentication;
using WorkSharp.ViewModels.User;

namespace WorkSharp.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DbProject, ProjectViewModel>()
                .ForMember(
                    dest => dest.TaskBoardViewModels,
                    opt => opt.MapFrom(
                        source => source.TaskBoards))
                .ForMember(
                    dest => dest.TeamViewModels,
                    opt => opt.MapFrom(
                        source => source.Teams))
                .ForMember(
                    dest=>dest.Creator, 
                    opt => opt.MapFrom(
                        source=>source.Creator))
                .ReverseMap();
            CreateMap<DbUser, RegisterViewModel>()
                .ReverseMap();
            CreateMap<DbTaskBoard, TaskBoardViewModel>()
                .ForMember(
                dest => dest.TaskViewModels,
                opt => opt.MapFrom(
                    source => source.Tasks))
                .ReverseMap();
            CreateMap<DbTask, TaskViewModel>()
                .ReverseMap();
            CreateMap<DbTeam, TeamViewModel>()
                .ForMember(
                    dest => dest.Members,
                    opt => opt.MapFrom(
                        source => source.Members))
                .ReverseMap();
            CreateMap<DbUser, UserViewModel>()
                .ForMember(
                    dest => dest.Teams, 
                    opt => opt.MapFrom(
                        source => source.Teams))
                .ReverseMap();
        }
    }
}
