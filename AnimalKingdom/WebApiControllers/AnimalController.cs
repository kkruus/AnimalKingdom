using AnimalKingdom.DataBaseContext;
using AnimalKingdom.Models;
using AnimalKingdom.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AnimalKingdom.WebApiControllers
{
    public class AnimalController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAnimals()
        {
            using (var context = new AnimalContext())
            {
                List<AnimalDTO> animals = context.Animals.Where(x => x.IsDeleted == false).Select(x => new AnimalDTO()
                {
                    AnimalId = x.AnimalId,
                    BirthYear = x.BirthYear,
                    Created = x.Created,
                    IsDeleted = x.IsDeleted,
                    Name = x.Name,
                    SpeciesId = x.SpeciesId
                }).ToList();
                if (animals == null)
                {
                    return BadRequest("Sellisest liigist loomasid ei ole");
                }
                return Ok(animals);
            }
        }

        [HttpPost]
        public IHttpActionResult AddOrUpdateAnimal([FromBody]Animal animal)
        {
            if (animal == null)
            {
                return BadRequest();
            }
            if (animal.BirthYear > DateTime.Now.Year)
            {
                return BadRequest("Looma sünniaeg ei saa olla tulevikus");
            }
            if (animal.SpeciesId <= 0)
            {
                return BadRequest();
            }
            using (var context = new AnimalContext())
            {
                //Kontrollime, kas tegu on uue või olemasoleva loomaga
                Animal updateAnimal = context.Animals.Where(x => x.AnimalId == animal.AnimalId && x.IsDeleted == false).FirstOrDefault();

                //Samast liigist ja sama nimega loomade kontrollimine
                var validationQuery = context.Animals.Where(x => x.Name == animal.Name && x.SpeciesId == animal.SpeciesId);
                if (updateAnimal != null)
                {
                    validationQuery = validationQuery.Where(x => x.Name != updateAnimal.Name);
                }
                List<Animal> sameSpeciesAnimals = validationQuery.ToList();
                if (sameSpeciesAnimals.Count > 0)
                {
                    return BadRequest("Sama liigist loomadel ei tohi olla sama nimi");
                }

                if (updateAnimal == null)
                {
                    animal.Created = DateTime.Now;
                    context.Animals.Add(animal);
                }
                else
                {
                    updateAnimal.Name = animal.Name;
                    updateAnimal.SpeciesId = animal.SpeciesId;
                    updateAnimal.BirthYear = animal.BirthYear;
                }
                context.SaveChanges();
                AnimalDTO savedAnimal = context.Animals.Where(x => x.AnimalId == animal.AnimalId).Select(x => new AnimalDTO()
                {
                    AnimalId = x.AnimalId,
                    BirthYear = x.BirthYear,
                    Created = x.Created,
                    IsDeleted = x.IsDeleted,
                    Name = x.Name,
                    SpeciesId = x.SpeciesId
                }).FirstOrDefault();
                return Ok(savedAnimal);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteAnimal(int animalId)
        {
            using (var context = new AnimalContext())
            {
                Animal animal = context.Animals.Where(x => x.AnimalId == animalId).FirstOrDefault();
                if (animal != null)
                {
                    animal.IsDeleted = true;
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest("Sellist looma ei leitud");
            }
        }
    }
}