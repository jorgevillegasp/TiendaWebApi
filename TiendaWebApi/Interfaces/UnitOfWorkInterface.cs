using Microsoft.AspNetCore.DataProtection.Repositories;

namespace TiendaWebApi.Interfaces;

public interface UnitOfWorkInterface
{
    ProductoInterface Productos { get; }
    MarcaInterface Marcas { get; }
    CategoriaInterface Categorias { get; }
    RolInterface Roles { get; }
    UsuarioInterface Usuarios { get; }
    Task<int> SaveAsync();
}
