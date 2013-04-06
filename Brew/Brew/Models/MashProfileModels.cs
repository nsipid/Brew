using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("MashProfile")]
    public class MashProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        public string Name { get; set; }
        [Required]
        public float GrainTemp { get; set; } // The temperature of the grain before adding it to the mash in degrees Celsius.
        public virtual ICollection<MashStep> Steps { get; set; }
        public string Notes { get; set; }
        public float TunTemp { get; set; } // Grain tun temperature – may be used to adjust the infusion temperature for equipment if the program supports it.  Measured in degrees C.
        public float SpargeTemp { get; set; } // Temperature of the sparge water used in degrees Celsius.
        public float PH { get; set; } // The PH of the sparge.
        public float TunWeight { get; set; } // Weight of the mash tun in kilograms
        public float TunSpecificHeat { get; set; } // Specific heat of the tun material in calories per gram-degree C.
        public bool EquipAdjust { get; set; } // If TRUE, mash infusion and decoction calculations should take into account the temperature effects of the equipment (tun specific heat and tun weight).  If FALSE, the tun is assumed to be pre-heated. 

        public MashProfile()
        {
            Steps = new HashSet<MashStep>();            
        }
    }
}