using System.ComponentModel.DataAnnotations;

namespace product_management.Models;

public class Category
{  [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<ProductCategory> CategoryProducts { get; set; }= new List<ProductCategory>();


    public Category(){}
        public Category(int id, string name)
    {   
        Id = id;
        Name = name;
        
    }



}
