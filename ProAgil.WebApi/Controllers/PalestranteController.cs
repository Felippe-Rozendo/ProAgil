using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebApi.Controllers
{        
    [ApiController]
    [Route("/api/[controller]")]
    public class PalestranteController: ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository _repo)
        {
            this._repo = _repo;

        }

        //MÉTODO GET
        [HttpGet]
        public async Task<IActionResult> GetAllPalestrantesAsync()
        {
            try
            {
                var result = await _repo.GetAllPalestrantesAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
        }

        //MÉTODO GET-ID
        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> GetPalestranteById(int PalestranteId)
        {
            try
            {
                var result = await _repo.GetPalestranteAsyncById(PalestranteId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
        }

        //MÉTODO POST
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()) return Ok(model);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
            return BadRequest();
        }

        //MÉTODO UPDATE
        [HttpPut("{PalestranteId}")]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsyncById(PalestranteId, false);
                if(palestrante == null) return NotFound();

                _repo.Update(model);
                if(await _repo.SaveChangesAsync()) return Ok(model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
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
                if(palestrante == null) return NotFound();
                
                _repo.Delete(palestrante);
                if(await _repo.SaveChangesAsync()) return Ok("Excluido com sucesso!");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
            return BadRequest();
        }    

    }
}