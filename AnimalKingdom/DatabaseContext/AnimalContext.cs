using AnimalKingdom.DatabaseContext;
using AnimalKingdom.Models;
using System.Data.Entity;
using System.Reflection;

namespace AnimalKingdom.DataBaseContext
{
    public class AnimalContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Species> Species { get; set; }
        public AnimalContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new AnimalInitializer());
            Database.Initialize(true);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}