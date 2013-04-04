using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;
using System.Xml;

namespace Brew.Controllers
{
    public class DBController : Controller
    {
        //
        // GET: /DB/

        public ActionResult Index()
        {
            SimpleMembershipInitializer();

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

        public void ParseHopXML(string fileLocation)
        {
            XmlTextReader reader = new XmlTextReader(fileLocation);
            var currentNodeName = "";
            Models.Hop hop = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        if (currentNodeName == "HOP")
                        {
                            hop = new Models.Hop();
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                hop.Name = reader.Value;
                                break;
                            case "ALPHA":
                                hop.Alpha = float.Parse(reader.Value);
                                break;
                            case "AMOUNT":
                                hop.Weight = float.Parse(reader.Value);
                                break;
                            case "USE":
                                hop.HopUses = Models.HopUtils.getHopUse(reader.Value);
                                break;
                            case "TIME":
                                hop.Time = float.Parse(reader.Value);
                                break;
                            case "NOTES":
                                hop.Notes = reader.Value;
                                break;
                            case "TYPE":
                                hop.HopType = Models.HopUtils.getHopType(reader.Value);
                                break;
                            case "FORM":
                                hop.HopForm = Models.HopUtils.getHopForm(reader.Value);
                                break;
                            case "BETA":
                                hop.Beta = float.Parse(reader.Value);
                                break;
                            case "HSI":
                                hop.HSI = float.Parse(reader.Value);
                                break;
                            case "ORIGIN":
                                hop.Origin = reader.Value;
                                break;
                            case "SUBSTITUTES":
                                hop.Substitutes = reader.Value;
                                break;
                            case "HUMULENE":
                                hop.Humulene = float.Parse(reader.Value);
                                break;
                            case "CARYOPHYLLENE":
                                hop.Caryophyllene = float.Parse(reader.Value);
                                break;
                            case "COHUMULONE":
                                hop.Cohumulone = float.Parse(reader.Value);
                                break;
                            case "MYRCENE":
                                hop.Myrcene = float.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "HOP" && hop != null)
                        {
                            using (var context = new Models.UsersContext())
                            {
                                context.Hops.Add(hop);
                                context.SaveChanges();
                            }
                        }
                        break;
                }
            }
        }

        public void ParseYeastXML(string fileLocation)
        {
            XmlTextReader reader = new XmlTextReader(fileLocation);
            var currentNodeName = "";
            Models.Yeast yeast = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        if (currentNodeName == "YEAST")
                        {
                            yeast = new Models.Yeast();
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                yeast.Name = reader.Value;
                                break;
                            case "TYPE":
                                yeast.YeastType = Models.YeastUtils.getYeastType(reader.Value);
                                break;
                            case "FORM":
                                yeast.YeastForm = Models.YeastUtils.getYeastForm(reader.Value);
                                break;
                            case "AMOUNT":
                                yeast.Amount = float.Parse(reader.Value);
                                break;
                            case "AMOUNT_IS_WEIGHT":
                                yeast.AmoutIsWeight = bool.Parse(reader.Value);
                                break;
                            case "LABORATORY":
                                yeast.Laboratory = reader.Value;
                                break;
                            case "PRODUCT_ID":
                                yeast.ProductID = int.Parse(reader.Value);
                                break;
                            case "MIN_TEMPERATURE":
                                yeast.MinTemperature = float.Parse(reader.Value);
                                break;
                            case "MAX_TEMPERATURE":
                                yeast.MaxTemperature = float.Parse(reader.Value);
                                break;
                            case "FLOCCULATION":
                                yeast.Focculation = Models.YeastUtils.getYeastFlocculation(reader.Value);
                                break;
                            case "ATTENUATION":
                                yeast.Attenuation = float.Parse(reader.Value);
                                break;
                            case "NOTES":
                                yeast.Notes = reader.Value;
                                break;
                            case "BEST_FOR":
                                yeast.BestFor = reader.Value;
                                break;
                            case "TIMES_CULTURED":
                                yeast.TimesCultured = int.Parse(reader.Value);
                                break;
                            case "MAX_REUSE":
                                yeast.MaxReuse = int.Parse(reader.Value);
                                break;
                            case "ADD_TO_SECONDARY":
                                yeast.AddToSecondary = bool.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "YEAST" && yeast != null)
                        {
                            using (var context = new Models.UsersContext())
                            {
                                context.Yeasts.Add(yeast);
                                context.SaveChanges();
                            }
                        }
                        break;
                }
            }
        }

        public void ParseFermentableXML(string fileLocation)
        {
            XmlTextReader reader = new XmlTextReader(fileLocation);
            var currentNodeName = "";
            Models.Fermentable fermentable = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        if (currentNodeName == "FERMENTABLE")
                        {
                            fermentable = new Models.Fermentable();
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                fermentable.Name = reader.Value;
                                break;
                            case "TYPE":
                                fermentable.FermentableType = Models.FermentableUtils.getFermentableType(reader.Value);
                                break;
                            case "AMOUNT":
                                fermentable.Amount = float.Parse(reader.Value);
                                break;
                            case "YIELD":
                                fermentable.Yield = float.Parse(reader.Value);
                                break;
                            case "COLOR":
                                fermentable.Color = float.Parse(reader.Value);
                                break;
                            case "ADD_AFTER_BOIL":
                                fermentable.AddAfterBoil = bool.Parse(reader.Value);
                                break;
                            case "ORIGIN":
                                fermentable.Origin = reader.Value;
                                break;
                            case "NOTES":
                                fermentable.Notes = reader.Value;
                                break;
                            case "COARSE_FINE_DIFF":
                                fermentable.CoarseFineDiff = float.Parse(reader.Value);
                                break;
                            case "MOISTURE":
                                fermentable.Moisture = float.Parse(reader.Value);
                                break;
                            case "PROTEIN":
                                fermentable.Protein = float.Parse(reader.Value);
                                break;
                            case "MAX_IN_BATCH":
                                fermentable.MaxInBatch = float.Parse(reader.Value);
                                break;
                            case "RECOMMEND_MASH":
                                fermentable.Recommended_Mash = bool.Parse(reader.Value);
                                break;
                            case "IS_MASHED":
                                fermentable.IsMashed = bool.Parse(reader.Value);
                                break;
                            case "IBU_GAL_PER_LB":
                                fermentable.IBUs = float.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "FERMENTABLE" && fermentable != null)
                        {
                            using (var context = new Models.UsersContext())
                            {
                                context.Fermentables.Add(fermentable);
                                context.SaveChanges();
                            }
                        }
                        break;
                }
            }
        }

        public ActionResult Generate()
        {
            // ParseHopXML("C:\\Projects\\Brew\\BeerXML\\Hops.xml");
            // ParseYeastXML("C:\\Projects\\Brew\\BeerXML\\Yeast.xml");
            // ParseFermentableXML("C:\\Projects\\Brew\\BeerXML\\Fermentables.xml");
            ViewBag.Message = "Record Inserted";
            return View();
        }
    }
}
