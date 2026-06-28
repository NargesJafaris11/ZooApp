using System.ComponentModel.DataAnnotations;

namespace ZooApp.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
    public string Name { get; set; } = "";

    public List<Animal> Animals { get; set; } = new();
}