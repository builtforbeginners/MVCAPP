using DBModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public class ContextFactory : System.Data.Entity.DropCreateDatabaseIfModelChanges<VehicleContext>
    {
    }
}
