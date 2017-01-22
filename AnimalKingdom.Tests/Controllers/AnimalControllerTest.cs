using AnimalKingdom.Models;
using AnimalKingdom.Models.DTO;
using AnimalKingdom.WebApiControllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace AnimalKingdom.Tests.Controllers
{
    [TestClass]
    public class AnimalControllerTest
    {
        [TestMethod]
        public void GetAllAnimals_ShouldReturnAllAnimals()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = controller.GetAnimals().ExecuteAsync(CancellationToken.None);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            List<AnimalDTO> animals;
            Assert.IsTrue(response.Result.TryGetContentValue<List<AnimalDTO>>(out animals));
            Assert.IsNotNull(animals);
        }

        [TestMethod]
        public void PostAnimal_ShouldReturnAnimal()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            Animal newAnimal = new Animal()
            {
                Name = DateTime.Now.ToString(),
                Created = DateTime.Now,
                BirthYear = 2016,
                SpeciesId = 2
            };

            var response = controller.AddOrUpdateAnimal(newAnimal).ExecuteAsync(CancellationToken.None);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            AnimalDTO animal;
            Assert.IsTrue(response.Result.TryGetContentValue<AnimalDTO>(out animal));
            Assert.IsNotNull(animal);
        }

        [TestMethod]
        public void PostAnimalWithInvalidFields_ShouldReturnBadRequest()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            Animal newAnimal = new Animal()
            {
                Name = DateTime.Now.ToString(),//alati uus nimi
                Created = DateTime.Now,
                BirthYear = DateTime.Now.Year + 5,
                SpeciesId = 2
            };
            
            var invalidBirthResponse = controller.AddOrUpdateAnimal(newAnimal).ExecuteAsync(CancellationToken.None);
            Assert.AreEqual(HttpStatusCode.BadRequest, invalidBirthResponse.Result.StatusCode);

            newAnimal.BirthYear = DateTime.Now.Year;
            newAnimal.SpeciesId = -1;
            var invalidSpeciesIdResponse = controller.AddOrUpdateAnimal(newAnimal).ExecuteAsync(CancellationToken.None);
            Assert.AreEqual(HttpStatusCode.BadRequest, invalidSpeciesIdResponse.Result.StatusCode);
        }

        [TestMethod]
        public void UpdateAnimal_FieldsShouldUpdate()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = controller.GetAnimals().ExecuteAsync(CancellationToken.None);

            List<AnimalDTO> animals;
            var content = response.Result.TryGetContentValue<List<AnimalDTO>>(out animals);
            //Uuendame esimest looma baasis
            AnimalDTO newAnimal = animals.First();
            Animal updateAnimal = new Animal()
            {
                AnimalId = newAnimal.AnimalId,
                BirthYear = newAnimal.BirthYear - 3,
                Name = DateTime.Now.ToString(),
                Created = newAnimal.Created,
                IsDeleted = newAnimal.IsDeleted,
                SpeciesId = 3
            };

            var updateResponse = controller.AddOrUpdateAnimal(updateAnimal).ExecuteAsync(CancellationToken.None);
            AnimalDTO returnedAnimal;
            Assert.IsTrue(updateResponse.Result.TryGetContentValue<AnimalDTO>(out returnedAnimal));
            Assert.IsNotNull(returnedAnimal);
            Assert.AreEqual(returnedAnimal.Name, updateAnimal.Name);
            Assert.AreEqual(returnedAnimal.BirthYear, updateAnimal.BirthYear);
            Assert.AreEqual(returnedAnimal.SpeciesId, updateAnimal.SpeciesId);
        }

        [TestMethod]
        public void AddAnimalSameNameAndSpecies_ShouldReturnBadRequest()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var response = controller.GetAnimals().ExecuteAsync(CancellationToken.None);

            List<AnimalDTO> animals;
            var content = response.Result.TryGetContentValue<List<AnimalDTO>>(out animals);

            AnimalDTO firstAnimal = animals.First();
            Animal newAnimal = new Animal()
            {
                BirthYear = firstAnimal.BirthYear,
                Name = firstAnimal.Name,
                Created = firstAnimal.Created,
                IsDeleted = firstAnimal.IsDeleted,
                SpeciesId = firstAnimal.SpeciesId
            };

            var updateResponse = controller.AddOrUpdateAnimal(newAnimal).ExecuteAsync(CancellationToken.None);
            Assert.IsNotNull(updateResponse);
            Assert.IsTrue(updateResponse.IsCompleted);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateResponse.Result.StatusCode);
        }

        [TestMethod]
        public void DeleteAnimal_ShouldRemoveAnimalFromAnimalList()
        {
            var controller = new AnimalController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var response = controller.GetAnimals().ExecuteAsync(CancellationToken.None);
            List<AnimalDTO> animals;

            var content = response.Result.TryGetContentValue<List<AnimalDTO>>(out animals);
            Assert.IsTrue(animals.Count > 0);
            int deleteableAnimalId = animals.First().AnimalId;
            var updateResponse = controller.DeleteAnimal(deleteableAnimalId).ExecuteAsync(CancellationToken.None);
            Assert.IsTrue(updateResponse.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            //Testime, et loom kustutati nimekirjast
            var testResponse = controller.GetAnimals().ExecuteAsync(CancellationToken.None);
            List<AnimalDTO> animalTestSet;
            testResponse.Result.TryGetContentValue<List<AnimalDTO>>(out animalTestSet);
            Assert.IsNotNull(animalTestSet);
            var removedAnimalsInTestSet = animalTestSet.Where(x => x.AnimalId == deleteableAnimalId).ToList();
            Assert.AreEqual(removedAnimalsInTestSet.Count(), 0);
        }
    }
}
