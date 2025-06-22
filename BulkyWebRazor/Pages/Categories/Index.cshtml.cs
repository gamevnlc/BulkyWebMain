using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

public class Index : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Category> Categories { get; set; }

    public Index(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        Categories = _db.Categories.ToList();
    }
}