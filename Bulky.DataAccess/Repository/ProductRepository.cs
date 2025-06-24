using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db): base(db)
    {
        _db = db;
    }
    public void Update(Product obj)
    {
        var objFromDB = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDB != null)
        {
            objFromDB.Title = obj.Title;
            objFromDB.Description = obj.Description;
            objFromDB.Price = obj.Price;
            objFromDB.CategoryId = obj.CategoryId;
            objFromDB.ISBN = obj.ISBN;
            objFromDB.Author = obj.Author;
            objFromDB.Price50 = obj.Price50;
            objFromDB.Price100 = obj.Price100;
            if (obj.ImageUrl != null)
            {
                objFromDB.ImageUrl = obj.ImageUrl;
            }
        }
        // _db.Products.Update(obj);
    } 
}