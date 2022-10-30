namespace TiendaWebApi.Models
{
    public class Rol : BaseEntity
    {
        /*
        public Rol()
        {
            UsuariosRoles = new HashSet<UsuariosRoles>();
        }
        */
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();

        public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; }
    }
}

