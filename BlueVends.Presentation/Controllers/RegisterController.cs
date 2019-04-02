using AutoMapper;
using BlueVends.Business.BusinessObjects;
using BlueVends.Business.Exceptions;
using BlueVends.Presentation.Mappers.Register;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.User;
using System;
using System.Web.Mvc;

namespace BlueVends.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        UserBusinessContext userBusinessContext;
        IMapper _RegistrationVMMapper;
        public RegisterController()
        {
            userBusinessContext = new UserBusinessContext();
            _RegistrationVMMapper = AutoMappers.RegistrationVMMapper;
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name, UserName, Password, ConfirmedPassword, Email, PhoneNumber")] UserRegistrationViewModel userRegistrationViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UserDTO userDTO = _RegistrationVMMapper.Map<UserRegistrationViewModel, UserDTO>(userRegistrationViewModel);
                    UserBasicDTO newUserBasicDTO = userBusinessContext.RegisterUser(userDTO);
                    return View("Success");
                }
                else
                {
                    return View(userRegistrationViewModel);
                }
            }
            catch(UserNameAlreadyExistsException)
            {
                ModelState.AddModelError("", "Username is already taken");
                return View(userRegistrationViewModel);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong. Please try after some time");
            }
            return View("Error");
        }
    }
}
