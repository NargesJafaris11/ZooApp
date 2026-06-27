using Microsoft.AspNetCore.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers;

public class AnimalController : Controller
{
    private static List<Animal> animals = new()
    {
        new Animal { Id = 1, Name = "Simba", Species = "Lion", Age = 5 },
        new Animal { Id = 2, Name = "Dumbo", Species = "Elephant", Age = 12 },
        new Animal { Id = 3, Name = "Melman", Species = "Giraffe", Age = 7 }
    };

    public IActionResult Index()
    {
        return View(animals);
    }

    public IActionResult Details(int id)
    {
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
        animal.Id = animals.Max(a => a.Id) + 1;
        animals.Add(animal);

        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Edit(int id)
    {
        Animal? animal = animals.FirstOrDefault(a => a.Id == id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    [HttpPost]
    public IActionResult Edit(Animal animal)
    {
        Animal? existingAnimal = animals.FirstOrDefault(a => a.Id == animal.Id);

        if (existingAnimal == null)
        {
            return NotFound();
        }

        existingAnimal.Name = animal.Name;
        existingAnimal.Species = animal.Species;
        existingAnimal.Age = animal.Age;

        return RedirectToAction(nameof(Index));
    }
}