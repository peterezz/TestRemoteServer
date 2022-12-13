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
    public class FarmsController : Controller
    {
        private readonly ePrime_Test_2019ddasmfet7Context _context;

        public FarmsController(ePrime_Test_2019ddasmfet7Context context)
        {
            _context = context;
        }

        // GET: Farms
        public async Task<IActionResult> Index()
        {
            var ePrime_Test_2019ddasmfet7Context = _context.Farm.Include(f => f.Country).Include(f => f.ParentFarm);
            return View(await ePrime_Test_2019ddasmfet7Context.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            var ePrime_Test_2019ddasmfet7Context = _context.Farm.Include(f => f.Country).Include(f => f.ParentFarm);
            return View(await ePrime_Test_2019ddasmfet7Context.ToListAsync());
        }

        // GET: Farms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.Country)
                .Include(f => f.ParentFarm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // GET: Farms/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.CountryRegion, "Id", "NameAr");
            ViewData["ParentFarmId"] = new SelectList(_context.Farm, "Id", "NameEn");
            return View();
        }

        // POST: Farms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameEn,NameAr,Gpsx,Gpsy,CreationDate,CreatedBy,ModificationDate,ModifiedBy,Deleted,WithoutPackHouse,ParentFarmId,CountryId,NoOfReceivedStickers,NoOfUsedStickers,Remarks,HarvestDate")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.CountryRegion, "Id", "NameAr", farm.CountryId);
            ViewData["ParentFarmId"] = new SelectList(_context.Farm, "Id", "NameEn", farm.ParentFarmId);
            return View(farm);
        }

        // GET: Farms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.CountryRegion, "Id", "NameAr", farm.CountryId);
            ViewData["ParentFarmId"] = new SelectList(_context.Farm, "Id", "NameEn", farm.ParentFarmId);
            return View(farm);
        }

        // POST: Farms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameEn,NameAr,Gpsx,Gpsy,CreationDate,CreatedBy,ModificationDate,ModifiedBy,Deleted,WithoutPackHouse,ParentFarmId,CountryId,NoOfReceivedStickers,NoOfUsedStickers,Remarks,HarvestDate")] Farm farm)
        {
            if (id != farm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmExists(farm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.CountryRegion, "Id", "NameAr", farm.CountryId);
            ViewData["ParentFarmId"] = new SelectList(_context.Farm, "Id", "NameEn", farm.ParentFarmId);
            return View(farm);
        }

        // GET: Farms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.Country)
                .Include(f => f.ParentFarm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Farm == null)
            {
                return Problem("Entity set 'ePrime_Test_2019ddasmfet7Context.Farm'  is null.");
            }
            var farm = await _context.Farm.FindAsync(id);
            if (farm != null)
            {
                _context.Farm.Remove(farm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmExists(int id)
        {
          return _context.Farm.Any(e => e.Id == id);
        }
    }
}
