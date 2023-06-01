using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;

namespace API_CUIDADORES.Controllers
{
    [Route("api/loginusu")]
    [ApiController]
    public class AuthUsuariosController : ControllerBase
    {
        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromForm] string login, [FromForm] string senha)
        {

            var usuarioDAO = new UsuariosDAO();

            var usuario = usuarioDAO.Login(login, senha);

            if(usuario.id == 0)
            {
                return BadRequest("Usuário ou senha inválidos");
            }

            var token = GenerateJwtToken(usuario, "16b564d8e7b9b32a36edf518df48e40c35aeb933ce4abab8e75fd3e5ef89a7f6");

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else
            {
                // Lógica de tratamento caso o token seja nulo ou vazio
                return BadRequest("Falha ao gerar o token JWT");
            }
        }

        public string GenerateJwtToken(UsuariosDTO usuario, string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("ID", usuario.id.ToString()),
                new Claim("Email", usuario.email),
                new Claim("cpf", usuario.cpf)
            };

            var token = new JwtSecurityToken(
                "",
                "",
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    [Route("api/logincui")]
    [ApiController]
    public class AuthCuidadoresController : ControllerBase
    {
        public readonly CuidadoresDAO cuidadoresDAO;

        public AuthCuidadoresController()
        {
            cuidadoresDAO = new CuidadoresDAO();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            // Aqui você pode implementar a lógica de autenticação do cuidador
            // Por exemplo, verificar as credenciais do cuidador no banco de dados

            // Se a autenticação for bem-sucedida, você pode gerar um token JWT
            var token = GenerateJwtToken("nome", "16b564d8e7b9b32a36edf518df48e40c35aeb933ce4abab8e75fd3e5ef89a7f6");

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else
            {
                // Lógica de tratamento caso o token seja nulo ou vazio
                return BadRequest("Falha ao gerar o token JWT");
            }
        }

        public string GenerateJwtToken(string username, string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, "email"),
                new Claim(ClaimTypes.SerialNumber, "cpf")
            };

            var token = new JwtSecurityToken(
                "",
                "",
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
