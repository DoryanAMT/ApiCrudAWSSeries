using ApiCrudAWSSeries.Data;
using ApiCrudAWSSeries.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiCrudAWSSeries.Repositories
{
    public class RepositorySeries
    {
        private SeriesContext context;
        public RepositorySeries(SeriesContext context)
        {
            this.context = context;
        }
        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await this.context.Series.ToListAsync();
        }
        public async Task<Serie> FindSerieAsync
            (int idSerie)
        {
            return await this.context.Series
                .FirstOrDefaultAsync(x => x.IdSerie == idSerie);
        }

        private async Task<int> GetMaxIdSeriesAsync()
        {
            return await this.context.Series.MaxAsync(x => x.IdSerie);
        }
        public async Task CreateSerieAsync
            (string nombre, string imagen, int anyo)
        {
            Serie serie = new Serie()
            {
                IdSerie = await this.GetMaxIdSeriesAsync(),
                Nombre = nombre,
                Imagen = imagen,
                Anyo = anyo
            };
            await this.context.Series.AddAsync(serie);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync
            (int idSerie, string nombre, string imagen, int anyo)
        {
            Serie serie = await this.FindSerieAsync(idSerie);
            serie.Nombre = nombre;
            serie.Imagen = imagen;
            serie.Anyo = anyo;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteSeriesAsync
            (int idSerie)
        {
            Serie serie = await this.FindSerieAsync(idSerie);
            this.context.Remove(serie);
            await this.context.SaveChangesAsync();
        }
    }
}
