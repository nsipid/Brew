using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class HopsViewModel
    {
        public readonly PagedList<HopViewModel> Hops;

        public HopsViewModel(uint pageNo)
        {
            var allHops = new List<HopViewModel>();
            using (var context = new Models.ModelsContext())
            {
                var dbModel = (from r in context.Hops select r);
                foreach (var hop in dbModel)
                {
                    allHops.Add(new HopViewModel
                    {
                        Type = hop.HopType.ToString(),
                        Amount = hop.Amount,
                        Alpha = hop.Alpha,
                        Beta = (double)hop.Beta,
                        PercentHumulene = (double)hop.Humulene,
                        PercentCaryophyllene = (double)hop.Caryophyllene,
                        PercentCohumulone = (double)hop.Cohumulone,
                        PercentMyrcene = (double)hop.Myrcene,
                        UsedDuring = -1,
                        UsageTime = TimeSpan.FromMilliseconds(hop.Time),
                        Form = hop.HopForm.ToString(),
                        Stability = (double)hop.HSI
                                             
                    });
                }
            }

            Hops = new PagedList<HopViewModel>(allHops.GetRange((int)(pageNo * 30), (int)Math.Min(allHops.Count, pageNo * 30 + 30)), allHops.Count < 30 ? 0 : (uint)Math.Ceiling(allHops.Count / 30d), pageNo, 30);
        }
    }
}