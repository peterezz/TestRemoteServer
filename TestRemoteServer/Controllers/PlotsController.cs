using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestRemoteServer.Models;

namespace TestRemoteServer.Controllers
{
    public class PlotsController : Controller
    {
        private readonly ePrime_Test_2019ddasmfet7Context _context;

        public PlotsController(ePrime_Test_2019ddasmfet7Context context)
        {
            _context = context;
        }

        // GET: Plots
        public async Task<IActionResult> Index()
        {
            var ePrime_Test_2019ddasmfet7Context = _context.Plot.Include(p => p.Farm).Include(p => p.Variety);
            return View(await ePrime_Test_2019ddasmfet7Context.ToListAsync());
        }

        // GET: Plots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plot == null)
            {
                return NotFound();
            }

            var plot = await _context.Plot
                .Include(p => p.Farm)
                .Include(p => p.Variety)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plot == null)
            {
                return NotFound();
            }

            return View(plot);
        }
    }
}
