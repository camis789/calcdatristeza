using System;
using SisProdutos.models;
using SisProdutos.Config;
using System.Linq;
using System.Collections.Generic;

namespace SisProdutos.Service
{
    public class UserService
    {
        DbContextProduct context = new DbContextProduct();

        public User AddUser(User user)
        {
            user.DateCreate = DateTime.Now;

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public bool Auth(User user)
        {
            var result = context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<User> ListUsers()
        {
            var result = context.Users.ToList();

            return result;
        }
    }
}
