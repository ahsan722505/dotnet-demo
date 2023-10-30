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
    public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();




    public Product(){}
        public Product(int id, string name, string description, decimal price)
    {   
        Id = id;
        Name = name;
        Description=description;
        Price=price;
        
    }





}
