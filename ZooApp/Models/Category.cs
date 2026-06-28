using System.ComponentModel.DataAnnotations;

namespace ZooApp.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Categorienaam is verplicht.")]
    [StringLength(50, ErrorMessage = "Categorienaam mag maximaal 50 tekens zijn.")]
    public string Name { get; set; } = "";

    public List<Animal> Animals { get; set; } = new();
}