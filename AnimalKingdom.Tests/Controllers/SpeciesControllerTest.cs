using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalKingdom.WebApiControllers;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using AnimalKingdom.Models.DTO;

namespace AnimalKingdom.Tests.Controllers
{
    [TestClass]
    public class SpeciesControllerTest
    {
        [TestMethod]
        public void GetAllSpecies_ShouldReturnAllSpecies()
        {
            var controller = new SpeciesController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var response = controller.GetSpecies().ExecuteAsync(CancellationToken.None);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            List<SpeciesDTO> species;
            Assert.IsTrue(response.Result.TryGetContentValue<List<SpeciesDTO>>(out species));
            Assert.IsNotNull(species);
        }
    }
}
