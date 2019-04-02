using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Presentation.Mappers.User;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class UserController : Controller
    {
        UserBusinessContext userBusinessContext = new UserBusinessContext();
        IMapper _OrdersVMMapper;
        // GET: User

        public UserController()
        {
            _OrdersVMMapper = AutoMappers.OrdersVMMapper;
        }
        public ActionResult Orders()
        {
            ViewBag.LoggedIn = "true";
            OrdersViewModel ordersViewModel = new OrdersViewModel();
            try
            {
                UserOrdersDTO newuserOrdersDTO = userBusinessContext.GetOrders(new Guid(Session["UserID"].ToString()));
                ordersViewModel.Orders = _OrdersVMMapper.Map<IEnumerable<UserOrderDTO>, IEnumerable<OrderViewModel>>(newuserOrdersDTO.Orders);
                return View(ordersViewModel);
            }
            catch(Exception ex)
            {
                return View("InternalError");
            }
        }

        public ActionResult Order(Guid orderID)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            try
            {
                UserOrderDTO newuserOrderDTO = userBusinessContext.GetOrder(orderID);
                return Json(newuserOrderDTO, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CheckAdmin()
        {
            try
            {
                if(userBusinessContext.CheckAdmin(new Guid(Session["UserID"].ToString())))
                {
                    return View("Admin");
                }
                else
                {
                    return View("NotAdmin");
                }
            }
            catch (Exception)
            {
                return View("InternalError");
            }
        }
    }
}
