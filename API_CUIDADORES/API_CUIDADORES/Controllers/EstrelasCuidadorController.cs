using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_CUIDADORES.Controllers
{
    [ApiController]
    [Route("api/estrelascui")]
    public class EstrelasCuidadorController : ControllerBase
    {
        private EstrelasCuidadorDAO estrelasCuidadorDAO;

        public EstrelasCuidadorController()
        {
            estrelasCuidadorDAO = new EstrelasCuidadorDAO();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var estrelas = estrelasCuidadorDAO.Listar();
            return Ok(estrelas);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EstrelasCuidadorDTO estrelasCuidadorDTO)
        {
            try
            {
                estrelasCuidadorDAO.Cadastrar(estrelasCuidadorDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                estrelasCuidadorDAO.Remover(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Alterar(EstrelasCuidadorDTO estrelasCuidadorDTO)
        {
            try
            {
                estrelasCuidadorDAO.Alterar(estrelasCuidadorDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
