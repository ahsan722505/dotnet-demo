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


    public Product(){}
        public Product(int id, string name, string description, decimal price)
    {   
        Id = id;
        Name = name;
        Description=description;
        Price=price;
        
    }

       public static List<Product> GetProducts()
    {

        string jsonFilePath="./Products.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Deserialize the content into a list of Product objects
        return JsonSerializer.Deserialize<List<Product>>(jsonContent)!;
    }

    public static Product GetProduct(int? id)
    {

        string jsonFilePath="./Products.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        var products= JsonSerializer.Deserialize<List<Product>>(jsonContent)!;

        var product=products.Find(prod=> prod.Id == id)!;
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
