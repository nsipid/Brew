using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class MashStepUtils
    {
        public static MashStepType getMashStepType(string dbValue)
        {
            switch (dbValue)
            {
                case "Infusion":
                    return Models.MashStepType.Infusion;
                case "Temperature":
                    return Models.MashStepType.Temperature;
                case "Decoction":
                    return Models.MashStepType.Decoction;     
                default:
                    throw new Exception(dbValue + " is not a MashStepType");
            }
        }
    }

    public enum MashStepType
    {
        Infusion,
        Temperature,
        Decoction
    }

    //A mash step is an internal record used within a mash profile to denote a separate step in a multi-step mash. 
    [Table("MashStep")]
    public class MashStep
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        public string Name { get; set; }
        [Required]
        public MashStepType MashStepType { get; set; }
        public float InfuseAmount { get; set; } // The volume of water in liters to infuse in this step
        [Required]
        public float StepTemp { get; set; } // The target temperature for this step in degrees Celsius.
        [Required]
        public float StepTime { get; set; } // The number of minutes to spend at this step
        public float RampTime { get; set; } // Time in minutes to achieve the desired step temperature         
        public float EndTemp { get; set; } // Temperature you can expect the mash to fall to after a long mash step.  Measured in degrees Celsius.
        public float  InfuseTemp { get; set; }
        public float DecoctionAmount { get; set; }

        [Required]
        public int SequenceNumber { get; set; }
    }
}