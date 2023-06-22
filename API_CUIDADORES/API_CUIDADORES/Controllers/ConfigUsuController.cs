using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_CUIDADORES.Controllers
{
    [ApiController]
    [Route("api/ConfigUsu")]
    public class ConfigUsuController : ControllerBase
    {
        private readonly ConfigUsuDAO configUsuDAO;

        public ConfigUsuController()
        {
            configUsuDAO = new ConfigUsuDAO();
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] int id)
        {
            try
            {
                var usuarios = configUsuDAO.Listar(id);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar usuários: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                configUsuDAO.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao remover usuário: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody] UsuariosDTO usuario)
        {
            try
            {
                usuario.id = id;
                configUsuDAO.Alterar(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar usuário: {ex.Message}");
            }
        }
    }
}
