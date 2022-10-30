using TiendaWebApi.Repositories;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Services
{
    public class CategoriaService : GenericService<Categoria>, CategoriaInterface
    {
        private TiendaWebApiContext context;

        public CategoriaService(TiendaWebApiContext context) : base(context)
        {

        }
    }
}
