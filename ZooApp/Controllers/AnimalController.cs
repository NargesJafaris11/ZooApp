using Microsoft.AspNetCore.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers;

public class AnimalController : Controller
{
    public IActionResult Index()
    {
        List<Animal> animals = new()
        {
            new Animal { Id = 1, Name = "Simba", Species = "Lion", Age = 5 },
            new Animal { Id = 2, Name = "Dumbo", Species = "Elephant", Age = 12 },
            new Animal { Id = 3, Name = "Melman", Species = "Giraffe", Age = 7 }
        };

        return View(animals);
    }
    
    public IActionResult Details(int id)
    {
        List<Animal> animals = new()
        {
            new Animal { Id = 1, Name = "Simba", Species = "Lion", Age = 5 },
            new Animal { Id = 2, Name = "Dumbo", Species = "Elephant", Age = 12 },
            new Animal { Id = 3, Name = "Melman", Species = "Giraffe", Age = 7 }
        };

        Animal? animal = animals.FirstOrDefault(a => a.Id == id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Animal animal)
    {
        return RedirectToAction(nameof(Index));
    }
}