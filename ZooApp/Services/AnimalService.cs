using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Models;

namespace ZooApp.Services;

public class AnimalService : IAnimalService
{
    private readonly ZooDbContext _context;

    public AnimalService(ZooDbContext context)
    {
        _context = context;
    }

    public List<Animal> GetAll()
    {
        return _context.Animals
            .Include(a => a.Category)
            .ToList();
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
    }

    public void Update(Animal animal)
    {
        _context.Animals.Update(animal);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        Animal? animal = _context.Animals.Find(id);

        if (animal != null)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }
    }
    
    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }
}