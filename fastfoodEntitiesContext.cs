using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace WpfApp2
{

    class fastfoodEntitiesContext : DbContext
    {
        public fastfoodEntitiesContext() : base("fastfoodEntities")
        { }
        public DbSet<Ingredient> Ingredient { get; set; }
    }
}
