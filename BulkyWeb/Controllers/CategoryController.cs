
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    // private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        // _categoryRepository = db;
        _unitOfWork = unitOfWork;
    }

    // GET
    public IActionResult Index()
    {
        List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();
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
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();
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

        Category? category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
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
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();
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

        Category? category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.CategoryRepository.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "The category has been deleted successfully";
        return RedirectToAction("Index", "Category");
    }
}