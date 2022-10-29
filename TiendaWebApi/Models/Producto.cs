using System;
using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public partial class Producto : BaseEntity
    {
        //public int Id { get; set; }
        public int? CategoriaId { get; set; }
        public int? MarcaId { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Marca Id1 { get; set; } = null!;
        public virtual Categoria IdNavigation { get; set; } = null!;
    }
}
