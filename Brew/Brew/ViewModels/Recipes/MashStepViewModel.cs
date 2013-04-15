using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Recipes
{
    public class MashStepViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float InfuseAmountLiters { get; set; } 
        public float StepTempCel { get; set; } 
        public TimeSpan StepTimeMin { get; set; } 
        public float EndTempCel { get; set; } 
        public float InfuseTempCel { get; set; } 
        public float DecoctionAmount { get; set; }
        public int SequenceNumber { get; set; }
    } 
}