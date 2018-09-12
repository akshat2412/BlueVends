using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlueVends.Shared.DTO.Product;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        ProductBusinessContext productBusinessContext = new ProductBusinessContext();
        IMapper CategoryProductVMMapper;
        IMapper ProductProductVMMapper;
        IMapper ProductsSearchResultVMMapper;
        // GET: Products

        public ProductsController()
        {
            var productCollectionDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<VariantDTO, VariantViewModel>();
                cfg.CreateMap<CategoryProductsDTO, CategoryProductsViewModel>();
            });

            var productDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<VariantDTO, VariantViewModel>();
            });

            var productSearchResultDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<VariantDTO, VariantViewModel>();
                cfg.CreateMap<ProductsSearchResultDTO, ProductsSearchResultViewModel>();
            });

            CategoryProductVMMapper = new Mapper(productCollectionDTOConfig);
            ProductProductVMMapper = new Mapper(productDTOConfig);
            ProductsSearchResultVMMapper = new Mapper(productSearchResultDTOConfig);
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
            viewModel = CategoryProductVMMapper.Map<CategoryProductsDTO, CategoryProductsViewModel>(categoryProductsDTO);
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
            ViewBag.LoggedIn = "true";
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
            viewModel = ProductProductVMMapper.Map<ProductDTO, ProductViewModel>(productDTO);
            return View(viewModel);

        }

        
        public ActionResult SearchProducts(string SearchString)
        {
            if (Session["UserID"] != null)
            {
                ViewBag.LoggedIn = "True";
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
                viewModel = ProductsSearchResultVMMapper.Map<ProductsSearchResultDTO, ProductsSearchResultViewModel>(newProductsSearchResultDTO);
                ViewBag.SearchString = SearchString;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                return View("InternalError");
            }
        }
        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
