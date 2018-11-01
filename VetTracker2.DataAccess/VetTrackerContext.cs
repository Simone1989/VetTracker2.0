using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using VetTracker2.Model;

namespace VetTracker2.DataAccess
{
    public class VetTrackerContext : DbContext
    {
        public VetTrackerContext() : base("VetTrackerDb")
        {

        }

        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
