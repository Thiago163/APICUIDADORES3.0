using API_CUIDADORES.DAO;
using API_CUIDADORES.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_CUIDADORES.Controllers
{
    [ApiController]
    [Route("api/favoritosusu")]
    public class FavoritosUsuarioController : ControllerBase
    {
        private FavoritosUsuarioDAO FavoritosUsuarioDAO;
        public FavoritosUsuarioController()
        {
            FavoritosUsuarioDAO = new FavoritosUsuarioDAO();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var favoritos = FavoritosUsuarioDAO.Listar();
            return Ok(favoritos);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] FavoritosUsuarioDTO FavoritosUsuarioDTO)
        {
            try
            {
                FavoritosUsuarioDAO.Cadastrar(FavoritosUsuarioDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Remover(int usuario_id, int cuidador_id)
        {
            FavoritosUsuarioDAO dao = new FavoritosUsuarioDAO();
            dao.Remover(usuario_id, cuidador_id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar(FavoritosUsuarioDTO cuidador)
        {
            FavoritosUsuarioDAO dao = new FavoritosUsuarioDAO();
            dao.Alterar(cuidador);
            return Ok();
        }
    }
}
