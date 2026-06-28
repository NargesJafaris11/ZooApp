using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Models;

namespace ZooApp.Services;

public class AnimalService : IAnimalService
{
    private readonly ZooDbContext _context;
    private readonly ILoggerService _logger;

    public AnimalService(ZooDbContext context, ILoggerService logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<Animal> GetAll()
    {
        return _context.Animals
            .Include(a => a.Category)
            .ToList();
    }

    public List<Animal> SearchFilterAndSort(string? searchString, int? categoryId, string? sortOrder)
    {
        List<Animal> animals = GetAll();

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

        return animals;
    }

    public Animal? GetById(int id)
    {
        return _context.Animals
            .Include(a => a.Category)
            .FirstOrDefault(a => a.Id == id);
    }

    public void Add(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();

        _logger.Log($"Animal added: {animal.Name}");
    }

    public void Update(Animal animal)
    {
        _context.Animals.Update(animal);
        _context.SaveChanges();

        _logger.Log($"Animal updated: {animal.Name}");
    }

    public void Delete(int id)
    {
        Animal? animal = _context.Animals.Find(id);

        if (animal != null)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();

            _logger.Log($"Animal deleted: {animal.Name}");
        }
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }
}