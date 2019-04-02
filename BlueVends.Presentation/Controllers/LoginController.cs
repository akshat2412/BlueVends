using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.Mappers.Login;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;
using System;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    public class LoginController : Controller
    {

        UserBusinessContext userBusinessContext;
        IMapper _LoginMapper;
        public LoginController()
        {
            userBusinessContext = new UserBusinessContext();
            _LoginMapper = AutoMappers.LoginMapper;
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
                UserLoginDTO userLoginDTO = _LoginMapper.Map<LoginViewModel, UserLoginDTO>(loginViewModel);
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
