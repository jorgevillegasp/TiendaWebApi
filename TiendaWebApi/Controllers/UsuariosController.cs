
using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Dtos;
using TiendaWebApi.Interfaces;

namespace TiendaWebApi.Controllers;


public class UsuariosController :BaseApiController
{
    private readonly UsuarioInterface _userService;

    public UsuariosController(UsuarioInterface userService)
    { 
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }


}
