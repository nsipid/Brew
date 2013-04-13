using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.Utilities
{
    public class StatisticsUtils
    {
        public static double GetSiteAvg(string strBeerName)
        {
            Dictionary<string, long> oFlavorCounts = new Dictionary<string, long>();
            using (var context = new Models.ModelsContext())
            {
                //determine sitewide average of ratings for all recipes
                var vRecipes = from r in context.Recipes
                               select r.Name;

                //get sitewide average (prior mean)
                double dRecipesAvgRatingsSum = 0;
                long lRecipeRatingsCount;
                double dRecipeRatingsSum;
                double dRecipeRatingsAvg;
                foreach (var vRecipeName in vRecipes)
                {
                    GetRecipeRatingsSimpleStats((string)vRecipeName,
                                                out lRecipeRatingsCount,
                                                out dRecipeRatingsSum,
                                                out dRecipeRatingsAvg);

                    dRecipesAvgRatingsSum += dRecipeRatingsAvg;
                }
                double dRecipesAvgRating = dRecipesAvgRatingsSum / vRecipes.Count();

                //get local mean and sum of ratings for this recipe
                GetRecipeRatingsSimpleStats(strBeerName,
                                            out lRecipeRatingsCount,
                                            out dRecipeRatingsSum,
                                            out dRecipeRatingsAvg);

                //determine an appropriate constant value for Bayesian average
                double dPropConstant = 10; //just use 10 for now, tweak if necessary

                //return Bayesian average using derived parameters
                return GetBayesianAvg(dRecipesAvgRating, dRecipeRatingsSum, dPropConstant, lRecipeRatingsCount);
            }
        }

        /// <summary>
        /// Returns simple statistics about the Ratings associated with the supplied Recipe name.
        /// </summary>
        /// <param name="strRecipeName"></param>
        /// <param name="lRatingsCount"></param>
        /// <param name="dRatingsSum"></param>
        /// <param name="dAvgRating"></param>
        public static void GetRecipeRatingsSimpleStats(string strRecipeName, 
                                                       out long lRatingsCount, 
                                                       out double dRatingsSum, 
                                                       out double dAvgRating)
        {
            using (var context = new Models.ModelsContext())
            {
                //retrieve table of all ratings scores for this recipe
                var vRatingScores = from r in context.Ratings
                                    where r.Recipe_Name == strRecipeName
                                    select r.Rating_Score;

                //determine sum of individual ratings for this recipe
                dRatingsSum = 0;
                foreach (var vRatingScore in vRatingScores)
                    dRatingsSum += (double)vRatingScore;

                //set other out variables for return
                lRatingsCount = vRatingScores.Count();
                if (lRatingsCount == 0)
                    dAvgRating = 0;
                else
                    dAvgRating = dRatingsSum / lRatingsCount;
            }
        }

        /// <summary>
        /// Calculates the Bayesian average for the data items with the supplied
        /// statistical parameters.
        /// </summary>
        /// <param name="dPriorMean"></param>
        /// <param name="dSumOfValues"></param>
        /// <param name="dC"></param>
        /// <param name="dValueCount"></param>
        /// <returns></returns>
        public static double GetBayesianAvg(double dPriorMean,
                                            double dSumOfValues,
                                            double dC,
                                            double dValueCount)
        {
            return (dC * dPriorMean + dSumOfValues) / (dC + dValueCount);
        }

        /// <summary>
        /// Calculates the absolute deviation of the supplied data items using the supplied
        /// mean value for the data items.
        /// </summary>
        /// <param name="lItemCount"></param>
        /// <param name="dItemAvg"></param>
        /// <param name="oDataItems"></param>
        /// <returns></returns>
        public static double GetAbsDev(double dItemMean, List<double> oDataItems)
        {
            double dItemSummation = 0;
            foreach (double dItem in oDataItems)
                dItemSummation += Math.Abs(dItem - dItemMean);
            return dItemSummation / oDataItems.Count;
        }
    }
}