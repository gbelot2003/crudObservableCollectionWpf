using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCompras2.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
    }
}