using AnimalKingdom.DataBaseContext;
using AnimalKingdom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AnimalKingdom.DatabaseContext
{
    public class AnimalInitializer : CreateDatabaseIfNotExists<AnimalContext>, IDatabaseInitializer<AnimalContext>
    {
        protected override void Seed(AnimalContext context)
        {
            var animals = new List<Animal>(){
                new Animal() {
                    AnimalId = 1,
                    Name = "kiisu",
                    Created = DateTime.Now,
                    BirthYear = 2016,
                    Species = new Species()
                    {
                        Name="kass"
                    }
                },
                 new Animal() {
                    AnimalId = 2,
                    Name = "miisu",
                    Created = DateTime.Now,
                    BirthYear = 2016,
                    Species = new Species()
                    {
                        Name="koer"
                    }
                },
                  new Animal() {
                     AnimalId = 3,
                    Name = "kurrmurr",
                    Created = DateTime.Now,
                    BirthYear = 2016,
                    Species = new Species()
                    {
                        Name="kala"
                    }
                },
            };
            context.Animals.AddRange(animals);
            base.Seed(context);
        }
    }
}