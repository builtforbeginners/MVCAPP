using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.ViewModel
{
    public class VehicleOwnerViewModel
    {
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public int OwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleVIN { get; set; }
    }
}