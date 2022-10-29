using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Services
{

    public class UsuarioService : GenericService<Usuario>, UsuarioInterface
    {
        public UsuarioService(TiendaWebApiContext context) : base(context)
        {
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
