using System;
using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public partial class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }

    }
}
