using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_CUIDADORES.Controllers
{
    [ApiController]
    [Route("api/configusu")]
    public class ConfigUsuController : ControllerBase
    {
        private readonly ConfigUsuDAO _configUsuDAO;

        public ConfigUsuController()
        {
            _configUsuDAO = new ConfigUsuDAO();
        }

        [HttpGet]
        public ActionResult<List<UsuariosDTO>> Listar()
        {
            var usuarios = _configUsuDAO.Listar();
            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            _configUsuDAO.Remover(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Alterar(int id, [FromBody] UsuariosDTO usuario)
        {
            if (id != usuario.id)
            {
                return BadRequest();
            }

            _configUsuDAO.Alterar(usuario);
            return NoContent();
        }
    }
}
