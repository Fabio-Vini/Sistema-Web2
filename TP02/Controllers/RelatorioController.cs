/*
Nome da dupla:

Fábio Vinicius Santos de Freitas

Kauã Felipe da Silva Jenior
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP02.Data;

namespace TP02.Controllers;

public class RelatorioController : Controller
{
    private readonly ApplicationDbContext _context;

    public RelatorioController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var bls = await _context.Bls
            .AsNoTracking()
            .Include(bl => bl.Containers.OrderBy(container => container.Numero))
            .OrderBy(bl => bl.Numero)
            .ToListAsync();

        return View(bls);
    }
}
