using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_management.Models;

namespace product_management.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;
    public ProductController(ApplicationDbContext db){
            _db=db;
    }
    public IActionResult Index()
    {
        var products= _db.Products.Include(p=> p.ProductCategories).ThenInclude(pc=> pc.Category).ToList();
        return View(products);
    }

    //GET
    public IActionResult Create()
    {
         var categories = _db.Categories.ToList();
         ViewBag.Categories=categories;
    
    return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product obj,List <int> CategoryIds)
    {
        
        if (ModelState.IsValid)
        {
            _db.Products.Add(obj);
            _db.SaveChanges();

            foreach(var categoryId in CategoryIds){
                _db.ProductCategories.Add(new ProductCategory { ProductId = obj.Id, CategoryId = categoryId });
            }
            _db.SaveChanges();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);   
    }

     public IActionResult Edit(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var product= _db.Products.Include(p=> p.ProductCategories).ThenInclude(pc=> pc.Category).FirstOrDefault(p=> p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories= _db.Categories.ToList();

        return View(product);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product obj, List<int> SelectedCategoryIds)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Update(obj);

            _db.ProductCategories.RemoveRange(_db.ProductCategories.Where(pc=> pc.ProductId == obj.Id));
            _db.SaveChanges();
            
            foreach(var categoryId in SelectedCategoryIds){
                _db.ProductCategories.Add(new ProductCategory{CategoryId =categoryId, ProductId = obj.Id});
            }

            _db.SaveChanges();

            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    public IActionResult Delete(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var product= _db.Products.Include(p=> p.ProductCategories).ThenInclude(pc=> pc.Category).FirstOrDefault(p=> p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories= _db.Categories.ToList();

        return View(product);
    }

    //POST
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Products.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Products.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
        
    }
}
