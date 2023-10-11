using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace product_management.Models;

public class Category
{  [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }


    public Category(){}
        public Category(int id, string name)
    {   
        Id = id;
        Name = name;
        
    }

       public static List<Category> GetCategories()
    {

        string jsonFilePath="./Categories.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Deserialize the content into a list of Category objects
        return JsonSerializer.Deserialize<List<Category>>(jsonContent)!;
    }

    public static Category GetCategory(int? id)
    {

        string jsonFilePath="./Categories.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        var categories= JsonSerializer.Deserialize<List<Category>>(jsonContent)!;

        var category=categories.Find(cat=> cat.Id == id)!;
        return category;
    }

    public static void  updateCategory(Category category){
             string jsonFilePath = "./Categories.json";
        
        // Read and deserialize the existing categories
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(jsonContent) ?? new List<Category>();

        // Find the category to update
        var categoryToUpdate = categories.FirstOrDefault(c => c.Id == category.Id);

        if (categoryToUpdate != null)
        {
            // Update the category properties
            categoryToUpdate.Name = category.Name;
            // ... update other properties as needed ...

            // Serialize the updated categories list back to the JSON file
            string updatedJson = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, updatedJson);
        }
        else
        {
            Console.WriteLine("Category not found!");
        }
    }

    public static void  addCategory(Category category){
        string jsonFilePath = "./Categories.json";
        // Read and deserialize the existing categories
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(jsonContent) ?? new List<Category>();
        int newId;
        if (categories.Any())
        {
            newId = categories[categories.Count - 1].Id + 1;
        }
        else
        {
            newId = 1;  // or whatever starting value you prefer
        }
        category.Id=newId;
        categories.Add(category);
        // Serialize the updated categories list back to the JSON file
        string updatedJson = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, updatedJson);
        
    }

    public static void  deleteCategory(int id){
        string jsonFilePath = "./Categories.json";
        // Read and deserialize the existing categories
        string jsonContent = File.ReadAllText(jsonFilePath);
        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(jsonContent) ?? new List<Category>();
        categories.RemoveAll(cat=> cat.Id == id);
        // Serialize the updated categories list back to the JSON file
        string updatedJson = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, updatedJson);
        
    }



}
