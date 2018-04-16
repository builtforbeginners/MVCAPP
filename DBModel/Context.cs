using DBModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public class VehicleContext : DbContext
    {
        public static string connectionString = "Data Source=127.0.0.1\\Sense; Initial Catalog=Vehicle; Integrated Security=False; User Id=sa; Password=Sense17*; MultipleActiveResultSets=True";
        public VehicleContext(): base(connectionString)
        {

        }

        public VehicleContext GetContext()
        {
            return new VehicleContext();
        }

        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<VehicleOwner> VehicleOwners { get; set; }
    }
}
