using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Users.Repository
{
    public static class UsersRepositoryFactory
    {
        public static IUsersRepository CreateAppStoreReportsRepository()
        {
            return new UsersRepository();
        }
    }
}
