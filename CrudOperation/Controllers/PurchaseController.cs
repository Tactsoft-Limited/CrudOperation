using CrudOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public PurchaseController(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var purchases = _context.Purchases.Include(i=>i.Supplier);
            return View(await purchases.ToListAsync());
        }

        // GET: PurchaseController/Details/5
        public IActionResult Details(int id)
        {
            Purchase item = _context.Purchases.Where(x => x.Id == id)
                .Include(i => i.PurchaseItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefault();
            item.PurchaseItems.ForEach(x => x.Amount = x.Quantity * x.PurchasePrice);

            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemName");

            return View(item);
        }

        // GET: PurchaseController/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemName");
            Purchase purchase = new Purchase();
            purchase.PurchaseItems.Add(new PurchaseItem() { Id = 1 });
            return View(purchase);
        }

        // POST: PurchaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (!ModelState.IsValid && purchase.SupplierId == 0)
                return View(purchase);

            purchase.PurchaseItems.RemoveAll(x => x.Quantity == 0);

             _context.Add(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PurchaseController/Edit/5
        public IActionResult Edit(int id)
        {
            Purchase item = _context.Purchases.Where(x => x.Id == id)
                .Include(i => i.PurchaseItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefault();
            item.PurchaseItems.ForEach(x => x.Amount = x.Quantity * x.PurchasePrice);

            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemName");

            return View(item);
        }

        // POST: PurchaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Purchase purchase)
        {
            purchase.PurchaseItems.RemoveAll(x => x.Quantity == 0);

            try
            {
                List<PurchaseItem> purchaseItems = _context.PurchaseItems.Where(x => x.PurchaseId == purchase.Id).ToList();
                _context.PurchaseItems.RemoveRange(purchaseItems);
                await _context.SaveChangesAsync();

                _context.Attach(purchase);
                _context.Entry(purchase).State = EntityState.Modified;
                _context.PurchaseItems.AddRange(purchase.PurchaseItems);
               await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: PurchaseController/Delete/5
        public IActionResult Delete(int id)
        {
            Purchase item = _context.Purchases.Where(x => x.Id == id)
                .Include(i => i.PurchaseItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefault();
            item.PurchaseItems.ForEach(x => x.Amount = x.Quantity * x.PurchasePrice);

            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemName");

            return View(item);
        }

        // POST: PurchaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Purchase purchase)
        {
            purchase.PurchaseItems.RemoveAll(a => a.Quantity == 0);

            try
            {
                List<PurchaseItem> purchaseItems = _context.PurchaseItems.Where(x => x.PurchaseId == purchase.Id).ToList();
                _context.PurchaseItems.RemoveRange(purchaseItems);
                await _context.SaveChangesAsync();

                _context.Attach(purchase);
                _context.Entry(purchase).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
