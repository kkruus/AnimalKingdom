using System.Collections.Generic;

namespace AnimalKingdom.Models
{
    public class Species
    {
        public int SpeciesId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
    }
}