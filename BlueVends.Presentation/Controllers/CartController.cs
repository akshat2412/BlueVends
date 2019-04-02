using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.ActionFilters;
using BlueVends.Presentation.Mappers.Cart;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class CartController : Controller
    {
        CartBusinessContext cartBusinessContext;
        private IMapper _CartMapper;
        private IMapper _CartsMapper;
        public CartController()
        {
            cartBusinessContext = new CartBusinessContext();

            _CartMapper = AutoMappers.CartMapper;
            _CartsMapper = AutoMappers.CartsMapper;
        }

        // POST: Add item to cart
        [HttpPost]
        public ActionResult AddItem([Bind(Include = "VariantID, ProductID, OrderQuantity, OrderLimit, Inventory")] CartViewModel cartViewModel )
        {
            CartMessageViewModel cartMessageViewModel = new CartMessageViewModel();

            if (ModelState.IsValid)
            {
                CartDTO cartDTO = _CartMapper.Map<CartDTO>(cartViewModel);
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
            cartsViewModel.CartItems = _CartsMapper.Map<IEnumerable<CartVariantDTO>, IEnumerable<CartVariantViewModel>>(newCartsDTO.CartItems);
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
