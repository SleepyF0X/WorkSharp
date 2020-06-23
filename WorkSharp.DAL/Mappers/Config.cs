using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.Mappers.MapperConfig
{
    public class Config
    {
        MapperConfiguration ProfileConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());
        //MapperConfiguration ProjectConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());
        //MapperConfiguration TaskConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());
        //MapperConfiguration TaskBoardConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());
        //MapperConfiguration TeamConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());
        //MapperConfiguration UserConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, DbProfile>());

    }
}
