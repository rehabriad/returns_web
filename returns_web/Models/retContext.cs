using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace returns_web.Models
{
    public class retContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public retContext() : base("name=retContext")
        {
        }

        public System.Data.Entity.DbSet<returns_web.Models.Returns> Returns { get; set; }

        public System.Data.Entity.DbSet<returns_web.Models.Retsale> Retsales { get; set; }

        public System.Data.Entity.DbSet<returns_web.Models.Retpurch> Retpurches { get; set; }

        public System.Data.Entity.DbSet<returns_web.Models.Master> Masters { get; set; }
    
    }
}
