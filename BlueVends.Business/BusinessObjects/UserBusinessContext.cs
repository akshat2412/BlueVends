﻿using BlueVends.Business.Exceptions;
using BlueVends.DataAccess.DBObjects;
using BlueVends.DataAccess.Exceptions;
using BlueVends.Shared.DTO.User;
using System;

namespace BlueVends.Business.BusinessObjects
{
    public class UserBusinessContext
    {
        UserDatabase UserDBObject;
        public UserBusinessContext()
        {
            UserDBObject = new UserDatabase();
        }

        public UserBasicDTO LoginUser(UserLoginDTO userLoginDTO)
        {
            //userLoginDTO.Password = PasswordHasher.HashPassword(userLoginDTO.Password);
            try
            {
                if (UserDBObject.UserExists(userLoginDTO))
                {
                    UserBasicDTO newUserBasicDTO = UserDBObject.GetUser(userLoginDTO);
                    if(PasswordHasher.VerifyPassword(userLoginDTO.Password, newUserBasicDTO.PasswordHash))
                    {
                        return newUserBasicDTO;
                    }
                }
                throw new InvalidLoginException();
            }
            catch(NotFoundException ex)
            {
                throw new InvalidLoginException();
            }
        }

        public UserBasicDTO RegisterUser(UserDTO userDTO)
        {
            try
            {
                if (UserDBObject.UserNameExists(userDTO.UserName))
                {
                    throw new UserNameAlreadyExistsException();
                }
            }
            catch (NotFoundException ex)
            {
                userDTO.PasswordHash = PasswordHasher.HashPassword(userDTO.Password);
                UserBasicDTO newUserBasicDTO = UserDBObject.AddUser(userDTO);
                return newUserBasicDTO;
            }
            catch (UserNameAlreadyExistsException ex)
            {
                throw new UserNameAlreadyExistsException();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return null;
        }

        public UserOrdersDTO GetOrders(Guid userID)
        {
            UserOrdersDTO newuserOrdersDTO = UserDBObject.GetOrders(userID);
            return newuserOrdersDTO;
        }

        public UserOrderDTO GetOrder(Guid orderId)
        {
            UserOrderDTO newUserOrderDTO = UserDBObject.GetOrder(orderId);
            return newUserOrderDTO;
        }

        public bool CheckAdmin(Guid UserID)
        {
            return UserDBObject.CheckAdmin(UserID);
        }
    }
}
