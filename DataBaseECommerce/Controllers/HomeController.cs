using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataBaseECommerce.Controllers
{
    public class HomeController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        ECommerceEntities db = new ECommerceEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //calling the query strings in visual studio
        public ActionResult CustomersCities()
        {
            var result = db.Database.SqlQuery<string>("EXEC GetAllCities").ToList();
            Logger.Info("user is getting a list of cities");
            return View(result);
        }

        public ActionResult AllCustomers()
        {
            var result = db.Database.SqlQuery<string>("EXEC GetAllCustomers").ToList();
            return View(result);
        }

        public ActionResult CustomersInCities(string cityName)
        {
            var result = db.Database.SqlQuery<string>
                ("EXEC CustomersInCity @City", new SqlParameter("city", cityName)).ToList();
            return View("CustomerCities", result);
        }

        public string FirstCustomerInCity(string cityName)
        {
            var result = db.Database.SqlQuery<string>
                ("EXEC FirstCustomerInCity @City", new SqlParameter("city", cityName)).ToList();
            return result.FirstOrDefault();
        }

        public ActionResult GetUserInfo(int id)
        {
            try
            {
                var result = db.Database.SqlQuery<UserInfo>
              ("EXEC FirstCustomerInCity @id", new SqlParameter("id", id)).ToList();
                return View(result);
            }
            catch (Exception c)
            {
                Logger.Error("An Error Occured while getting the user {0}.");
                return View(new List<UserInfo>());
            }
        }

        public ActionResult ShowProductCategory()
        {
            var result = db.Database.SqlQuery<ProductCategory>
                ("EXEC ShowProductCategory").ToList();
            return View(result);
        }

        public ActionResult ShowProductDetails(int id)
        {
            var result = db.Database.SqlQuery<ProductInfo>
                ("EXEC ProductDetails @id", new SqlParameter("id", id)).ToList();
            return View("ShowProductDetails", result);
        }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class ProductCategory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductInfo
    {
        public string ProductName { get; set; }
        public  int Stock { get; set; }
        public float Price { get; set; }
    }
}