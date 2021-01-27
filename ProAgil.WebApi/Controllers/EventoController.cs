using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;

        }

        //MÉTODO GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllEventosAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
        }

        //MÉTODO GET
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, true);
                return Ok(evento);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("tema/{EventoTema}")]
        public async Task<IActionResult> GetByTema(string EventoTema)
        {
            try
            {
                var evento = await _repo.GetAllEventosAsyncByTema(EventoTema, true);
                return Ok(evento);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()) return Ok(model);

            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

        
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(eventoId, false);
                if(evento == null) return NotFound();

                _repo.Update(model);
                if(await _repo.SaveChangesAsync()) return Ok(model);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();
                
                _repo.Delete(evento);
                if(await _repo.SaveChangesAsync()) return Ok("Excluido com sucesso!");
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

    }
}