using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("MashStepType")]
    public class MashStepType
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    //A mash step is an internal record used within a mash profile to denote a separate step in a multi-step mash. 
    [Table("MashStep")]
    public class MashStep
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [StringLength(75)]        
        public string Name { get; set; }
        //[Required]
        public MashStepType MashStepType { get; set; }
        public float InfuseAmount { get; set; } // Volume (liters) The volume of water in liters to infuse in this step
        [Required, Range(-50, 110)] 
        public float StepTemp { get; set; } // The target temperature for this step in degrees Celsius.
        [Required]
        public float StepTime { get; set; } // Time in Minutes The number of minutes to spend at this step
        public float RampTime { get; set; } // Time in Minutes to achieve the desired step temperature         
        [Range(-50, 110)] 
        public float EndTemp { get; set; } // Temperature you can expect the mash to fall to after a long mash step.  Measured in degrees Celsius.
        [Range(-50, 110)]
        public float InfuseTemp { get; set; } // The calculated infusion temperature
        public float DecoctionAmount { get; set; } // Volume of mash to decoct
        [Required]
        public int SequenceNumber { get; set; }

        [ForeignKey("MashStepType")]
        public string MashStepType_Name { get; set; }
    }
}