using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Product;

namespace BlueVends.Presentation.Mappers.Home
{
    public class AutoMappers
    {
        public static IMapper AnalyticsMapper
        {
            get
            {
                var AnalyticsConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AnalyticsDTO, AnalyticsViewModel>();
                    cfg.CreateMap<CategoryProductsDTO, CategoryProductsViewModel>();
                    cfg.CreateMap<ProductDTO, ProductViewModel>();
                });
                return new Mapper(AnalyticsConfig);
            }
        }
    }
}