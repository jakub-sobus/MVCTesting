using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Users.Repository
{
    public class UsersRepository : IUsersRepository
    {
        dbadce929125d548b3b43fa0a900f524c4Entities context = new dbadce929125d548b3b43fa0a900f524c4Entities();

        public void InsertUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetUserInfo(string username)
        {
            return context.Users.Where(u => username.Equals(u.username)).SingleOrDefault();
        }

        public void UpdateUserInfo(User user)
        {
            User toUpdate = context.Users.Where(u => user.username.Equals(u.username)).SingleOrDefault();
            context.Entry(toUpdate).CurrentValues.SetValues(user);
            context.SaveChanges();
        }

    }
}
