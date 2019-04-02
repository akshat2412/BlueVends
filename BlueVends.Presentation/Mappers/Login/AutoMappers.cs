using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;

namespace BlueVends.Presentation.Mappers.Login
{
    public class AutoMappers
    {
        public static IMapper LoginMapper
        {
            get
            {
                var loginConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<LoginViewModel, UserLoginDTO>();
                });

                return new Mapper(loginConfig);
            }
        }
    }
}