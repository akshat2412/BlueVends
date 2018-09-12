using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Product;
using System.Data.Entity;
using BlueVends.DataAccess.Exceptions;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.DataAccess.DBObjects
{
    public class ProductDatabaseContext
    {
        IMapper CategoryProductMapper;
        IMapper ProductMapper;
        IMapper ProductSearchMapper;
        IMapper VariantMapper;
        BlueVendsDBEntities dbContext;
        public ProductDatabaseContext()
        {
            dbContext = new BlueVendsDBEntities();
            var productCollectionDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Variant, VariantDTO>();
                cfg.CreateMap<Product, ProductDTO>();
            });

            var productDTOConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<Variant, VariantDTO>();
            });

            var productsSearchDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<Variant, VariantDTO>();
            });

            var VariantDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Variant, VariantDTO>();
            });
            CategoryProductMapper = new Mapper(productCollectionDTOConfig);
            ProductMapper = new Mapper(productDTOConfig);
            ProductSearchMapper = new Mapper(productsSearchDTOConfig);
            VariantMapper = new Mapper(VariantDTOConfig);
        }

        public bool CategoryExists(string CategoryName)
        {
            Category category = dbContext.Category.Where(c => c.Name == CategoryName).FirstOrDefault();
            if(category == null)
            {
                throw new NotFoundException();
            }
            return true;
        }
        public CategoryProductsDTO GetCategoryProducts(string CategoryName)
        {
            Category category = dbContext.Category.Include(c => c.Product).Where(c => c.Name == CategoryName).FirstOrDefault();
            IEnumerable<Product> products = category.Product;
            CategoryProductsDTO newcategoryProductsDTO = new CategoryProductsDTO();
            newcategoryProductsDTO.Products = CategoryProductMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            newcategoryProductsDTO.Name = category.Name;
            return newcategoryProductsDTO;
        }

        public string GetCategoryName(Guid categoryID)
        {
            Category category = dbContext.Category.Find(categoryID);
            string name = category.Name;
            return name;
        }

        public bool ProductExists(Guid ProductID)
        {
            Product product = dbContext.Product.Find(ProductID);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            return true;
        }

        public ProductDTO GetProduct(Guid ProductID)
        {
            Product product = dbContext.Product.Where(p => p.ID == ProductID).Include(p => p.Variant).FirstOrDefault();
            ProductDTO newproductDTO = new ProductDTO();
            newproductDTO = ProductMapper.Map<Product, ProductDTO>(product);
            newproductDTO.Variants = VariantMapper.Map<IEnumerable<Variant>, IEnumerable<VariantDTO>>(product.Variant);
            return newproductDTO;
        }

        public ProductsSearchResultDTO GetProductsWithString(string SearchString)
        {
            IEnumerable<Product> searchResults = dbContext.Product.Where(p => p.Name.Contains(SearchString)).Include(p => p.Category);
            ProductsSearchResultDTO newProductsSearchResultDTO = new ProductsSearchResultDTO();
            newProductsSearchResultDTO.Products = ProductSearchMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(searchResults);
            return newProductsSearchResultDTO;
        }
    }
}
