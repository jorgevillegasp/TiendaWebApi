using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Interfaces
{

    public interface UsuarioInterface : GenericInterface<Usuario>
    {
        Task<Usuario> GetByUsernameAsync(string username);
    }
}
