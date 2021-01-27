using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebApi.Dtos;

namespace ProAgil.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public PalestranteController(IProAgilRepository _repo, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._repo = _repo;

        }

        //MÉTODO GET
        [HttpGet]
        public async Task<IActionResult> GetAllPalestrantesAsync()
        {
            try
            {
                var palestrante = await _repo.GetAllPalestrantesAsync(true);
                var result = _mapper.Map<PalestranteDto[]>(palestrante);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("nome/{PalestranteNome}")]
        public async Task<IActionResult> GetPalestranteByNome(string PalestranteNome)
        {
            try
            {
                var palestrante = await _repo.GetPalestrantesAsyncByName(PalestranteNome, true);
                var result = _mapper.Map<PalestranteDto[]>(palestrante);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        //MÉTODO GET-ID
        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> GetPalestranteById(int PalestranteId)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsyncById(PalestranteId, true);
                var result = _mapper.Map<PalestranteDto>(palestrante);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        //MÉTODO POST
        [HttpPost]
        public async Task<IActionResult> Post(PalestranteDto model)
        {
            try
            {
                var result = _mapper.Map<Palestrante>(model);
                _repo.Add(result);
                if (await _repo.SaveChangesAsync()) return Ok(_mapper.Map<Palestrante>(result));

            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

        //MÉTODO UPDATE
        [HttpPut("{PalestranteId}")]
        public async Task<IActionResult> Put(int PalestranteId, PalestranteDto model)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsyncById(PalestranteId, false);
                if (palestrante == null) return NotFound();
                _mapper.Map(model, palestrante);

                _repo.Update(palestrante);
                if (await _repo.SaveChangesAsync()) return Ok(_mapper.Map<PalestranteDto>(palestrante));
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }
            return BadRequest();
        }

        //MÉTODO DELETE
        [HttpDelete("{PalestranteId}")]
        public async Task<IActionResult> delete(int PalestranteId)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsyncById(PalestranteId, false);
                if (palestrante == null) return NotFound();

                _repo.Delete(palestrante);
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