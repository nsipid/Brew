using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;

namespace Brew.Controllers
{
    public class DBController : Controller
    {
        //
        // GET: /DB/

        public ActionResult Index()
        {
            ViewBag.Message = "F DISK";

            return View();
        }

        public void SimpleMembershipInitializer()
        {
            Database.SetInitializer<Models.UsersContext>(null);

            try
            {
                using (var context = new Models.UsersContext())
                {
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                }

                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    
        public ActionResult Generate()
        {
            SimpleMembershipInitializer();
            var hop = new Models.Hop { Name = "Stupid hop" };
            using (var context = new Models.UsersContext())
            {
                context.Hops.Add(hop);
                context.SaveChanges();
            }
            Console.Write("Person saved !");
            return View();
        }
    }
}
