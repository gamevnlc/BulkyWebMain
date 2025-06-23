using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    // private readonly IProductRepository _ProductRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        // _ProductRepository = db;
        _unitOfWork = unitOfWork;
    }

    // GET
    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.ProductRepository.GetAll().ToList();
        return View(products);
    }

    public IActionResult Create()
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        
        // ViewBag.CategoryList = CategoryList;
        // ViewData["CategoryList"] = CategoryList;
        ProductVM productVm = new()
        {
            CategoryList = CategoryList,
            Product = new Product()
        };
        return View(productVm);
    }
    
    [HttpPost]
    // public IActionResult Create(Product product)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _unitOfWork.ProductRepository.Add(product);
    //         _unitOfWork.Save();
    //         TempData["success"] = "The Product has been added successfully";
    //         return RedirectToAction("Index", "Product");
    //     }
    //     return View();
    // }
    //
    public IActionResult Create(ProductVM productVm)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Add(productVm.Product);
            _unitOfWork.Save();
            TempData["success"] = "The Product has been added successfully";
            return RedirectToAction("Index", "Product");
        }
        else
        {
            productVm.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            
            return View(productVm);
        }
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        // Product? Product1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        // Product? Product2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "The Product has been edited successfully";
            return RedirectToAction("Index", "Product");
        }
        return View();
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        _unitOfWork.ProductRepository.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "The Product has been deleted successfully";
        return RedirectToAction("Index", "Product");
    }
}