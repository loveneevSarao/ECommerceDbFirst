using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBaseECommerce.Models
{
    public class CustomerManager
    {
        ECommerceEntities db = new ECommerceEntities();

        public ICollection<Customer> GetCustomersInCity(string cityName)
        {
            var results = db.Customers.Where(c => c.City == cityName).ToList();
            return results;
        }
    }
}