using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Model
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        [ForeignKey("VehicleTypeID")]
        public VehicleType VehicleType { get; set; }
        public int VehicleTypeID { get; set; }
        public virtual ICollection<VehicleOwner> VehicleOwners { get; set; }
    }
}
