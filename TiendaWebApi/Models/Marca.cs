using System;
using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public partial class Marca : BaseEntity
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
