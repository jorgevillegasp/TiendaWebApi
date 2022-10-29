using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Services
{
    public class ProductoService : GenericService<Producto>, ProductoInterface
    {

        private readonly TiendaWebApiContext context;

        public ProductoService(TiendaWebApiContext context) : base(context)
        {
        }

        public async Task<List<Producto>> GetAll()
        {
            return await Task.FromResult(context.Productos.OrderByDescending(c => c.Id).ToList());
        }


    }
}
