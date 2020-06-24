using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;
using WorkSharp.Domain.Models;

namespace WorkSharp.DAL.Mappers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Models.Profile, DbProfile>().ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.User.Id)).ReverseMap();
            CreateMap<Project, DbProject>().ForMember(dest => dest.CreatorId, opt => opt.MapFrom(source => source.Creator.Id)).ReverseMap();
            CreateMap<Task, DbTask>().ForMember(dest => dest.TaskBoardId, opt => opt.MapFrom(source => source.TaskBoard.Id)).ForMember(dest => dest.ExecutorId, opt => opt.MapFrom(source => source.Executor.Id)).ReverseMap();
            CreateMap<TaskBoard, DbTaskBoard>().ForMember(dest => dest.ProjectId, opt => opt.MapFrom(source => source.Project.Id)).ReverseMap();
            CreateMap<Team, DbTeam>().ForMember(dest => dest.ProjectId, opt => opt.MapFrom(source => source.Project.Id)).ReverseMap();
            CreateMap<User, DbUser>().ReverseMap();
        }
        //MapperConfiguration ProfileConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Models.Profile, DbProfile>().ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.User.Id)).ReverseMap());
        //MapperConfiguration ProjectConfig = new MapperConfiguration(cfg => cfg.CreateMap<Project, DbProject>().ForMember(dest => dest.CreatorId, opt => opt.MapFrom(source => source.Creator.Id)).ReverseMap());
        //MapperConfiguration TaskConfig = new MapperConfiguration(cfg => cfg.CreateMap<Task, DbTask>().ForMember(dest => dest.TaskBoardId, opt => opt.MapFrom(source => source.TaskBoard.Id)).ForMember(dest => dest.ExecutorId, opt => opt.MapFrom(source => source.Executor.Id)).ReverseMap());
        //MapperConfiguration TaskBoardConfig = new MapperConfiguration(cfg => cfg.CreateMap<TaskBoard, DbTaskBoard>().ForMember(dest => dest.ProjectId, opt => opt.MapFrom(source => source.Project.Id)).ReverseMap());
        //MapperConfiguration TeamConfig = new MapperConfiguration(cfg => cfg.CreateMap<Team, DbTeam>().ForMember(dest => dest.ProjectId, opt => opt.MapFrom(source => source.Project.Id)).ReverseMap());
        //MapperConfiguration UserConfig = new MapperConfiguration(cfg => cfg.CreateMap<User, DbUser>().ReverseMap());

    }
}
