/*
Nome da dupla:

Fábio Vinicius Santos de Freitas

Kauã Felipe da Silva Jenior
*/

using Microsoft.AspNetCore.Mvc;

namespace TP02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
