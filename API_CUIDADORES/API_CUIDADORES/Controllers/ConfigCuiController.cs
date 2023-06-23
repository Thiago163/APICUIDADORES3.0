using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_CUIDADORES.Controllers
{
    [ApiController]
    [Route("api/ConfigCui")]
    public class ConfigCuiController : ControllerBase
    {
        private readonly ConfigCuiDAO configCuiDAO;

        public ConfigCuiController()
        {
            configCuiDAO = new ConfigCuiDAO();
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] int id)
        {
            try
            {
                var cuidadores = configCuiDAO.Listar(id);
                return Ok(cuidadores);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar usuários: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            ConfigCuiDAO dao = new ConfigCuiDAO();
            dao.Remover(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar(CuidadoresDTO cuidador)
        {
            ConfigCuiDAO dao = new ConfigCuiDAO();
            dao.Alterar(cuidador);
            return Ok();
        }
    }
}
