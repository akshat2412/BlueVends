using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.Mappers.Home;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Product;
using System;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _AnalyticsMapper;
        public HomeController()
        {
            _AnalyticsMapper = AutoMappers.AnalyticsMapper;
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
                analyticsViewModel = _AnalyticsMapper.Map<AnalyticsDTO, AnalyticsViewModel>(analyticsDTO);
                return View(analyticsViewModel);
            }
            catch (Exception)
            {
                return View("Internal Error");
            }
        }
    }
}