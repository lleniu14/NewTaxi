using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Users;

namespace WebApi.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<RegisterModel, User>();
                cfg.CreateMap<UpdateModel, User>();
                cfg.CreateMap<Driver, DriverOrderDTO>();
            })
            .CreateMapper();
    }
}
