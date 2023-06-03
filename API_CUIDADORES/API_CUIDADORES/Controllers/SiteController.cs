using API_CUIDADORES.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.Controllers
{
    [Route("api/site")]
    [ApiController]
    public class SiteController : Controller
    {
        [HttpGet]
        public IActionResult Listar()
        {
            CuidadoresDAO dao = new CuidadoresDAO();
            var cuidador = dao.Listar();
            return Ok(cuidador);
        }
    }
}
