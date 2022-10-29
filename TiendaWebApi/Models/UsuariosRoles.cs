namespace TiendaWebApi.Models
{
    public class UsuariosRoles
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
