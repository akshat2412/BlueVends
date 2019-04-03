using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.DataAccess.Mappers.CartMappers
{
    class AutoMappers
    {
        public static IMapper CartItemsMapper
        {
            get
            {
                var CartsConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Variant, VariantDTO>();
                    cfg.CreateMap<Cart, CartVariantDTO>();
                    cfg.CreateMap<Product, ProductDTO>();
                });
                return new Mapper(CartsConfig);
            }
        }
    }
}
