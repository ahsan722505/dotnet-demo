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
        var productViewModel = new Product();
    
    // Load available categories from the data source
    List<Category> availableCategories = Category.GetCategories();
    
    // Set the available categories on the view model
    // For this step, you'll need to add an AvailableCategories property to your Product model.
    productViewModel.AvailableCategories = availableCategories;
    
    // Pass the populated view model to the view
    return View(productViewModel);
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
