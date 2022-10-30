namespace TiendaWebApi.Models
{
    public class Usuario : BaseEntity
    {
        
        public Usuario()
        {
            UsuariosRoles = new HashSet<UsuariosRoles>();
        }
        
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Rol> Roles { get; set; } = new HashSet<Rol>();
        public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; }

    }
}