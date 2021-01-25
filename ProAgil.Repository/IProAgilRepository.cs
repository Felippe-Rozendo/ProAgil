using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //GERAL
         void Add<T>(T entity)where T : class;
         void Update<T>(T entity)where T : class;
         void Delete<T>(T entity)where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTO
         Task<Evento[]> GetAllEventosAsyncByTema(string Tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);

         //PALESTRANTE
         Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
         Task<Palestrante[]> GetPalestrantesAsyncByName(string Name, bool includeEventos);
         Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos);
    }
}