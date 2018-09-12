using BlueVends.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Shared.DTO.User;
using BlueVends.Business.Exceptions;

namespace BlueVends.Presentation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        UserBusinessContext userBusinessContext;
        IMapper LoginMapper;
        public LoginController()
        {
            userBusinessContext = new UserBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LoginViewModel, UserLoginDTO>();
            });

            LoginMapper = new Mapper(config);
        }

        public ActionResult Login()
        {
            return View();
        }


        // GET: Login/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName, Password" )] LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                UserLoginDTO userLoginDTO = LoginMapper.Map<LoginViewModel, UserLoginDTO>(loginViewModel);
                try
                {
                    UserBasicDTO loggedInUserDTO = userBusinessContext.LoginUser(userLoginDTO);
                    Session["UserID"] = loggedInUserDTO.ID;
                    ViewBag.LoggedIn = "True";
                    return RedirectToAction("Index", "Home");
                }
                catch(InvalidLoginException ex)
                {

                    ModelState.AddModelError("", "Invalid Login Credentials.");
                    return View(loginViewModel);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong, Please try again.");
                    return View("Error");
                }
            }
            return View(loginViewModel);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
