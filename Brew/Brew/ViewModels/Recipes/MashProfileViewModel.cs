using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Recipes
{
    public class MashProfileViewModel
    {
        public int UID { get; set; }
        [StringLength(75)]
        public string Name { get; set; }
        [Required, Range(-50, 110)]
        public float GrainTemp { get; set; } // The temperature of the grain before adding it to the mash in degrees Celsius.
        public virtual List<MashStepViewModel> Steps { get; set; }
        public string Notes { get; set; }
        [Range(-50, 110)]
        public float TunTemp { get; set; } // Grain tun temperature – may be used to adjust the infusion temperature for equipment if the program supports it.  Measured in degrees C.
        [Range(-50, 110)]
        public float SpargeTemp { get; set; } // Temperature of the sparge water used in degrees Celsius.
        [Range(-1, 11.5)] // battery acid to ammonia 
        public float PH { get; set; } // The PH of the sparge.
        public float TunWeight { get; set; } // Weight of the mash tun in kilograms
        public float TunSpecificHeat { get; set; } // Specific heat of the tun material in calories per gram-degree C.
        public bool EquipAdjust { get; set; } // If TRUE, mash infusion and decoction calculations should take into account the temperature effects of the equipment (tun specific heat and tun weight).  If FALSE, the tun is assumed to be pre-heated. 

        public MashProfileViewModel()
        {
            Steps = new List<MashStepViewModel>();
        }
    }
}