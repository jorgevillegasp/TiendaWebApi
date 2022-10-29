using System;
using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public partial class Marca : BaseEntity
    {
        //public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual Producto? Producto { get; set; }
    }
}
