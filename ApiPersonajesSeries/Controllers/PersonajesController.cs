using ApiPersonajesSeries.Models;
using ApiPersonajesSeries.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesSeries.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }

        [HttpGet]
        [Route("PersonajesSeries/{serie}")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajesSerie(string serie)
        {
            return await this.repo.GetPersonajesSerieAsync(serie);
        }

        [HttpGet]
        [Route("Series")]
        public async Task<ActionResult<List<string>>> GetSeries()
        {
            return await this.repo.GetSeriesAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Personaje>> FindPersonaje(int id)
        {
            return await this.repo.FindPersonajeAsync(id);
        }

        [HttpPost]
        [Route("InsertPersonaje")]
        public async Task<ActionResult> InsertPersonaje(Personaje personaje)
        {
            await this.repo.CreatePersonajeAsync(personaje);
            return Ok();
        }

        [HttpPut]
        [Route("UpdatePersonaje")]
        public async Task<ActionResult> UpdatePersonaje(Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync(personaje);
            return Ok();
        }

        [HttpDelete]
        [Route("DeletePersonaje/{id}")]
        public async Task<ActionResult> DeletePersonaje(int id)
        {
            if (await this.repo.FindPersonajeAsync(id) == null)
                return NotFound();
            await this.repo.DeletePersonajeAsync(id);
            return Ok();
        }
    }
}
