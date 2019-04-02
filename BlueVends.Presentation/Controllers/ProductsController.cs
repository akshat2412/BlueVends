using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Presentation.Mappers.Products;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Product;
using System;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        ProductBusinessContext productBusinessContext = new ProductBusinessContext();
        IMapper _CategoryProductVMMapper;
        IMapper _ProductProductVMMapper;
        IMapper _ProductsSearchResultVMMapper;
        // GET: Products

        public ProductsController()
        {
            _CategoryProductVMMapper = AutoMappers.CategoryProductVMMapper;
            _ProductProductVMMapper = AutoMappers.ProductProductVMMapper;
            _ProductsSearchResultVMMapper = AutoMappers.ProductsSearchResultVMMapper;
        }

        public ActionResult CategoryProducts(string CategoryName)
        {
            
            CategoryProductsViewModel viewModel = new CategoryProductsViewModel();
            CategoryProductsDTO categoryProductsDTO = new CategoryProductsDTO();
            try
            {
                categoryProductsDTO = productBusinessContext.GetCategoryProducts(CategoryName);
            }
            catch(CategoryDoesNotExistsException ex)
            {
                return View("Error");
            }
            catch(Exception ex)
            {
                return View("Internal Error");
            }
            viewModel = _CategoryProductVMMapper.Map<CategoryProductsDTO, CategoryProductsViewModel>(categoryProductsDTO);
            if(Session["UserID"] != null)
            {
                viewModel.IsLoggedIn = true;
            }
            return View(viewModel);
        }

        [UserAuthenticationFilter]
        public ActionResult ProductDetail(Guid ProductID)
        {
            ProductViewModel viewModel = new ProductViewModel();
            ProductDTO productDTO = new ProductDTO();
            try
            {
                productDTO = productBusinessContext.GetProduct(ProductID);
            }
            catch (CategoryDoesNotExistsException ex)
            {
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Internal Error");
            }
            viewModel = _ProductProductVMMapper.Map<ProductDTO, ProductViewModel>(productDTO);
            viewModel.IsLoggedIn = true;
            return View(viewModel);

        }

        
        public ActionResult SearchProducts(string SearchString)
        {
            if (Session["UserID"] != null)
            {
                ViewBag.IsLoggedIn = "True";
            }

            if (String.IsNullOrEmpty(SearchString))
            {
                return View("Search String Empty");//TODO
            }

            ProductsSearchResultDTO newProductsSearchResultDTO = new ProductsSearchResultDTO();
            ProductsSearchResultViewModel viewModel = new ProductsSearchResultViewModel();
            try
            {
                newProductsSearchResultDTO = productBusinessContext.GetProductsWithString(SearchString);
                viewModel = _ProductsSearchResultVMMapper.Map<ProductsSearchResultDTO, ProductsSearchResultViewModel>(newProductsSearchResultDTO);
                ViewBag.SearchString = SearchString;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                return View("InternalError");
            }
        }
        
    }
}
