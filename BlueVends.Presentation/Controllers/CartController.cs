using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.ViewModels;
using AutoMapper;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class CartController : Controller
    {
        CartBusinessContext cartBusinessContext;
        IMapper CartMapper;
        IMapper CartsMapper;
        public CartController()
        {
            cartBusinessContext = new CartBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CartViewModel, CartDTO>();
            });

            var config2 = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<VariantDTO, VariantViewModel>();
                cfg.CreateMap<CartVariantDTO, CartVariantViewModel>();
            });

            CartMapper = new Mapper(config);
            CartsMapper = new Mapper(config2);
        }
        // GET: Cart
        [HttpPost]
        public ActionResult AddItem([Bind(Include = "VariantID, ProductID, OrderQuantity, OrderLimit, Inventory")] CartViewModel cartViewModel )
        {
            CartMessageViewModel cartMessageViewModel = new CartMessageViewModel();

            if (ModelState.IsValid)
            {
                CartDTO cartDTO = CartMapper.Map<CartDTO>(cartViewModel);
                cartDTO.UserID = new Guid(Session["UserID"].ToString());
                try
                {
                    cartBusinessContext.AddItemToCart(cartDTO);
                    cartMessageViewModel.SuccessMessage = "Item Successfuly Added to Cart";
                    cartMessageViewModel.IsLoggedIn = true;
                    return View(cartMessageViewModel);
                }
                catch(ItemAlreadyInCartException ex)
                {
                    cartMessageViewModel.ErrorMessages.Add("Item is already present in the cart");
                    return View(cartMessageViewModel);
                }
                catch(Exception ex)
                {
                    return View("InternalError");
                }
            }
            else
            {
                foreach(var modelState in ModelState.Values)
                {
                    foreach(var err in modelState.Errors)
                    {
                        cartMessageViewModel.ErrorMessages.Add(err.ErrorMessage.ToString());
                    }
                }
                return View(cartMessageViewModel);
            }
        }

        
        public ActionResult ViewCart()
        {
            CartsDTO newCartsDTO = cartBusinessContext.GetCart(new Guid(Session["UserID"].ToString()));
            CartsViewModel cartsViewModel = new CartsViewModel();
            cartsViewModel.CartItems = CartsMapper.Map<IEnumerable<CartVariantDTO>, IEnumerable<CartVariantViewModel>>(newCartsDTO.CartItems);
            cartsViewModel.CartItems = cartsViewModel.CartItems.ToList();
            cartsViewModel.SubTotal = newCartsDTO.SubTotal;
            cartsViewModel.IsLoggedIn = true;
            return View(cartsViewModel);
        }


        public ActionResult RemoveItem(Guid VariantID)
        {
            cartBusinessContext.RemoveItem(new Guid(Session["UserID"].ToString()), VariantID);
            return RedirectToAction("ViewCart");
        }
    }
}
