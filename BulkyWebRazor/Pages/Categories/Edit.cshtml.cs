using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories;

[BindProperties]
public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    // [BindProperty]
    public Category Category { get; set; }

    public Edit(ApplicationDbContext db)
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
        if (ModelState.IsValid)
        {
            _db.Categories.Update(Category);
            _db.SaveChanges();
            TempData["success"] = "The category has been edited successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}