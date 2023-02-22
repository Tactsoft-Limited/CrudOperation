using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudOperation.Models;

namespace CrudOperation.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Student.Include(s => s.City).Include(s => s.Country).Include(s => s.State);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList( Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList());
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");

            return PartialView("Create");
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                if (pictureFile != null && pictureFile.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/students",
                     pictureFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        pictureFile.CopyTo(stream);
                    }
                    student.Picture = $"{pictureFile.FileName}";
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", student.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", student.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", student.StateId);
            return View("Create", student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", student.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", student.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", student.StateId);
            return PartialView(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _student = await _context.Student.FindAsync(student.Id);

                    if (pictureFile != null && pictureFile.Length > 0)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/students",
                         pictureFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            pictureFile.CopyTo(stream);
                        }
                        student.Picture = $"{pictureFile.FileName}";
                    }
                    else
                    {
                        student.Picture = _student.Picture;
                    }
                    _student.Picture = student.Picture;
                    _student.StudentName = student.StudentName;
                    _student.Address = student.Address;
                    _student.GenderId = student.GenderId;
                    _student.Ssc = student.Ssc;
                    _student.Hsc = student.Hsc;
                    _student.Bsc = student.Bsc;
                    _student.CountryId = student.CountryId;
                    _student.StateId = student.StateId;
                    _student.CityId = student.CityId;

                    _context.Update(_student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", student.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", student.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", student.StateId);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'AppDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Student.Any(e => e.Id == id);
        }
    }
}
