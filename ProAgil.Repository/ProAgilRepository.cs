using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //EVENTO
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Evento> query = _context.Eventos
                    .Include(c => c.Lotes)
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //ORDENANDO DO DATA MAIS ANTIGA PRA DATA MAIS NOVA
            query = query.AsNoTracking().OrderBy(c => c.Id);

            return await query.ToArrayAsync(); 

        }
        public async Task<Evento[]> GetAllEventosAsyncByTema(string Tema, bool includePalestrantes = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Evento> query = _context.Eventos
                    .Include(c => c.Lotes)
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //ORDENANDO DO DATA MAIS ANTIGA PRA DATA MAIS NOVA
            //E VERIFICANDO QUAIS TEMAS CONTEM NO EVENTO            
            //E PARA EVITAR QUALQUER TIPO DE ERRO ESTÁ TRANSFORMANDO A STRING EM MINÚNCULO
            query = query.AsNoTracking()
                    .Where(e => e.Tema.ToLower().Contains(Tema.ToLower()) );

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Evento> query = _context.Eventos
                    .Include(c => c.Lotes)
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //ORDENANDO DO DATA MAIS ANTIGA PRA DATA MAIS NOVA
            //E VERIFICANDO QUAL ID Q FOI REQUISITADO É IGUAL AO DO BANCO DE DADOS
            query = query.AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Where(e => e.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //PALESTRANTE
       
        public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includeEventos){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Evento);
            }

            //ORDENANDO POR ID
            //E VERIFICANDO QUAL ID É IGUAL AO ID DO PALESTRANTE EXISTENTE
            query = query.AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Where(c => c.Id == PalestranteId );

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includeEventos){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Evento);
            }

            //ORDENANDO POR ID
            query = query.AsNoTracking()
                         .OrderBy( c => c.Id );

            return await query.ToArrayAsync();    
        
        }

        public async Task<Palestrante[]> GetPalestrantesAsyncByName(string Name, bool includeEventos = false)
        {
            //FAZENDO A BUSCA NO BANCO DE DADOS
            IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(c => c.RedesSociais);

            //FAZENDO O TRATAMENTO DA OPÇÃO DE PALESTRANTES
            //MÉTODO NECESSÁRIO PARA NN FICAR CUSTOSO(PERFORMATICAMENTE) PRO BANCO DE DADOS
            if(includeEventos){
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Evento);
            }

            //ORDENANDO POR NOME
            //E VERIFICANDO QUAL NOME É IGUAL AO NOME DO PALESTRANTE EXISTENTE
            //E PARA EVITAR QUALQUER TIPO DE ERRO ESTÁ TRANSFORMANDO A STRING EM MINÚNCULO
            query = query.AsNoTracking()
                         .OrderBy(n => n.Nome)
                         .Where(p => p.Nome.ToLower() == Name.ToLower());

            return await query.ToArrayAsync();            
        }

    }
}