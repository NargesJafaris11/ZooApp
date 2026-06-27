namespace ZooApp.Models;

public class Animal
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Species { get; set; } = "";

    public int Age { get; set; }
    
    // Foreign Key
    public int? CategoryId { get; set; }
    
    
    // Navigation Property
    public Category? Category { get; set; }
}