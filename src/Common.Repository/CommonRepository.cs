using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Common.Repository
{
    public class CommonRepository
    {
        Entities context = new Entities();

        public List<string> GetCities()
        {
            return context.Cities.Select(c => c.City_name).ToList();
        }
    }
}
