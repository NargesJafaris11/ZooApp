using System.ComponentModel.DataAnnotations;

namespace ZooApp.Models;

public class Animal
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Species is required.")]
    [StringLength(50, ErrorMessage = "Species cannot exceed 50 characters.")]
    public string Species { get; set; } = "";

    [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
    public int Age { get; set; }

    // Foreign Key
    [Required(ErrorMessage = "Category is required.")]
    public int? CategoryId { get; set; }

    // Navigation Property
    public Category? Category { get; set; }
}