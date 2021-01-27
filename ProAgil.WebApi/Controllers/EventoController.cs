using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebApi.Dtos;

namespace ProAgil.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        //MÉTODO GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventosAsync(true);
                var result = _mapper.Map<EventoDto[]>(eventos);
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
                var result = _mapper.Map<EventoDto>(evento);
                return Ok(result);
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
                var result = _mapper.Map<EventoDto[]>(evento);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {                
                 var result = _mapper.Map<Evento>(model);
                _repo.Add(result);
                if (await _repo.SaveChangesAsync()) return Ok(_mapper.Map<EventoDto>(result));

            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }


        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();
                _mapper.Map(model, evento);

                _repo.Update(evento);
                if (await _repo.SaveChangesAsync()) return Ok(_mapper.Map<EventoDto>(evento));
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
                if (evento == null) return NotFound();

                _repo.Delete(evento);
                if (await _repo.SaveChangesAsync()) return Ok("Excluido com sucesso!");
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

    }
}