using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP02.Models;

namespace TP02.Controllers
{
    public class ContainersController : Controller
    {
        public static List<Container> ListaContainers = new List<Container>();

        public IActionResult Index()
        {
            // Garante o vínculo manual de cada container com o seu respectivo BL cadastrado
            if (ListaContainers != null)
            {
                foreach (var c in ListaContainers)
                {
                    c.Bl = BlsController.ListaBls?.FirstOrDefault(b => b.Id == c.BlId);
                }
            }

            // Envia a lista completa de containers para a View
            return View(ListaContainers ?? new List<Container>());
        }



        public IActionResult Create()
        {
            ViewData["BlId"] = new SelectList(BlsController.ListaBls, "Id", "Numero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Container container)
        {
            // Vincula manualmente o objeto BL com base no ID selecionado na caixinha
            container.Bl = BlsController.ListaBls?.FirstOrDefault(b => b.Id == container.BlId);

            // Salva direto na lista da memória ignorando validações de objetos complexos vazios
            container.Id = ListaContainers.Count + 1;
            ListaContainers.Add(container);

            // Redireciona imediatamente para a tabela de relatórios
            return RedirectToAction(nameof(Index));
        }



        // Métodos básicos de Edit e Delete adaptados para a lista
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var container = ListaContainers.FirstOrDefault(c => c.Id == id);
            if (container == null) return NotFound();
            ViewData["BlId"] = new SelectList(BlsController.ListaBls, "Id", "Numero", container.BlId);
            return View(container);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Container container)
        {
            var existente = ListaContainers.FirstOrDefault(c => c.Id == id);
            if (existente == null) return NotFound();

            if (ModelState.IsValid)
            {
                existente.Numero = container.Numero;
                existente.Tipo = container.Tipo;
                existente.Tamanho = container.Tamanho;
                existente.BlId = container.BlId;
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlId"] = new SelectList(BlsController.ListaBls, "Id", "Numero", container.BlId);
            return View(container);
        }

        // DELETE - GET (Abre a tela de confirmação)
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var container = ListaContainers.FirstOrDefault(m => m.Id == id);
            if (container == null) return NotFound();

            // Vincula o BL para mostrar na tela de confirmação qual documento será afetado
            container.Bl = BlsController.ListaBls?.FirstOrDefault(b => b.Id == container.BlId);

            return View(container);
        }

        // DELETE - POST (Executa a exclusão)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var container = ListaContainers.FirstOrDefault(x => x.Id == id);
            if (container != null)
            {
                ListaContainers.Remove(container); // Remove da memória
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
