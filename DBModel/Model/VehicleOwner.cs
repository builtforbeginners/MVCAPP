using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Model
{
    public class VehicleOwner
    {
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public int OwnerID { get; set; }

        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("OwnerID")]
        public virtual Owner Owner { get; set; }
    }
}
