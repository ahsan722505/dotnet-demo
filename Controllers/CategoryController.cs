using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using product_management.Models;

namespace product_management.Controllers;

public class CategoryController : Controller
{

    public IActionResult Index()
    {
          
        return View(Category.GetCategories());
    }

    public IActionResult Products(int id)
    {
         
        return View(Product.GetProductsByCategory(id));
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        
        if (ModelState.IsValid)
        {
            Category.addCategory(obj);
            TempData["success"] = "Category created successfully";
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
        Category category= Category.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            Category.updateCategory(obj);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category category= Category.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    //POST
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = Category.GetCategory(id);
        if (obj == null)
        {
            return NotFound();
        }

        Category.deleteCategory(obj.Id);
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
        
    }
}
