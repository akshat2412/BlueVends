using BlueVends.Business.Exceptions;
using BlueVends.DataAccess.DBObjects;
using BlueVends.DataAccess.Exceptions;
using BlueVends.Shared.DTO.Product;
using System;
using System.Linq;

namespace BlueVends.Business.BusinessObjects
{
    public class ProductBusinessContext
    {
        ProductDatabaseContext productDatabaseContext = new ProductDatabaseContext();


        public CategoryProductsDTO GetCategoryProducts(string CategoryName)
        {
            try
            {
                bool exists = productDatabaseContext.CategoryExists(CategoryName);
            }
            catch(NotFoundException ex)
            {
                throw new CategoryDoesNotExistsException();
            }
            CategoryProductsDTO newCategoryProductsDTO = productDatabaseContext.GetCategoryProducts(CategoryName);
            return newCategoryProductsDTO;
        }

        //public String GetCategoryName(Guid CategoryID)
        //{
        //    try
        //    {
        //        bool exists = productDatabaseContext.CategoryExists(CategoryID);
        //    }
        //    catch (CategoryNotFoundException ex)
        //    {
        //        throw new CategoryDoesNotExistsException();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unknown Error");
        //    }
        //    string name = productDatabaseContext.GetCategoryName(CategoryID);
        //    return name;
        //}

        public ProductDTO GetProduct(Guid ProductID)
        {
            try
            {
                bool exists = productDatabaseContext.ProductExists(ProductID);
            }
            catch (NotFoundException ex)
            {
                throw new ProductDoesNotExistsException();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown Error");
            }
            ProductDTO newProductDTO = productDatabaseContext.GetProduct(ProductID);
            return newProductDTO;
        }

        public ProductsSearchResultDTO GetProductsWithString(string SearchString)
        {
            ProductsSearchResultDTO newProductsSearchResultDTO = new ProductsSearchResultDTO();

            try
            {
                newProductsSearchResultDTO = productDatabaseContext.GetProductsWithString(SearchString);
                return newProductsSearchResultDTO;
            }
            catch(Exception ex)
            {
                throw new Exception("Unknown Error");
            }
        }

        public void Changes(Guid UserID)
        {
            productDatabaseContext.Changes(UserID);
        }

        public AnalyticsDTO GetTopProductsByCat()
        {
            AnalyticsDTO analyticsDTO = productDatabaseContext.GetTopProductsByCat();
            foreach(var category in analyticsDTO.Categories)
            {
                category.Products = category.Products.Take(4);
            }
            return analyticsDTO;
        }
    }
}
