using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.DataAccess.Mappers.ProductMappers
{
    class AutoMappers
    {
        public static IMapper CategoryProductMapper
        {
            get
            {
                var CategoryProductConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Variant, VariantDTO>();
                    cfg.CreateMap<Product, ProductDTO>();
                });
                return new Mapper(CategoryProductConfig);
            }
        }

        public static IMapper ProductMapper
        {
            get
            {
                var ProductConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<Variant, VariantDTO>();
                });
                return new Mapper(ProductConfig);
            }
        }

        public static IMapper ProductSearchMapper
        {
            get
            {
                var ProductsSearchConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<Variant, VariantDTO>();
                });
                return new Mapper(ProductsSearchConfig);
            }
        }

        public static IMapper VariantMapper
        {
            get
            {
                var VariantConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Variant, VariantDTO>();
                    cfg.CreateMap<Product, ProductDTO>();
                });
                return new Mapper(VariantConfig);
            }
        }

        public static IMapper AnalyticsMapper
        {
            get
            {
                var AnalyticsConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Variant, VariantDTO>();
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<Category, CategoryProductsDTO>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Product));
                });
                return new Mapper(AnalyticsConfig);
            }
        }
    }
}
