using BlueVends.Presentation.ViewModels;
using BlueVends.Business.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BlueVends.Shared.DTO.User;
using BlueVends.Business.Exceptions;
namespace BlueVends.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        UserBusinessContext userBusinessContext;
        IMapper RegistrationVMMapper;
        public RegisterController()
        {
            userBusinessContext = new UserBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserRegistrationViewModel, UserDTO>();
            });

            RegistrationVMMapper = new Mapper(config);
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

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    UserDTO userDTO = RegistrationVMMapper.Map<UserRegistrationViewModel, UserDTO>(userRegistrationViewModel);
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

        // GET: Register/Edit/5
        public ActionResult Success()
        {
            return View();
        }

        // POST: Register/Edit/5
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
    }
}
