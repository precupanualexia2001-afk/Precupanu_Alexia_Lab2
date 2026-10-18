using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Precupanu_Alexia_Lab2.Models;

namespace Precupanu_Alexia_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Precupanu_Alexia_Lab2Context _context;

    public EditModel(Precupanu_Alexia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
    .Include(b => b.Authors)
    .Include(b => b.Publisher)
    .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            Book.PublisherID);
        ViewData["AuthorsID"] = new SelectList(
    _context.Authors,
    "ID",
    "FirstName",
    Book.AuthorsID);
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Publisher, "ID", "PublisherName", Book.PublisherID);

            ViewData["AuthorsID"] = new SelectList(
                _context.Authors, "ID", "FirstName", Book.AuthorsID);

            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
