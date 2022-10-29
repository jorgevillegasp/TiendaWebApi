using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoInterface _productoInterface;
        private readonly UnitOfWorkInterface _unitOfWork;

        public ProductoController(UnitOfWorkInterface unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response _resp = new Response();
            try
            {
                _resp.Data = await _unitOfWork.Productos.GetAllAsync();
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
