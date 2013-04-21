using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.Utilities
{
    public class StatisticsUtils
    {
        public static Dictionary<string, double> GetLocalAvg()
        {
            using (var context = new Models.ModelsContext())
            {
                //retrieve table of all ratings scores for this recipe
                var vRatingScores = from r in context.Ratings
                                    group r by r.Recipe_Name into g
                                    select new { Name = g.Key, RatingSum = g.Sum(s => s.Rating_Score), Count = g.Count(), RatingAverage = g.Average(s => s.Rating_Score) };

                return vRatingScores.ToList().ToDictionary(g => g.Name, g => g.RatingAverage);
            }
        }

        public static Dictionary<string, double> GetSiteAvg()
        {
            Dictionary<string, long> oFlavorCounts = new Dictionary<string, long>();
            using (var context = new Models.ModelsContext())
            {
                //retrieve table of all ratings scores for this recipe
                var vRatingScores = from r in context.Ratings
                                    group r by r.Recipe_Name into g
                                    select new { Name = g.Key, RatingSum = g.Sum(s => s.Rating_Score), Count = g.Count(), RatingAverage = g.Average(s=>s.Rating_Score) };

                double dRecipesAvgRatingsSum = vRatingScores.Sum(s => s.RatingAverage);

                double dRecipesAvgRating = dRecipesAvgRatingsSum / vRatingScores.Count();

                //determine an appropriate constant value for Bayesian average
                double dPropConstant = 10; //just use 10 for now, tweak if necessary

                //return Bayesian average using derived parameters
                return vRatingScores.ToList().ToDictionary(g => g.Name, g => GetBayesianAvg(dRecipesAvgRating, g.RatingSum, dPropConstant, vRatingScores.Count()));  
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