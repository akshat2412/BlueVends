using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueVends.Presentation.ActionFilters;
using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Order;
using BlueVends.Business.Exceptions;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.Product;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class OrderController : Controller
    {
        OrderBusinessContext orderBusinessContext;
        IMapper AddressMapper;
        IMapper OrdersMapper;
        public OrderController()
        {
            orderBusinessContext = new OrderBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddressViewModel, AddressDTO>();
            });

            AddressMapper = new Mapper(config);

            var config2 = new MapperConfiguration(cfg => {
                cfg.CreateMap<OrderDTO, OrderViewModel>().ForMember(x => x.Products, opt => opt.Ignore());
                //cfg.CreateMap<OrderProductDTO, OrderProductViewModel>();
                //cfg.CreateMap<ProductDTO, ProductViewModel>();
                //cfg.CreateMap<OrderDTO, OrderViewModel>();
            });

            OrdersMapper = new Mapper(config);
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
                    AddressDTO addressDTO = AddressMapper.Map<AddressDTO>(addressViewModel);
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
                ordersViewModel.Orders = OrdersMapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(ordersDTO.Orders);
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
