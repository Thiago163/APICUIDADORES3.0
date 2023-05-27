using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.Controllers
{
    public class Auth_Usuario_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
