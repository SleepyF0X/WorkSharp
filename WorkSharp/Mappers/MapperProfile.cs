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
            CreateMap<DbProject, ProjectViewModel>().ForMember(dest=>dest.TaskBoardViewModels, opt => opt.MapFrom(source => source.TaskBoards)).ReverseMap();
            CreateMap<DbUser, RegisterViewModel>().ReverseMap();
            CreateMap<DbTaskBoard, TaskBoardViewModel>().ForMember(dest => dest.TaskViewModels, opt => opt.MapFrom(source => source.Tasks)).ReverseMap();
            CreateMap<DbTask, TaskViewModel>().ReverseMap();
        }
    }
}
