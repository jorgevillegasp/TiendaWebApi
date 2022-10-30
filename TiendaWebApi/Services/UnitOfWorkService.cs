using TiendaWebApi.Repositories;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;

namespace TiendaWebApi.Services
{
    public class UnitOfWorkService : UnitOfWorkInterface, IDisposable
    {
        
        private readonly TiendaWebApiContext _context;
        private ProductoInterface _productos;
        private MarcaInterface _marcas;
        private CategoriaInterface _categorias;
        private RolInterface _rol;
        private UsuarioInterface _usuario;

        public UnitOfWorkService(TiendaWebApiContext context)
        {
            _context = context;
        }

        public CategoriaInterface Categorias
        {
            get
            {
                if (_categorias == null)
                {
                    _categorias = new CategoriaService(_context);
                }
                return _categorias;
            }
        }

        public MarcaInterface Marcas
        {
            get
            {
                if (_marcas == null)
                {
                    _marcas = new MarcaService(_context);
                }
                return _marcas;
            }
        }

        public ProductoInterface Productos
        {
            get
            {
                if (_productos == null)
                {
                    _productos = new ProductoService(_context);
                }
                return _productos;
            }
        }
        public UsuarioInterface Usuarios
        {
            get
            {
                if (_usuario == null)
                {
                    _usuario = new UsuarioService(_context);
                }
                return _usuario;
            }
        }

        public RolInterface Roles
        {
            get
            {
                if (_rol == null)
                {
                    _rol = new RolService(_context);
                }
                return _rol;
            }
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
