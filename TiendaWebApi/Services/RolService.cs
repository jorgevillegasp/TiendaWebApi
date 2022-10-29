using Infrastructure.Repositories;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Repositories;

public class RolService : GenericService<Rol>, RolInterface
{

    public RolService(TiendaWebApiContext context) : base(context)
    {
    }
}
