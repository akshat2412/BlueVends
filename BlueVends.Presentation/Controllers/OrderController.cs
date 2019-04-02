using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Presentation.Mappers.Order;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class OrderController : Controller
    {
        OrderBusinessContext orderBusinessContext;
        IMapper _AddressMapper;
        IMapper _OrdersMapper;
        public OrderController()
        {
            orderBusinessContext = new OrderBusinessContext();
            _AddressMapper = AutoMappers.AddressMapper;
            _OrdersMapper = AutoMappers.OrdersMapper;
        }

        public OrdersDTO OrdersDTO { get; private set; }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout([Bind(Include = ("AddressLine1, AddressLine2, PIN"))] AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddressDTO addressDTO = _AddressMapper.Map<AddressDTO>(addressViewModel);
                    orderBusinessContext.PlaceOrder(new Guid(Session["UserID"].ToString()), addressDTO);
                    return View("Success");
                }
                catch(Exception ex)
                {
                    return View("Internal Error");
                }
            }
            else
            {
                return View(addressViewModel);
            }
        }

        public ActionResult MyOrders()
        {
            OrdersViewModel ordersViewModel = new OrdersViewModel();
            try
            {
                OrdersDTO ordersDTO = orderBusinessContext.GetOrders(new Guid(Session["UserID"].ToString()));
                ordersViewModel.Orders = _OrdersMapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(ordersDTO.Orders);
                ordersViewModel.IsLoggedIn = true;
            }
            catch(NoOrderException ex)
            {
                return View("NoOrders");
            }
            catch(Exception ex)
            {
                return View("InternalError");
            }
            return View(ordersViewModel);
        }
    }
}
