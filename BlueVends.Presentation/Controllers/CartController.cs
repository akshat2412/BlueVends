using BlueVends.Business.BusinessObjects;
using BlueVends.Presentation.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueVends.Business.Exceptions;
namespace BlueVends.Presentation.Controllers
{
    [UserAuthenticationFilter]
    public class CartController : Controller
    {
        CartBusinessContext cartBusinessContext = new CartBusinessContext();
        // GET: Cart
        public void AddItem(Guid VariantID)
        {
            try
            {
                cartBusinessContext.AddItemToCart(new Guid(Session["UserID"].ToString()), VariantID);
            }
            catch(ItemAlreadyInCartException ex)
            {
                return;
            }
            return;
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
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

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
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
