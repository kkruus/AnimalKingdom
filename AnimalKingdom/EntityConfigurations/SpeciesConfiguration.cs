using AnimalKingdom.Models;
using System.Data.Entity.ModelConfiguration;

namespace AnimalKingdom.EntityConfigurations
{
    public class SpeciesConfiguration : EntityTypeConfiguration<Species>
    {
        public SpeciesConfiguration()
        {
            HasKey(t => t.SpeciesId);
            Property(t => t.Name).IsRequired().HasMaxLength(256);
            HasMany(t => t.Animals).WithOptional(t => t.Species).HasForeignKey(t => t.SpeciesId);
        }
    }
}