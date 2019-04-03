using AutoMapper;
using BlueVends.DataAccess.Exceptions;
using BlueVends.DataAccess.Mappers.ProductMappers;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Variant;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlueVends.DataAccess.DBObjects
{
    public class ProductDatabaseContext
    {
        IMapper _CategoryProductMapper;
        IMapper _ProductMapper;
        IMapper _ProductSearchMapper;
        IMapper _VariantMapper;
        IMapper _AnalyticsMapper;
        BlueVendsDBEntities dbContext;

        public ProductDatabaseContext()
        {
            dbContext = new BlueVendsDBEntities();

            _CategoryProductMapper = AutoMappers.CategoryProductMapper;
            _ProductMapper = AutoMappers.ProductMapper;
            _ProductSearchMapper = AutoMappers.ProductSearchMapper;
            _VariantMapper = AutoMappers.VariantMapper;
            _AnalyticsMapper = AutoMappers.AnalyticsMapper;
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
            newcategoryProductsDTO.Products = _CategoryProductMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
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
            newproductDTO = _ProductMapper.Map<Product, ProductDTO>(product);
            newproductDTO.Variants = _VariantMapper.Map<IEnumerable<Variant>, IEnumerable<VariantDTO>>(product.Variant);
            return newproductDTO;
        }

        public ProductsSearchResultDTO GetProductsWithString(string SearchString)
        {
            IEnumerable<Product> searchResults = dbContext.Product.Where(p => p.Name.Contains(SearchString)).Include(p => p.Category);
            ProductsSearchResultDTO newProductsSearchResultDTO = new ProductsSearchResultDTO();
            newProductsSearchResultDTO.Products = _ProductSearchMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(searchResults);
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
            analyticsDTO.Categories = _AnalyticsMapper.Map<IEnumerable<Category>, IEnumerable<CategoryProductsDTO>>(Categories);
            return analyticsDTO;
        }
    }
}
