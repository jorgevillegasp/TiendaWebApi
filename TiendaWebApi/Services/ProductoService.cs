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

        public async Task<IEnumerable<Producto>> GetProductosMasCaros(int cantidad) =>
                    await _context.Productos
                        .OrderByDescending(p => p.Precio)
                        .Take(cantidad)
                        .ToListAsync();

        public override async Task<Producto> GetByIdAsync(int id)
        {
            return await _context.Productos
                            .Include(p => p.Marca)
                            .Include(p => p.Categoria)
                            .FirstOrDefaultAsync(p => p.Id == id);

        }

        public override async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos
                                .Include(u => u.Marca)
                                .Include(u => u.Categoria)
                                .ToListAsync();
        }

    }
}
