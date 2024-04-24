using ApiPersonajesSeries.Data;
using ApiPersonajesSeries.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesSeries.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(string serie)
        {
            return await this.context.Personajes
                .Where(p => p.Serie == serie)
                .ToListAsync();
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            return await this.context.Personajes
                .Select(p => p.Serie)
                .Distinct().ToListAsync();
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            return await this.context.Personajes
                .FirstOrDefaultAsync(p => p.IdPersonaje == idpersonaje);
        }

        public async Task CreatePersonajeAsync(Personaje personaje)
        {
            if (await this.context.Personajes.CountAsync() == 0)
                personaje.IdPersonaje = 1;
            else
                personaje.IdPersonaje = await this.context.Personajes.MaxAsync(p => p.IdPersonaje) + 1;
            await this.context.Personajes.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            Personaje persEditar = await FindPersonajeAsync(personaje.IdPersonaje);
            persEditar.Nombre = personaje.Nombre;
            persEditar.Imagen = personaje.Imagen;
            persEditar.Serie = personaje.Serie;
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            Personaje personaje = await FindPersonajeAsync(idpersonaje);
            this.context.Personajes.Remove(personaje);
            await this.context.SaveChangesAsync();
        }
    }
}
