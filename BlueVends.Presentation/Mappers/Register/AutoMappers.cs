using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;

namespace BlueVends.Presentation.Mappers.Register
{
    public class AutoMappers
    {
        public static IMapper RegistrationVMMapper
        {
            get
            {
                var RegistrationVMConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserRegistrationViewModel, UserDTO>();
                });

                return new Mapper(RegistrationVMConfig);
            }
        }
    }
}