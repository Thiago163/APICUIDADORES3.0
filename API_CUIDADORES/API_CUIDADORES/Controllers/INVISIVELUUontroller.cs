using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.Controllers
{
    [Route("api/invisivelusu")]
    [ApiController]
    [Authorize]

    public class INVISIVELUUontroller : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            INVISIVELUDAO dao = new INVISIVELUDAO();
            var cuidador = dao.Listar();
            return Ok(cuidador);
        }

        [HttpPost]
        public IActionResult Cadastrar(INVISIVELUDTO cuidador)
        {
            INVISIVELUDAO dao = new INVISIVELUDAO();
            dao.Cadastrar(cuidador);
            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar(INVISIVELUDTO cuidador)
        {
            INVISIVELUDAO dao = new INVISIVELUDAO();
            dao.Alterar(cuidador);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            INVISIVELUDAO dao = new INVISIVELUDAO();
            dao.Remover(id);
            return Ok();
        }
    }
}
