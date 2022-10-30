
using TiendaWebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;
using TiendaWebApi.Helpers;
using Microsoft.AspNetCore.Identity;
using TiendaWebApi.Dtos;
using System.Net;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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
            UnitOfWorkInterface unitOfWork, 
            TiendaWebApiContext context, 
            IOptions<JWT> jwt,
            IPasswordHasher<Usuario> passwordHasher
            ) : base(context)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> AddRoleAsync(AddRoleDto model)
        {

            var usuario = await _unitOfWork.Usuarios
                        .GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                return $"No existe algún usuario registrado con la cuenta {model.Username}.";
            }


            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            if (resultado == PasswordVerificationResult.Success)
            {


                var rolExiste = _unitOfWork.Roles
                                            .Find(u => u.Nombre.ToLower() == model.Role.ToLower())
                                            .FirstOrDefault();

                if (rolExiste != null)
                {
                    var usuarioTieneRol = usuario.Roles
                                                .Any(u => u.Id == rolExiste.Id);

                    if (usuarioTieneRol == false)
                    {
                        usuario.Roles.Add(rolExiste);
                        _unitOfWork.Usuarios.Update(usuario);
                        await _unitOfWork.SaveAsync();
                    }

                    return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
                }

                return $"Rol {model.Role} no encontrado.";
            }
            return $"Credenciales incorrectas para el usuario {usuario.UserName}.";
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
        {
            DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
            var usuario = await _unitOfWork.Usuarios
                        .GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
                return datosUsuarioDto;
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            if (resultado == PasswordVerificationResult.Success)
            {
                datosUsuarioDto.EstaAutenticado = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                datosUsuarioDto.Email = usuario.Email;
                datosUsuarioDto.UserName = usuario.UserName;
                datosUsuarioDto.Roles = usuario.Roles
                                                .Select(u => u.Nombre)
                                                .ToList();
                return datosUsuarioDto;
            }
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.UserName}.";
            return datosUsuarioDto;
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
                //Buscamos el rol determinado y se lo asignamos
                var rolPredeterminado = _unitOfWork.Roles
                                        .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                        .First();
                try
                {
                    //Asignamos a la coleccion de roles,
                    usuario.Roles.Add(rolPredeterminado);

                    //agrego el usuario
                    _unitOfWork.Usuarios.Add(usuario);

                    //guardo los cambios
                    await _unitOfWork.SaveAsync();

                    //Regresamos una cadema de exito
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

        private JwtSecurityToken CreateJwtToken(Usuario usuario)
        {
            var roles = usuario.Roles;
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role.Nombre));
            }
            var claims = new[]
            {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }


}
