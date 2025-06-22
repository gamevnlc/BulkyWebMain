
using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET
    public IActionResult Index()
    {
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The display order cannot be the same as the name");
        }
        // if (category.Name != null && category.Name == "test")
        // {
        //     ModelState.AddModelError("", "Test is an invalid category name");
        // }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "The category has been added successfully";
            return RedirectToAction("Index", "Category");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = _db.Categories.Find(id);
        // Category? category1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        // Category? category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "The category has been edited successfully";
            return RedirectToAction("Index", "Category");
        }
        return View();
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? category = _db.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = _db.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        _db.Categories.Remove(category);
        _db.SaveChanges();
        TempData["success"] = "The category has been deleted successfully";
        return RedirectToAction("Index", "Category");
    }
}