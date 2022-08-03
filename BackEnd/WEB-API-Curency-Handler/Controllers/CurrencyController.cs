using Microsoft.AspNetCore.Mvc;
using WEB_API_Curency_Handler.Model;
using WEB_API_Curency_Handler.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_Curency_Handler.Controllers
{  
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private IGetJsonService _jsonService;
        public CurrencyController(IGetJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        // GET: api/<CurrencyController>
        [Route("сurrencies")]
        [HttpGet]
        public async Task<IActionResult> Get(int? pageSize, int? pageNumber){
         try
         {
                IEnumerable<Valuta> curencies = await _jsonService.GetDataValutesAsync();
                return Ok(curencies.OrderBy(x => x.Name).Skip((pageNumber ?? 1 - 1) * pageSize ?? 5).Take(pageSize ?? 5));
         } 
         catch(Exception ex)
         {
         return BadRequest(ex.Message); 
         }
        }

        // GET api/<CurrencyController>/5
        [HttpGet("сurrency/{id}")]
        public async Task<IActionResult> Currencies(string id)
        {
            try
            {
                var result = (await _jsonService.GetDataValutesAsync())
                    .First(x => x.ID == id);
                if (result != null)
                {
                    return Ok(result);
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
