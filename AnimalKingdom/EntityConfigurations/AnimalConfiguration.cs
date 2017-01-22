using AnimalKingdom.Models;
using System.Data.Entity.ModelConfiguration;

namespace AnimalKingdom.DBContext
{
    public class AnimalConfiguration : EntityTypeConfiguration<Animal>
    {
        public AnimalConfiguration()
        {
            HasKey(t => t.AnimalId);
            Property(t => t.Name).IsRequired().HasMaxLength(256);
            Property(t => t.BirthYear).IsRequired();
            Property(t => t.Created).HasColumnType("datetime2");
            HasOptional(t => t.Species).WithMany(t => t.Animals).HasForeignKey(t => t.SpeciesId);
        }
    }
}