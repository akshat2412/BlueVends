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
                    if(Request.UrlReferrer.ToString() == "a") { return null; }
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
    }
}
