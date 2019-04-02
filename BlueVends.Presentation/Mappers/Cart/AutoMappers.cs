using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.Presentation.Mappers.Cart
{
    public class AutoMappers
    {
        public static IMapper CartMapper
        {
            get
            {
                var CartConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CartViewModel, CartDTO>();
                });
                return new Mapper(CartConfig);
            }
        }

        public static IMapper CartsMapper
        {
            get
            {
                var CartsConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, ProductViewModel>();
                    cfg.CreateMap<VariantDTO, VariantViewModel>();
                    cfg.CreateMap<CartVariantDTO, CartVariantViewModel>();
                });
                return new Mapper(CartsConfig);
            }
        }
    }
}