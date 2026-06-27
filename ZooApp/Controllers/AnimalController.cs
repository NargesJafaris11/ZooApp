using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Models;

namespace ZooApp.Controllers;

public class AnimalController : Controller
{
    private readonly ZooDbContext _context;

    public AnimalController(ZooDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        List<Animal> animals = _context.Animals
            .Include(a => a.Category)
            .ToList();

        return View(animals);
    }

    public IActionResult Details(int id)
    {
        Animal? animal = _context.Animals.FirstOrDefault(a => a.Id == id);

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
        if (!ModelState.IsValid)
        {
            return View(animal);
        }

        _context.Animals.Add(animal);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        Animal? animal = _context.Animals.FirstOrDefault(a => a.Id == id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    [HttpPost]
    public IActionResult Edit(Animal animal)
    {
        if (!ModelState.IsValid)
        {
            return View(animal);
        }

        _context.Animals.Update(animal);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        Animal? animal = _context.Animals.FirstOrDefault(a => a.Id == id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        Animal? animal = _context.Animals.FirstOrDefault(a => a.Id == id);

        if (animal != null)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}