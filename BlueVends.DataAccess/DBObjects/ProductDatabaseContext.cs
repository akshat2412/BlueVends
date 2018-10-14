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
using BlueVends.Shared.DTO.Cart;

namespace BlueVends.DataAccess.DBObjects
{
    public class ProductDatabaseContext
    {
        IMapper CategoryProductMapper;
        IMapper ProductMapper;
        IMapper ProductSearchMapper;
        IMapper VariantMapper;
        IMapper AnalyticsMapper;
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
                cfg.CreateMap<Product, ProductDTO>();
            });

            var AnalyticsConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Variant, VariantDTO>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<Category, CategoryProductsDTO>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Product));
            });


            CategoryProductMapper = new Mapper(productCollectionDTOConfig);
            ProductMapper = new Mapper(productDTOConfig);
            ProductSearchMapper = new Mapper(productsSearchDTOConfig);
            VariantMapper = new Mapper(VariantDTOConfig);
            AnalyticsMapper = new Mapper(AnalyticsConfig);
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

        public void UpdateInventory(CartsDTO cartsDTO)
        {
            foreach(var cartItem in cartsDTO.CartItems)
            {
                dbContext.Product.SingleOrDefault(p => p.ID == cartItem.Variant.Product.ID).Inventory -= cartItem.OrderQuantity;
                dbContext.Product.SingleOrDefault(p => p.ID == cartItem.Variant.Product.ID).QuantitySold += cartItem.OrderQuantity;
                dbContext.Category.SingleOrDefault(c => c.ID == cartItem.Variant.Product.CategoryID).ProductsSold += cartItem.OrderQuantity;
                dbContext.SaveChanges();
            }
            return;
        }

        public void Changes(Guid UserID)
        {
            //var variants = dbContext.Product.Where(p => p.Category.Name == "Watches").ToList();
            //var Orders = dbContext.Order.Where(o => o.UserID == UserID);
            //foreach(var order in Orders.ToList())
            //{
            //    dbContext.ProductOrderJunction.RemoveRange(dbContext.ProductOrderJunction.Where(poj => poj.OrderID == order.ID));
            //    dbContext.SaveChanges();
            //}

            //dbContext.Order.RemoveRange(dbContext.Order.Where(o => o.UserID == UserID));
            //dbContext.SaveChanges();

            //var products = dbContext.Product.Where(p => p.Inventory < 10);
            //foreach(var prod in products.ToList())
            //{
            //    dbContext.Product.Where(p => p.ID == prod.ID).SingleOrDefault().Inventory = 10;
            //    dbContext.Category.Where(c => c.ID == prod.CategoryID).SingleOrDefault().ProductsSold = 0;
            //    dbContext.SaveChanges();
            //}
            //foreach(var prod in variants)
            //{
            //    Variant variant = new Variant { ID = Guid.NewGuid(), Name = "Default", ProductID = prod.ID };
            //    dbContext.Variant.Add(variant);
            //    dbContext.SaveChanges();
            //}
           // var products = dbContext.Product.
            return;
        }

        public AnalyticsDTO GetTopProductsByCat()
        {
            var Categories = dbContext.Category.Include(c => c.Product).OrderByDescending(c => c.ProductsSold).ToList();
            foreach(var category in Categories)
            {
                category.Product = category.Product.OrderByDescending(p => p.QuantitySold).ToList();
            }
            AnalyticsDTO analyticsDTO = new AnalyticsDTO();
            analyticsDTO.Categories = AnalyticsMapper.Map<IEnumerable<Category>, IEnumerable<CategoryProductsDTO>>(Categories);
            return analyticsDTO;
        }
    }
}
