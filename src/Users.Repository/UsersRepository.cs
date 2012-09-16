using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Users.Repository
{
    public class UsersRepository : IUsersRepository
    {
        DataBaseEntities context = new DataBaseEntities();

        public void InsertUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetUserInfo(string username)
        {
            return context.Users.Where(u => username.Equals(u.username)).SingleOrDefault();
        }

        public void UpdateUserInfo(UserInfoViewMetadata user)
        {
            User toUpdate = context.Users.Where(u => user.username.Equals(u.username)).SingleOrDefault();

            //toUpdate.Apartment_number = user.Apartment_number;
            //toUpdate.City = user.City;
            //toUpdate.Email = user.Email;
            //toUpdate.House_number = user.House_number;
            //toUpdate.Name = user.Name;
            //toUpdate.Project = user.Project;
            //toUpdate.Street = user.Street;
            //toUpdate.Surname = user.Surname;
            //toUpdate.Zip_code = user.Zip_code;

            context.Entry(toUpdate).CurrentValues.SetValues(user);
            context.SaveChanges();
        }

    }
}
