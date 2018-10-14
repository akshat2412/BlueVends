using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
namespace BlueVends.Presentation.Controllers
{
    public class HomeController : Controller
    {
        IMapper AnalyticsMapper;
        public HomeController()
        {
            var AnalyticsConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<AnalyticsDTO, AnalyticsViewModel>();
                cfg.CreateMap<CategoryProductsDTO, CategoryProductsViewModel>();
                cfg.CreateMap<ProductDTO, ProductViewModel>();
            });


            AnalyticsMapper = new Mapper(AnalyticsConfig);
        }
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.IsLoggedIn = "True";
            }

            
            ProductBusinessContext productBusinessContext = new ProductBusinessContext();
            //productBusinessContext.Changes(new Guid(Session["UserID"].ToString()));
            AnalyticsViewModel analyticsViewModel = new AnalyticsViewModel();
            AnalyticsDTO analyticsDTO = new AnalyticsDTO();
            try
            {
                analyticsDTO = productBusinessContext.GetTopProductsByCat();
                analyticsViewModel = AnalyticsMapper.Map<AnalyticsDTO, AnalyticsViewModel>(analyticsDTO);
                return View(analyticsViewModel);
            }
            catch (Exception)
            {
                return View("Internal Error");
            }
        }
    }
}