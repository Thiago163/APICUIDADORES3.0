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
        public readonly UsuariosDAO usuariosDAO;
        public readonly CuidadoresDAO cuidadoresDAO;

        public AuthUsuariosController()
        {
            usuariosDAO = new UsuariosDAO();
            cuidadoresDAO = new CuidadoresDAO();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            // Aqui você pode implementar a lógica de autenticação do usuário
            // Por exemplo, verificar as credenciais do usuário no banco de dados

            // Se a autenticação for bem-sucedida, você pode gerar um token JWT
            var token = GenerateJwtToken("nome-do-usuario", "CcjwU9RgQYolvyxQQew7oCCcLJcnzUe89KWlD1Hf2BXdEQh0YKiieyFIoXea2Ig");

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
                new Claim(ClaimTypes.Email, "email-do-usuario"),
                new Claim(ClaimTypes.SerialNumber, "cpf-do-usuario")
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
            var token = GenerateJwtToken("nome-do-cuidador", "CcjwU9RgQYolvyxQQew7oCCcLJcnzUe89KWlD1Hf2BXdEQh0YKiieyFIoXea2Ig");

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
                new Claim(ClaimTypes.Email, "email-do-cuidador"),
                new Claim(ClaimTypes.SerialNumber, "cpf-do-cuidador")
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
