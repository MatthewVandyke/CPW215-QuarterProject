using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPW215_QuarterProject.Data;
using CPW215_QuarterProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CPW215_QuarterProject.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ItemsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/AddOrEdit(Insert)
        // GET: Items/AddOrEdit/5(Update)
        public async Task<IActionResult> AddOrEdit(string id = null)
        {
            if (id == null)
                return View(new Item());
            else
            {
                var itemModel = await _context.Items.FindAsync(id);
                if (itemModel == null)
                {
                    return NotFound();
                }
                return View(itemModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(string id, Item item)
		{
            if (ModelState.IsValid)
			{
                //Insert
                if (id == null)
				{
                    item.Seller = await _userManager.GetUserAsync(HttpContext.User);
                    await _context.Items.AddAsync(item);
                    await _context.SaveChangesAsync();
				}
				//Update
				else
				{
					try
					{
                        _context.Items.Update(item);
                        await _context.SaveChangesAsync();
					}
                    catch (DbUpdateConcurrencyException)
					{
                        if (!ItemExists(item.ItemId))
						{ return NotFound(); }
						else
						{ throw; }
					}
				}
                return Json(new { isValid = true, html = HtmlHelper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList()) });
			}
            return Json(new { isValid = false, html = HtmlHelper.RenderRazorViewToString(this, "AddOrEdit", item) });
		}

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
