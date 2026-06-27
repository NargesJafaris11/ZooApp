using System.ComponentModel.DataAnnotations;

namespace ZooApp.Models;

public class Animal
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naam is verplicht.")]
    [StringLength(50, ErrorMessage = "Naam mag maximaal 50 tekens zijn.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Diersoort is verplicht.")]
    [StringLength(50, ErrorMessage = "Diersoort mag maximaal 50 tekens zijn.")]
    public string Species { get; set; } = "";

    [Range(0, 100, ErrorMessage = "Leeftijd moet tussen 0 en 100 zijn.")]
    public int Age { get; set; }

    // Foreign Key
    public int? CategoryId { get; set; }

    // Navigation Property
    public Category? Category { get; set; }
}