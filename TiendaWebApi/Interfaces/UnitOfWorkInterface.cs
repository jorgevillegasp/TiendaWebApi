namespace TiendaWebApi.Interfaces;

public interface UnitOfWorkInterface
{
    ProductoInterface Productos { get; }
    MarcaInterface Marcas { get; }
    CategoriaInterface Categorias { get; }
    Task<int> SaveAsync();
}
