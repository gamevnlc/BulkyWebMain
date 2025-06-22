using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

[BindProperties]
public class Delete : PageModel
{
    private readonly ApplicationDbContext _db;

    // [BindProperty]
    public Category Category { get; set; }

    public Delete(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet(int? id)
    {
        if (id != null && id != 0)
        {
            Category = _db.Categories.Find(id);

        }
    }

    public IActionResult OnPost()
    {
        Category? category = _db.Categories.Find(Category.Id);
        if (category == null)
        {
            return NotFound();
        }
        _db.Categories.Remove(category);
        _db.SaveChanges();
        TempData["success"] = "Category deleted";
        return RedirectToPage("Index");
    }
}