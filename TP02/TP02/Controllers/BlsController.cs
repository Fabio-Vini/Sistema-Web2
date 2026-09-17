using Microsoft.AspNetCore.Mvc;
using TP02.Models;

namespace TP02.Controllers
{
    public class BlsController : Controller
    {

       
        public static List<Bl> ListaBls = new List<Bl>
{
    new Bl { Id = 1, Numero = "BL-IMPORTACAO-2026", Consignee = "Logística Cubatão LTDA", Navio = "Santos Express" },
    new Bl { Id = 2, Numero = "BL-EXPORTACAO-9988", Consignee = "Alimentos Brasil S/A", Navio = "Oceanic Clipper" }
};


        public IActionResult Index()
        {
            return View(ListaBls);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bl bl)
        {
            if (ModelState.IsValid)
            {
                bl.Id = ListaBls.Count + 1;
                ListaBls.Add(bl);
                return RedirectToAction(nameof(Index));
            }
            return View(bl);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var bl = ListaBls.FirstOrDefault(x => x.Id == id);
            if (bl == null) return NotFound();
            return View(bl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Bl bl)
        {
            var existente = ListaBls.FirstOrDefault(x => x.Id == id);
            if (existente == null) return NotFound();

            if (ModelState.IsValid)
            {
                existente.Numero = bl.Numero;
                existente.Consignee = bl.Consignee;
                existente.Navio = bl.Navio;
                return RedirectToAction(nameof(Index));
            }
            return View(bl);
        }

        // DELETE - GET (Abre a tela de confirmação)
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            // Busca o BL na lista da memória pelo ID
            var bl = ListaBls.FirstOrDefault(m => m.Id == id);
            if (bl == null) return NotFound();

            return View(bl);
        }

        // DELETE - POST (Executa a exclusão após clicar no botão)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bl = ListaBls.FirstOrDefault(x => x.Id == id);
            if (bl != null)
            {
                ListaBls.Remove(bl); // Remove da memória
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
