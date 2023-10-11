using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using product_management.Models;

namespace product_management.Controllers;

public class ProductController : Controller
{

    public IActionResult Index()
    {
          
        return View(Product.GetProducts());
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product obj)
    {
        
        if (ModelState.IsValid)
        {
            Product.addProduct(obj);
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
        Product product= Product.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            Product.updateProduct(obj);
            TempData["success"] = "Product updated successfully";
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
        Product product= Product.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    //POST
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = Product.GetProduct(id);
        if (obj == null)
        {
            return NotFound();
        }

        Product.deleteProduct(obj.Id);
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
        
    }
}
