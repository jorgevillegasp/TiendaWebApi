using System;
using System.Collections.Generic;
using TiendaWebApi.Models;

namespace TiendaWebApi.Models
{
    public partial class Categoria : BaseEntity
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        //public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
