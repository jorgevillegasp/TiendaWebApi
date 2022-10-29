using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Dtos;
using TiendaWebApi.Helpers;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaWebApi.Controllers
{

    [ApiVersion("1.0")]
    [ApiVersion("1.1")]


    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(UnitOfWorkInterface unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProductoListDto>>> Get([FromQuery] Params productParams)
        {
            var resultado = await _unitOfWork.Productos
                .GetAllAsync(
                    productParams.PageIndex, 
                    productParams.PageSize,
                    productParams.Search
             );

            //Mapeamos la lista del prodructo 
            var listaProductosDto = _mapper.Map<List<ProductoListDto>>(resultado.registros);

            Response.Headers.Add(
                "X-InlineCount", 
                resultado.totalRegistros.ToString()
            );
                

            return new Pager<ProductoListDto>(
                listaProductosDto, 
                resultado.totalRegistros,
                productParams.PageIndex, 
                productParams.PageSize, 
                productParams.Search 
            );

        }


        [HttpGet]
        [ApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get11()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();

            return _mapper.Map<List<ProductoDto>>(productos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            return _mapper.Map<ProductoDto>(producto);
        }

        //POST: api/Productos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoAddUpdateDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            _unitOfWork.Productos.Add(producto);
            await _unitOfWork.SaveAsync();

            if (producto == null)
            {
                return BadRequest();
            }
            productoDto.Id = producto.Id;
            return CreatedAtAction(nameof(Post), new { id = productoDto.Id }, productoDto);
        }


        //PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoAddUpdateDto>> Put(int id, [FromBody] ProductoAddUpdateDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var producto = _mapper.Map<Producto>(productoDto);
            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveAsync();
            return productoDto;
        }

        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            _unitOfWork.Productos.Remove(producto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
