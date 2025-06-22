using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

[BindProperties]
public class Create : PageModel
{
    private readonly ApplicationDbContext _db;
    
    // [BindProperty]
    public Category Category { get; set; }

    public Create(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        _db.Categories.Add(Category);
        _db.SaveChanges();
        return RedirectToPage("Index");
    }
}