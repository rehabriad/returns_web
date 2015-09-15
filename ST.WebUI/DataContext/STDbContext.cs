using ST.Entity;
using System.Data.Entity;

namespace ST.WebUI.DataContext
{
    public class STDbContext : DbContext
    {
        public STDbContext()
            : base ("name=DefaultConnection")
        {
        }

        public DbSet<Returns> Returns { get; set; }

        public DbSet<Retsale> Retsales { get; set; }

        public DbSet<Retpurch> Retpurches { get; set; }
        public DbSet<RetCapital> Retcapitals { get; set; }
        public DbSet<RetReceit> RetReceits { get; set; }
        public DbSet<RetLocalPurch> RetLocalPurches { get; set; }
        public DbSet<RetExPurch> RetExPurches { get; set; }

        public DbSet<Master> Masters { get; set; }
    }
}