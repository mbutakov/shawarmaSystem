using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using ShawarmaManager.database;

namespace ShawarmaManager
{

    class fastfoodEntitiesContext : DbContext
    {
        public fastfoodEntitiesContext() : base("fastfoodEntities")
        { }
        public DbSet<Ingredient> Ingredient { get; set; }
    }
}
