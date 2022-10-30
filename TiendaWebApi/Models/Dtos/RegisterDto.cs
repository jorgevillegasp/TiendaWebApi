﻿using System.ComponentModel.DataAnnotations;
namespace TiendaWebApi.Dtos;

public class RegisterDto
{
    [Required]
    public string Nombres { get; set; }
    [Required]
    public string ApellidoPaterno { get; set; }
    [Required]
    public string ApellidoMaterno { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
