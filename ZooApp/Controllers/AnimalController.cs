using Microsoft.AspNetCore.Mvc;
using ZooApp.Models;
using ZooApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZooApp.Controllers;

public class AnimalController : Controller
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    public IActionResult Index(string? searchString, int? categoryId, string? sortOrder)
    {
        List<Animal> animals = _animalService.GetAll();

        ViewBag.Categories = _animalService.GetCategories();
        ViewBag.CurrentSearch = searchString;
        ViewBag.CurrentCategoryId = categoryId;
        ViewBag.CurrentSortOrder = sortOrder;

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            animals = animals
                .Where(a => a.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (categoryId.HasValue)
        {
            animals = animals
                .Where(a => a.CategoryId == categoryId.Value)
                .ToList();
        }

        animals = sortOrder switch
        {
            "name_desc" => animals.OrderByDescending(a => a.Name).ToList(),
            "age_asc" => animals.OrderBy(a => a.Age).ToList(),
            "age_desc" => animals.OrderByDescending(a => a.Age).ToList(),
            _ => animals.OrderBy(a => a.Name).ToList()
        };

        return View(animals);
    }

    public IActionResult Details(int id)
    {
        Animal? animal = _animalService.GetById(id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(
            _animalService.GetCategories(),
            "Id",
            "Name");

        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Animal animal)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(
                _animalService.GetCategories(),
                "Id",
                "Name",
                animal.CategoryId);

            return View(animal);
        }

        _animalService.Add(animal);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        Animal? animal = _animalService.GetById(id);

        if (animal == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(
            _animalService.GetCategories(),
            "Id",
            "Name",
            animal.CategoryId);

        return View(animal);
    }

    [HttpPost]
    public IActionResult Edit(Animal animal)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(
                _animalService.GetCategories(),
                "Id",
                "Name",
                animal.CategoryId);

            return View(animal);
        }

        _animalService.Update(animal);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        Animal? animal = _animalService.GetById(id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _animalService.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}