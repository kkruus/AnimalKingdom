using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalKingdom.Models.DTO
{
    public class AnimalDTO
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; }
        public int SpeciesId { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - BirthYear;
            }
        }
    }
}