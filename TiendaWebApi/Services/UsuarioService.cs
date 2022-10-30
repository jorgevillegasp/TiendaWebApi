
using TiendaWebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;
using TiendaWebApi.Helpers;
using Microsoft.AspNetCore.Identity;
using TiendaWebApi.Dtos;
using System.Net;
using Microsoft.Extensions.Options;

namespace TiendaWebApi.Services
{

    public class UsuarioService : GenericService<Usuario>, UsuarioInterface
    {

        private readonly JWT _jwt;
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(TiendaWebApiContext context) : base(context)
        {
        }

        public UsuarioService(
            TiendaWebApiContext context, 
            UnitOfWorkInterface unitOfWork, 
            IOptions<JWT> jwt,
            IPasswordHasher<Usuario> passwordHasher
            ) : base(context)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public Task<string> AddRoleAsync(AddRoleDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            //Establecemos la informacion de un usuario
            var usuario = new Usuario
            {
                Nombres = registerDto.Nombres,
                ApellidoMaterno = registerDto.ApellidoMaterno,
                ApellidoPaterno = registerDto.ApellidoPaterno,
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            //Incriptamos la contraseña
            usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password);

            var usuarioExiste = _unitOfWork.Usuarios
                                        .Find(u => u.UserName.ToLower() == registerDto.UserName.ToLower())
                                        .FirstOrDefault();

            //Validamos si existe el usuario con ese userName en la Base de datos
            if (usuarioExiste == null)
            {
                //Buscamos el rol determinado
                var rolPredeterminado = _unitOfWork.Roles
                                        .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                        .First();
                try
                {
                    //Asignamos a la coleccion de roles,
                    //agrego el usuario
                    //guardo los cambios 
                    //Regresamos una cadema de exito
                    usuario.Roles.Add(rolPredeterminado);
                    _unitOfWork.Usuarios.Add(usuario);
                    await _unitOfWork.SaveAsync();

                    return $"El usuario  {registerDto.UserName} ha sido registrado exitosamente";
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {
                return $"El usuario con {registerDto.UserName} ya se encuentra registrado.";
            }
        }
    }
}
