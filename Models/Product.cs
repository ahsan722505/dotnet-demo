using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace product_management.Models;

public class Product
{  [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
     [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    public List<int> CategoryIds { get; set; } = new List<int>();
    public List<string> CategoryNames { get; set; } = new List<string>();
    public List<Category> AvailableCategories { get; set; } = new List<Category>();



    public Product(){}
        public Product(int id, string name, string description, decimal price, List<int> categoryIds)
    {   
        Id = id;
        Name = name;
        Description=description;
        Price=price;
        CategoryIds=categoryIds;
        
    }

       public static List<Product> GetProducts()
    {

        string jsonFilePath="./Products.json";
        string jsonContent = File.ReadAllText(jsonFilePath);
         List<Product> products=JsonSerializer.Deserialize<List<Product>>(jsonContent)!;
         List<Category> categories=Category.GetCategories();
        foreach (var product in products)
            {
                product.CategoryNames = product.CategoryIds
                    .Select(id => categories.FirstOrDefault(cat => cat.Id == id)?.Name)
                    .Where(name => name != null)
                    .ToList()!;
            }

        return products;

    }

    public static Product GetProduct(int? id)
    {

        string jsonFilePath="./Products.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        var products= JsonSerializer.Deserialize<List<Product>>(jsonContent)!;

        var product=products.Find(prod=> prod.Id == id)!;
        var categories= Category.GetCategories();
        product.AvailableCategories=categories;
        return product;
    }

    public static void  updateProduct(Product product){
             string jsonFilePath = "./Products.json";
        
        // Read and deserialize the existing products
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonContent) ?? new List<Product>();

        // Find the product to update
        var productToUpdate = products.FirstOrDefault(prod => prod.Id == product.Id);

        if (productToUpdate != null)
        {
            // Update the product properties
            productToUpdate.Name = product.Name;
            productToUpdate.Description=product.Description;
            productToUpdate.Price=product.Price;
            productToUpdate.CategoryIds=product.CategoryIds;

            // Serialize the updated products list back to the JSON file
            string updatedJson = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, updatedJson);
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    public static void  addProduct(Product product){
        string jsonFilePath = "./Products.json";
        // Read and deserialize the existing products
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonContent) ?? new List<Product>();
        int newId;
        if (products.Any())
        {
            newId = products[products.Count - 1].Id + 1;
        }
        else
        {
            newId = 1;  // or whatever starting value you prefer
        }
        product.Id=newId;
        products.Add(product);
        // Serialize the updated products list back to the JSON file
        string updatedJson = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, updatedJson);
        
    }

    public static void  deleteProduct(int id){
        string jsonFilePath = "./Products.json";
        // Read and deserialize the existing products
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonContent) ?? new List<Product>();
        products.RemoveAll(prod=> prod.Id == id);
        // Serialize the updated products list back to the JSON file
        string updatedJson = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, updatedJson);
        
    }



}
