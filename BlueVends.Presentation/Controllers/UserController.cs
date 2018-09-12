using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlueVends.Shared.DTO.Order;
using BlueVends.Shared.DTO.Shared;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class UserController : Controller
    {
        UserBusinessContext userBusinessContext = new UserBusinessContext();
        IMapper OrdersVMMapper;
        // GET: User

        public UserController()
        {
            var OrdersDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<OrderDTO, OrderViewModel>();
                cfg.CreateMap<OrderProductDTO, OrderProductViewModel>();
            });

            OrdersVMMapper = new Mapper(OrdersDTOConfig);
        }
        public ActionResult Orders()
        {
            ViewBag.LoggedIn = "true";
            OrdersViewModel ordersViewModel = new OrdersViewModel();
            try
            {
                UserOrdersDTO newuserOrdersDTO = userBusinessContext.GetOrders(new Guid(Session["UserID"].ToString()));
                ordersViewModel.Orders = OrdersVMMapper.Map<IEnumerable<UserOrderDTO>, IEnumerable<OrderViewModel>>(newuserOrdersDTO.Orders);
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
            return View("../Home/Index");
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
