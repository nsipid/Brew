using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class FermentablesViewModel
    {
        public readonly PagedList<FermentableViewModel> Fermentables;
               
        public FermentablesViewModel(uint pageNo)
        {
            var allFermentables = new List<FermentableViewModel>();
            using (var context = new Models.UsersContext())
            {
                var dbModel = (from r in context.Fermentables select r);
                foreach (var fermentable in dbModel)
                {
                    allFermentables.Add(new FermentableViewModel
                    {
                        Amount = fermentable.Amount.ToString(),
                        Color = fermentable.Color.ToString(),
                        AddedAfterBoiling = fermentable.AddAfterBoil.ToString(),
                        Yield = fermentable.Yield,
                        CourseGrainYield = fermentable.CoarseFineDiff                       
                    });
                }
            }

            Fermentables = new PagedList<FermentableViewModel>(allFermentables.GetRange((int)(pageNo * 30), (int)Math.Min(allFermentables.Count, pageNo * 30 + 30)), allFermentables.Count < 30 ? 0 : (uint)Math.Ceiling(allFermentables.Count / 30d), pageNo, 30);
        }
    }
}