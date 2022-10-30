using TiendaWebApi.Dtos;
using TiendaWebApi.Models;

namespace TiendaWebApi.Interfaces
{

    public interface UsuarioInterface : GenericInterface<Usuario>
    {
        Task<Usuario> GetByUsernameAsync(string username);

        //los que estaban en Services dentro de API


        
        Task<string> RegisterAsync(RegisterDto model);
        Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);

        Task<string> AddRoleAsync(AddRoleDto model);
        
    }
}
