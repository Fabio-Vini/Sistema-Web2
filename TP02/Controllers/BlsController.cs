/*
Nome da dupla:

Fábio Vinicius Santos de Freitas

Kauã Felipe da Silva Jenior
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP02.Data;
using TP02.Models;

namespace TP02.Controllers;

public class BlsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BlsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var bls = await _context.Bls
            .AsNoTracking()
            .Include(bl => bl.Containers)
            .OrderBy(bl => bl.Numero)
            .ToListAsync();

        return View(bls);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
            return NotFound();

        var bl = await _context.Bls
            .AsNoTracking()
            .Include(item => item.Containers)
            .FirstOrDefaultAsync(item => item.Id == id);

        return bl is null ? NotFound() : View(bl);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Numero,Consignee,Navio")] Bl bl)
    {
        if (await _context.Bls.AnyAsync(item => item.Numero == bl.Numero))
            ModelState.AddModelError(nameof(bl.Numero), "Já existe um BL com esse número.");

        if (!ModelState.IsValid)
            return View(bl);

        _context.Add(bl);
        await _context.SaveChangesAsync();
        TempData["Success"] = "BL cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return NotFound();

        var bl = await _context.Bls.FindAsync(id);
        return bl is null ? NotFound() : View(bl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Consignee,Navio")] Bl bl)
    {
        if (id != bl.Id)
            return NotFound();

        if (await _context.Bls.AnyAsync(item => item.Numero == bl.Numero && item.Id != id))
            ModelState.AddModelError(nameof(bl.Numero), "Já existe um BL com esse número.");

        if (!ModelState.IsValid)
            return View(bl);

        var existente = await _context.Bls.FindAsync(id);
        if (existente is null)
            return NotFound();

        existente.Numero = bl.Numero;
        existente.Consignee = bl.Consignee;
        existente.Navio = bl.Navio;
        await _context.SaveChangesAsync();

        TempData["Success"] = "BL atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
            return NotFound();

        var bl = await _context.Bls
            .AsNoTracking()
            .Include(item => item.Containers)
            .FirstOrDefaultAsync(item => item.Id == id);

        return bl is null ? NotFound() : View(bl);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bl = await _context.Bls.FindAsync(id);
        if (bl is null)
            return RedirectToAction(nameof(Index));

        if (await _context.Containers.AnyAsync(container => container.BlId == id))
        {
            TempData["Error"] = "Este BL possui containers vinculados. Exclua ou transfira os containers antes.";
            return RedirectToAction(nameof(Index));
        }

        _context.Bls.Remove(bl);
        await _context.SaveChangesAsync();
        TempData["Success"] = "BL excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
