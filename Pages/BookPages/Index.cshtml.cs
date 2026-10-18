using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Precupanu_Alexia_Lab2.Models;

namespace Precupanu_Alexia_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Precupanu_Alexia_Lab2Context _context;

    public IndexModel(Precupanu_Alexia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .ToListAsync();
    }
}
