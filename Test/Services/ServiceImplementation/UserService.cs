using System;
using System.Collections.Generic;
using System.Linq;
using Test.Codes;
using Test.Models;
using Test.Services.IService;

namespace Test.Services.ServiceImplementation
{
    public class UserService : IUserService
    {
        private readonly TestDBContext context;

        public UserService(TestDBContext context)
        {
            this.context = context;
        }

        public void DeleteUser(long id, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                Users user = context.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    List<Images> images = context.Images.Where(p => p.UserId == id).ToList();
                    context.Images.RemoveRange(images);
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    ErrorMessage = ErrorCode.NotFindUser;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }

        public Users GetUserProfile(long id, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                Users user = context.Users.FirstOrDefault(p => p.Id == id);

                if (user == null)
                {
                    ErrorMessage = ErrorCode.NotFindUser;
                }
                return user;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return null;
            }
        }

        public IEnumerable<Users> GetUsers(string FirstName, string LastName, string FatherName, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (FirstName == "_") { FirstName = ""; }
                if (LastName == "_") { LastName = ""; }
                if (FatherName == "_") { FatherName = ""; }

                var users = context.Users.Where(p => p.FirstName.Contains(FirstName)
                                                  && p.LastName.Contains(LastName)
                                                  && p.FatherName.Contains(FatherName)).ToList();
                return users;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void UpdateUser(long id, string FirstName, string LastName, string FatherName, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(FirstName))
                {
                    ErrorMessage = ErrorCode.EmptyField;
                    return;
                }
                else
                {
                    Users user = context.Users.FirstOrDefault(p => p.Id == id);

                    if (user != null)
                    {
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.FatherName = FatherName;
                        context.Users.Update(user);
                        context.SaveChanges();
                        return;
                    }
                    ErrorMessage = ErrorCode.NotFindUser;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }

        public void AddUser(string FirstName, string LastName, string FatherName, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(FirstName))
                {
                    ErrorMessage = ErrorCode.EmptyField;
                    return;
                }
                else
                {
                    context.Users.Add(new Users { FirstName = FirstName, LastName = LastName, FatherName = FatherName });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }
    }
}
