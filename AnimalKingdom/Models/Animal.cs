using System;

namespace AnimalKingdom.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; }
        public int SpeciesId { get; set; }
        public virtual Species Species { get; set; }        
    }
}