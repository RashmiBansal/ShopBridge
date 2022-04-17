using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShopBridge.Models;

namespace ShopBridge.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        }
                
        public DbSet<Product> Products { get; set; }
    }
}