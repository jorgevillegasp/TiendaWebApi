using Infrastructure.Repositories;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;


namespace TiendaWebApi.Services
{
    public class MarcaService : GenericService<Marca>, MarcaInterface
        {
        private TiendaWebApiContext context;

        public MarcaService(TiendaWebApiContext context) : base(context)
        {
            this.context = context;
        }
    }
}
