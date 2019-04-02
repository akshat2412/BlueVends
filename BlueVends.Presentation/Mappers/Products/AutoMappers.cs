using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.Presentation.Mappers.Products
{
    public class AutoMappers
    {
        public static IMapper CategoryProductVMMapper
        {
            get
            {
                var CategoryProductConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, ProductViewModel>();
                    cfg.CreateMap<VariantDTO, VariantViewModel>();
                    cfg.CreateMap<CategoryProductsDTO, CategoryProductsViewModel>();
                });
                return new Mapper(CategoryProductConfig);
            }
        }

        public static IMapper ProductProductVMMapper
        {
            get
            {
                var ProductProductConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, ProductViewModel>();
                    cfg.CreateMap<VariantDTO, VariantViewModel>();
                });
                return new Mapper(ProductProductConfig);
            }
        }

        public static IMapper ProductsSearchResultVMMapper
        {
            get
            {
                var ProductSearchResultConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, ProductViewModel>();
                    cfg.CreateMap<VariantDTO, VariantViewModel>();
                    cfg.CreateMap<ProductsSearchResultDTO, ProductsSearchResultViewModel>();
                });
                return new Mapper(ProductSearchResultConfig);
            }
        }
    }
}