﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;
using System.Xml;

namespace Brew.DBGeneration
{
    
    public class BrewXMLParser
    {        
        //
        // GET: /DB/

        public Models.Yeast ParseYeasts(XmlTextReader reader, ref bool AddToSecondary, ref float Amount, ref bool AmoutIsWeight)
        {
            Models.Yeast yeast = new Models.Yeast();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                yeast.Name = reader.Value;
                                break;
                            case "TYPE":
                                Models.YeastType yeastType = new Models.YeastType() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.YeastTypes.Find(yeastType.Name) == null)
                                    {
                                        context.YeastTypes.Add(yeastType);
                                        context.SaveChanges();
                                    }
                                }
                                yeast.YeastType_Name = reader.Value;
                                break;
                            case "FORM":
                                Models.YeastForm yeastForm = new Models.YeastForm() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.YeastForms.Find(yeastForm.Name) == null)
                                    {
                                        context.YeastForms.Add(yeastForm);
                                        context.SaveChanges();
                                    }
                                }
                                yeast.YeastForm_Name = reader.Value;
                                break;
                            case "AMOUNT":
                                Amount = float.Parse(reader.Value);
                                break;
                            case "AMOUNT_IS_WEIGHT":
                                AmoutIsWeight = bool.Parse(reader.Value);
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
                                Models.YeastFlocculation focculation = new Models.YeastFlocculation() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.YeastFlocculations.Find(focculation.Name) == null)
                                    {
                                        context.YeastFlocculations.Add(focculation);
                                        context.SaveChanges();
                                    }
                                }
                                yeast.YeastFlocculation_Name = reader.Value;
                                break;
                            case "ATTENUATION":
                                yeast.Attenuation = float.Parse(reader.Value);
                                break;
                            case "TIMES_CULTURED":
                                yeast.TimesCultured = int.Parse(reader.Value);
                                break;
                           case "ADD_TO_SECONDARY":
                                AddToSecondary = bool.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "YEAST")
                        {
                            return yeast;
                        }
                        break;
                }
            }
            return yeast;
        }

        public Models.Fermentable ParseFermentable(XmlTextReader reader, ref bool AddAfterBoil, ref bool IsMashed, ref float Amount)
        {
            Models.Fermentable fermentable = new Models.Fermentable();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        break;
                    case XmlNodeType.Text:
                         switch (currentNodeName)
                        {
                            case "NAME":
                                fermentable.Name = reader.Value;
                                break;
                            case "TYPE":
                                Models.FermentableType fermentableType = new Models.FermentableType() { Name = reader.Value};
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.FermentableTypes.Find(fermentableType.Name) == null)
                                    {
                                        context.FermentableTypes.Add(fermentableType);
                                        context.SaveChanges();
                                    }
                                }

                                fermentable.FermentableType_Name = reader.Value;
                                break;
                            case "AMOUNT":
                                Amount = float.Parse(reader.Value);
                                break;
                            case "YIELD":
                                fermentable.Yield = float.Parse(reader.Value);
                                break;
                            case "COLOR":
                                fermentable.Color = float.Parse(reader.Value);
                                break;
                            case "ADD_AFTER_BOIL":
                                AddAfterBoil = bool.Parse(reader.Value);
                                break;
                            case "ORIGIN":
                                fermentable.Origin = reader.Value;
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
                           case "IS_MASHED":
                                IsMashed = bool.Parse(reader.Value);
                                break;
                            case "IBU_GAL_PER_LB":
                                fermentable.IBUs = float.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "FERMENTABLE")
                        {
                            return fermentable;
                        }
                        break;
                }
            }
            return fermentable;
        }

        public Models.Hop ParseHop(XmlTextReader reader, ref string HopUses_Name, ref float Amount, ref float Time)
        {
            Models.Hop hop = new Models.Hop();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
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
                                Amount = float.Parse(reader.Value);
                                break;
                            case "USE":
                                Models.HopUse hopUse = new Models.HopUse() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.HopUses.Find(hopUse.Name) == null)
                                    {
                                        context.HopUses.Add(hopUse);
                                        context.SaveChanges();
                                    }
                                }
                                HopUses_Name = reader.Value;
                                break;
                            case "TIME":
                                Time = float.Parse(reader.Value);
                                break;
                           case "TYPE":
                                Models.HopType hopType = new Models.HopType() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.HopTypes.Find(hopType.Name) == null)
                                    {
                                        context.HopTypes.Add(hopType);
                                        context.SaveChanges();
                                    }
                                }
                                hop.HopType_Name = reader.Value;
                                break;
                            case "FORM":
                                Models.HopForm hopForm = new Models.HopForm() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.HopForms.Find(hopForm.Name) == null)
                                    {
                                        context.HopForms.Add(hopForm);
                                        context.SaveChanges();
                                    }
                                }
                                hop.HopForm_Name = reader.Value;
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
                                //hop.Substitutes = reader.Value;
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
                        if (reader.Name == "HOP")
                        {
                            return hop;
                        }
                        break;
                }
            }
            return hop;
        }

        public Models.Style ParseStyle(XmlTextReader reader)
        {
            Models.Style style = new Models.Style();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                style.Name = reader.Value;
                                break;
                            case "CATEGORY":
                                style.Category = reader.Value;
                                break;
                            case "CATEGORY_NUMBER":
                                style.CategoryNumber = reader.Value;
                                break;
                            case "STYLE_LETTER":
                                style.StyleLetter = reader.Value;
                                break;
                            case "STYLE_GUIDE":
                                style.StyleGuide = reader.Value;
                                break;
                            case "TYPE":
                                Models.StyleType styleType = new Models.StyleType() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.StyleTypes.Find(styleType.Name) == null)
                                    {
                                        context.StyleTypes.Add(styleType);
                                        context.SaveChanges();
                                    }
                                }
                                style.StyleType_Name = reader.Value;
                                break;
                            case "OG_MIN":
                                style.OGMin = float.Parse(reader.Value);
                                break;
                            case "OG_MAX":
                                style.OGMax = float.Parse(reader.Value);
                                break;
                            case "FG_MIN":
                                style.FGMin = float.Parse(reader.Value);
                                break;
                            case "FG_MAX":
                                style.FGMax = float.Parse(reader.Value);
                                break;
                            case "IBU_MIN":
                                style.IBUMin = float.Parse(reader.Value);
                                break;
                            case "IBU_MAX":
                                style.IBUMax = float.Parse(reader.Value);
                                break;
                            case "COLOR_MIN":
                                style.ColorMin = float.Parse(reader.Value);
                                break;
                            case "COLOR_MAX":
                                style.ColorMax = float.Parse(reader.Value);
                                break;
                            case "ABV_MIN":
                                style.ABVMin = float.Parse(reader.Value);
                                break;
                            case "ABV_MAX":
                                style.ABVMax = float.Parse(reader.Value);
                                break;
                            case "CARB_MIN":
                                style.CRABMin = float.Parse(reader.Value);
                                break;
                            case "CARB_MAX":
                                style.CRABMax = float.Parse(reader.Value);
                                break;
                            case "NOTES":
                                style.Notes = reader.Value;
                                break;
                            case "PROFILE":
                                style.Profile = reader.Value;
                                break;
                            case "INGREDIENTS":
                                style.Ingredients = reader.Value;
                                break;
                            case "EXAMPLES":
                                style.Eamples = reader.Value;
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "STYLE")
                        {
                            return style;
                        }
                        break;
                }
            }
            return style;
        }

        public void ParseEquipment(XmlTextReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.Name == "EQUIPMENT")
                        {
                            return;
                        }
                        break;
                }
            }
        }
        public void ParseBeerXML(string fileLocation)
        {
            XmlTextReader reader = new XmlTextReader(fileLocation);
            var currentNodeName = "";

            Models.Recipe recipe = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: 
                        currentNodeName = reader.Name;
                        switch (currentNodeName)
                        {
                            case "RECIPE":
                                recipe = new Models.Recipe();
                                break;
                            case "STYLE":
                                Models.Style style = ParseStyle(reader);
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.Styles.Find(style.Name) == null)
                                    {
                                        context.Styles.Add(style);
                                        context.SaveChanges();
                                    }
                                }
                                recipe.Style = style;
                                break;
                             case "HOP":
                                string HopUses_Name = "";
                                float Amount = 0;
                                float Time = 0;
                                Models.Hop hop = ParseHop(reader, ref HopUses_Name, ref Amount, ref Time);
                                Models.RecipeHop recipeHop = new Models.RecipeHop();
                                recipeHop.Recipe_Name = recipe.Name;
                                recipeHop.Hop_Name = hop.Name;
                                recipeHop.HopUses_Name = HopUses_Name;
                                recipeHop.Amount = Amount;
                                recipeHop.Time = Time;

                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.Hops.Find(hop.Name) == null)
                                    {
                                        context.Hops.Add(hop);
                                        context.SaveChanges();
                                    }
                                }
                                if (!recipe.RecipeHops.Contains(recipeHop))
                                {
                                    recipe.RecipeHops.Add(recipeHop);
                                }
                                break;
                             case "FERMENTABLE":
                                bool AddAfterBoil = false;
                                bool IsMashed = false;
                                Amount = 0;
                                Models.Fermentable fermentable = ParseFermentable(reader, ref AddAfterBoil, ref IsMashed, ref Amount);
                                
                                Models.RecipeFermentable recipeFermentable = new Models.RecipeFermentable();
                                recipeFermentable.AddAfterBoil = AddAfterBoil;
                                recipeFermentable.IsMashed = IsMashed;
                                recipeFermentable.Fermentable_Name = fermentable.Name;
                                recipeFermentable.Recipe_Name = recipe.Name;
                                recipeFermentable.Amount = Amount;
                                
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.Fermentables.Find(fermentable.Name) == null)
                                    {
                                        context.Fermentables.Add(fermentable);
                                        context.SaveChanges();
                                    }                                   
                                }
                                
                                if (!recipe.RecipeFermentables.Contains(recipeFermentable))
                                {
                                    recipe.RecipeFermentables.Add(recipeFermentable);
                                }
                                break;
                             case "YEAST":
                                bool AddToSecondary = false;
                                bool AmountIsWeight = false;
                                Amount = 0;
                                Models.Yeast yeast = ParseYeasts(reader, ref AddToSecondary, ref Amount, ref AmountIsWeight);

                                Models.RecipeYeast recipeYeast = new Models.RecipeYeast();
                                recipeYeast.AddToSecondary = AddToSecondary;
                                recipeYeast.Recipe_Name = recipe.Name;
                                recipeYeast.Yeast_Name = yeast.Name;
                                recipeYeast.AmountIsWeight = AmountIsWeight;
                                recipeYeast.Amount = Amount;

                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.Yeasts.Find(yeast.Name) == null)
                                    {
                                        context.Yeasts.Add(yeast);
                                        context.SaveChanges();
                                    }
                                }
                                if (!recipe.RecipeYeasts.Contains(recipeYeast))
                                {
                                    recipe.RecipeYeasts.Add(recipeYeast);
                                }
                                break;
                             case "MASH":
                                Models.MashProfile mash = ParseMash(reader);                               
                                recipe.Mash = mash;
                                break;
                            case "EQUIPMENT":
                                ParseEquipment(reader);
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (recipe == null)
                        {
                            break;
                        }
                        switch (currentNodeName)
                        {
                            case "NAME":
                                recipe.Name = reader.Value;
                                break;
                            case "TYPE":
                                Models.RecipieType recipieType = new Models.RecipieType() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.RecipieTypes.Find(recipieType.Name) == null)
                                    {
                                        context.RecipieTypes.Add(recipieType);
                                        context.SaveChanges();
                                    }
                                }
                                recipe.RecipieType_Name = reader.Value;
                                break;
                            case "BATCH_SIZE":
                                recipe.BatchSize = float.Parse(reader.Value);
                                break;
                            case "BOIL_SIZE":
                                recipe.BoilSize = float.Parse(reader.Value);
                                break;
                            case "BOIL_TIME":
                                recipe.BoilTime = float.Parse(reader.Value);
                                break;
                            case "EFFICIENCY":
                                recipe.Efficiency = float.Parse(reader.Value);
                                break;
                            case "NOTES":
                                recipe.Notes = reader.Value;
                                break;
                            case "TASTE_NOTES":
                                recipe.TasteNotes = reader.Value;
                                break;
                            case "TASTE_RATING":
                                recipe.TasteRating = float.Parse(reader.Value);
                                break;
                            case "OG":
                                recipe.OG = float.Parse(reader.Value);
                                break;
                            case "FG":
                                recipe.FG = float.Parse(reader.Value);
                                break;
                            case "FERMENTATION_STAGES":
                                recipe.FermentationStages = int.Parse(reader.Value);
                                break;
                            case "PRIMARY_AGE":
                                recipe.PrimayAge = float.Parse(reader.Value);
                                break;
                            case "PRIMARY_TEMP":
                                recipe.PrimayTemp = float.Parse(reader.Value);
                                break;
                            case "SECONDARY_AGE":
                                recipe.SecondaryAge = float.Parse(reader.Value);
                                break;
                            case "SECONDARY_TEMP":
                                recipe.SecondaryTemp = float.Parse(reader.Value);
                                break;
                            case "TERTIARY_AGE":
                                recipe.TertiaryAge = float.Parse(reader.Value);
                                break;
                            case "TERTIARY_TEMP":
                                recipe.TertiaryTemp = float.Parse(reader.Value);
                                break;
                            case "AGE":
                                recipe.Age = float.Parse(reader.Value);
                                break;
                            case "AGE_TEMP":
                                recipe.AgeTemp = float.Parse(reader.Value);
                                break;
                            case "DATE":
                                DateTime maxDt = new DateTime(2000,1,1,17,0,0);
                                TimeSpan timeSpan = DateTime.Today - maxDt;
                                var randomTest = new Random();
                                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                                DateTime newDate = maxDt + newSpan;
                                recipe.Date = DateTime.Today;
                                break;
                            case "CARBONATION":
                                recipe.Carbonation = float.Parse(reader.Value);
                                break;
                            case "FORCED_CARBONATION":
                                recipe.ForcedCarbonation = bool.Parse(reader.Value);
                                break;
                            case "PRIMING_SUGAR_NAME":
                                recipe.PrimingSugarName = reader.Value;
                                break;
                            case "CARBONATION_TEMP":
                                recipe.CarbonationTemp = float.Parse(reader.Value);
                                break;
                            case "PRIMING_SUGAR_EQUIV":
                                recipe.PrimingSugarEquiv = float.Parse(reader.Value);
                                break;
                            case "KEG_PRIMING_FACTOR":
                                recipe.KegPrimingFactor = float.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "RECIPE" && recipe != null)
                        {
                            using (var context = new Models.ModelsContext())
                            {
                                if (context.Recipes.Find(recipe.Name) == null)
                                {
                                    //foreach (Models.Yeast yeast in recipe.Yeasts)
                                    //{                                        
                                    //    context.Yeasts.Attach(yeast);
                                    //    yeast.UsingRecipes.Add(recipe);                                
                                    //}

                                    //foreach (Models.Fermentable fermentable in recipe.Fermentables)
                                    //{
                                    //    context.Fermentables.Attach(fermentable);       
                                    //    fermentable.UsingRecipes.Add(recipe);                                        
                                    //}

                                    //foreach (Models.Hop hop in recipe.Hops)
                                    //{
                                    //    context.Hops.Attach(hop);
                                    //    hop.UsingRecipes.Add(recipe);                                      
                                    //}

                                    context.Styles.Attach(recipe.Style);
                                    recipe.Style.UsingRecipes.Add(recipe);
                                
                                    context.MashProfiles.Add(recipe.Mash);
                                    
                                    context.Recipes.Add(recipe);
                                    context.SaveChanges();
                                }
                            }
                            recipe = null;
                        }
                        break;
                }
            }
        }

        public Models.MashProfile ParseMash(XmlTextReader reader)
        {
            int stepSequenceNumber = 1;
            Models.MashProfile mash = new Models.MashProfile();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        switch (currentNodeName)
                        {
                            case "MASH_STEP":
                                Models.MashStep step = ParseMashStep(reader);
                                step.SequenceNumber = stepSequenceNumber;
                                stepSequenceNumber++;                                
                                mash.Steps.Add(step);
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                mash.Name = reader.Value;
                                break;
                            case "GRAIN_TEMP":
                                mash.GrainTemp = float.Parse(reader.Value);
                                break;
                            case "NOTES":
                                mash.Notes = reader.Value;
                                break;
                            case "TUN_TEMP":
                                mash.TunTemp = float.Parse(reader.Value);
                                break;
                            case "SPARGE_TEMP":
                                mash.SpargeTemp = float.Parse(reader.Value);
                                break;
                            case "PH":
                                mash.PH = float.Parse(reader.Value);
                                break;
                            case "TUN_WEIGHT":
                                mash.TunWeight = float.Parse(reader.Value);
                                break;
                            case "TUN_SPECIFIC_HEAT":
                                mash.TunSpecificHeat = float.Parse(reader.Value);
                                break;
                            case "EQUIP_ADJUST":
                                mash.EquipAdjust = bool.Parse(reader.Value);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "MASH")
                        {
                            return mash;
                        }
                        break;
                }
            }
            return mash;
        }

        public Models.MashStep ParseMashStep(XmlTextReader reader)
        {
            Models.MashStep mashStep = new Models.MashStep();
            var currentNodeName = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        currentNodeName = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (currentNodeName)
                        {
                            case "NAME":
                                mashStep.Name = reader.Value;
                                break;
                            case "TYPE":
                                Models.MashStepType mashStepType = new Models.MashStepType() { Name = reader.Value };
                                using (var context = new Models.ModelsContext())
                                {
                                    if (context.MashStepTypes.Find(mashStepType.Name) == null)
                                    {
                                        context.MashStepTypes.Add(mashStepType);
                                        context.SaveChanges();
                                    }
                                }
                                mashStep.MashStepType_Name = reader.Value;
                                break;
                            case "INFUSE_AMOUNT":
                                mashStep.InfuseAmount = float.Parse(reader.Value);
                                break;
                            case "STEP_TEMP":
                                mashStep.StepTemp = float.Parse(reader.Value);
                                break;
                            case "STEP_TIME":
                                mashStep.StepTime = float.Parse(reader.Value);
                                break;
                            case "RAMP_TIME":
                                mashStep.RampTime = float.Parse(reader.Value);
                                break;
                            case "END_TEMP":
                                mashStep.EndTemp = float.Parse(reader.Value);
                                break;
                            case "INFUSE_TEMP":
                                mashStep.InfuseTemp = float.Parse(reader.Value);
                                break;
                            case "DECOCTION_AMOUNT":
                                mashStep.DecoctionAmount = float.Parse(reader.Value);
                                break;                            
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "MASH_STEP")
                        {
                            return mashStep;
                        }
                        break;
                }
            }
            return mashStep;
        }
               
        public void Generate()
        {
            //ParseBeerXML("C:\\Projects\\Brew\\BeerXML\\btrecipes.xml");
            //new Brew.DBGeneration.BrewXMLParser().Generate();  

            // Add a user to a recipe... 
            /*
            using (var context = new Models.ModelsContext())
            {
                //var recipe = context.Recipes.Where(x => x.Name == "Bt: American Barleywine").Select(x => x);
                //var user = context.UserProfiles.Where(x => x.UserId == 1).Select(x => x);

                //recipe.Single().Brewers.Add(user.Single());

                Random random = new Random();

                var recipes = context.Recipes.Select(x => x).ToArray();
                var users = context.UserProfiles.Select(x => x).ToArray();
                foreach (var recipe in recipes)
                {
                    // select random user
                    int i = random.Next(0, users.Length);
                    recipe.Brewers.Add(users[i]);
                }


                context.SaveChanges();
            }
             * */
        }        
    }
}
    