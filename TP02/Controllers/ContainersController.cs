/*
Nome da dupla:

Fábio Vinicius Santos de Freitas

Kauã Felipe da Silva Jenior
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP02.Data;
using TP02.Models;

namespace TP02.Controllers;

public class ContainersController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContainersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var containers = await _context.Containers
            .AsNoTracking()
            .Include(container => container.Bl)
            .OrderBy(container => container.Numero)
            .ToListAsync();

        return View(containers);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
            return NotFound();

        var container = await _context.Containers
            .AsNoTracking()
            .Include(item => item.Bl)
            .FirstOrDefaultAsync(item => item.Id == id);

        return container is null ? NotFound() : View(container);
    }

    public async Task<IActionResult> Create()
    {
        await CarregarBlsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Numero,Tipo,Tamanho,BlId")] Container container)
    {
        await ValidarContainerAsync(container);

        if (!ModelState.IsValid)
        {
            await CarregarBlsAsync(container.BlId);
            return View(container);
        }

        _context.Add(container);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Container cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return NotFound();

        var container = await _context.Containers.FindAsync(id);
        if (container is null)
            return NotFound();

        await CarregarBlsAsync(container.BlId);
        return View(container);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Tipo,Tamanho,BlId")] Container container)
    {
        if (id != container.Id)
            return NotFound();

        await ValidarContainerAsync(container, id);

        if (!ModelState.IsValid)
        {
            await CarregarBlsAsync(container.BlId);
            return View(container);
        }

        var existente = await _context.Containers.FindAsync(id);
        if (existente is null)
            return NotFound();

        existente.Numero = container.Numero;
        existente.Tipo = container.Tipo;
        existente.Tamanho = container.Tamanho;
        existente.BlId = container.BlId;
        await _context.SaveChangesAsync();

        TempData["Success"] = "Container atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
            return NotFound();

        var container = await _context.Containers
            .AsNoTracking()
            .Include(item => item.Bl)
            .FirstOrDefaultAsync(item => item.Id == id);

        return container is null ? NotFound() : View(container);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var container = await _context.Containers.FindAsync(id);
        if (container is not null)
        {
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Container excluído com sucesso.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CarregarBlsAsync(int? selecionado = null)
    {
        var bls = await _context.Bls
            .AsNoTracking()
            .OrderBy(bl => bl.Numero)
            .ToListAsync();

        ViewData["BlId"] = new SelectList(bls, "Id", "Numero", selecionado);
        ViewData["PossuiBls"] = bls.Count > 0;
    }

    private async Task ValidarContainerAsync(Container container, int? ignorarId = null)
    {
        if (container.Tamanho is not 20 and not 40)
            ModelState.AddModelError(nameof(container.Tamanho), "Selecione o tamanho 20 ou 40 pés.");

        if (!await _context.Bls.AnyAsync(bl => bl.Id == container.BlId))
            ModelState.AddModelError(nameof(container.BlId), "Selecione um BL existente.");

        if (await _context.Containers.AnyAsync(item =>
                item.Numero == container.Numero && (!ignorarId.HasValue || item.Id != ignorarId.Value)))
            ModelState.AddModelError(nameof(container.Numero), "Já existe um container com esse número.");
    }
}
