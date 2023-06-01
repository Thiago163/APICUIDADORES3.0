using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.Controllers
{
    [Route("api/invisivelcui")]
    [ApiController]

    public class INVISIVELCController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            INVISIVELCDAO dao = new INVISIVELCDAO();
            var cuidador = dao.Listar();
            return Ok(cuidador);
        }

        [HttpPost]
        public IActionResult Cadastrar(INVISIVELCDTO cuidador)
        {
            INVISIVELCDAO dao = new INVISIVELCDAO();
            dao.Cadastrar(cuidador);
            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar(INVISIVELCDTO cuidador)
        {
            INVISIVELCDAO dao = new INVISIVELCDAO();
            dao.Alterar(cuidador);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            INVISIVELCDAO dao = new INVISIVELCDAO();
            dao.Remover(id);
            return Ok();
        }
    }
}
