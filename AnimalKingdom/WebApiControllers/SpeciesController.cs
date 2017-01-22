using AnimalKingdom.DataBaseContext;
using AnimalKingdom.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AnimalKingdom.WebApiControllers
{
    public class SpeciesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAnimals()
        {
            using (var context = new AnimalContext())
            {
                List<SpeciesDTO> species = context.Species.Select(x => new SpeciesDTO()
                {
                    SpeciesId = x.SpeciesId,
                    Name = x.Name
                }).ToList();

                if (species == null)
                {
                    return BadRequest("Loomade liigid puuduvad");
                }
                return Ok(species);
            }
        }
    }
}