namespace TiendaWebApi.Dtos;
public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int CategoriaId { get; set; }
    public int MarcaId { get; set; }
    public MarcaDto Marca { get; set; }
    public DateTime FechaCreacion { get; set; }
}
