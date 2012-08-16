using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Common.Repository
{
    public class CommonRepository
    {
        dbadce929125d548b3b43fa0a900f524c4Entities context = new dbadce929125d548b3b43fa0a900f524c4Entities();

        public List<string> GetCities()
        {
            return context.Cities.Select(c => c.City_name).ToList();
        }
    }
}
