using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Models;
using ZooApp.Services;

namespace ZooApp.Tests;

public class AnimalServiceTests
{
    private static ZooDbContext CreateContext()
    {
        DbContextOptions<ZooDbContext> options = new DbContextOptionsBuilder<ZooDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ZooDbContext(options);
    }

    private static AnimalService CreateService(ZooDbContext context)
    {
        return new AnimalService(context, new FakeLoggerService());
    }

    private static Category CreateCategory(ZooDbContext context)
    {
        Category category = new Category
        {
            Name = "Mammal"
        };

        context.Categories.Add(category);
        context.SaveChanges();

        return category;
    }

    [Fact]
    public void GetAll_ShouldReturnAllAnimals()
    {
        ZooDbContext context = CreateContext();

        Category category = CreateCategory(context);

        context.Animals.Add(new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5,
            CategoryId = category.Id
        });

        context.Animals.Add(new Animal
        {
            Name = "Dumbo",
            Species = "Elephant",
            Age = 12,
            CategoryId = category.Id
        });

        context.SaveChanges();

        AnimalService service = CreateService(context);

        List<Animal> animals = service.GetAll();

        Assert.Equal(2, animals.Count);
    }

    [Fact]
    public void GetById_ShouldReturnAnimal_WhenAnimalExists()
    {
        ZooDbContext context = CreateContext();

        Category category = CreateCategory(context);

        Animal animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5,
            CategoryId = category.Id
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = CreateService(context);

        Animal? result = service.GetById(animal.Id);

        Assert.NotNull(result);
        Assert.Equal("Simba", result.Name);
    }

    [Fact]
    public void Add_ShouldAddAnimalToDatabase()
    {
        ZooDbContext context = CreateContext();

        Category category = CreateCategory(context);

        AnimalService service = CreateService(context);

        Animal animal = new Animal
        {
            Name = "Alex",
            Species = "Lion",
            Age = 8,
            CategoryId = category.Id
        };

        service.Add(animal);

        Assert.Equal(1, context.Animals.Count());
    }

    [Fact]
    public void Delete_ShouldRemoveAnimalFromDatabase()
    {
        ZooDbContext context = CreateContext();

        Category category = CreateCategory(context);

        Animal animal = new Animal
        {
            Name = "Dumbo",
            Species = "Elephant",
            Age = 12,
            CategoryId = category.Id
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = CreateService(context);

        service.Delete(animal.Id);

        Assert.Empty(context.Animals);
    }

    [Fact]
    public void Update_ShouldUpdateAnimal()
    {
        ZooDbContext context = CreateContext();

        Category category = CreateCategory(context);

        Animal animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5,
            CategoryId = category.Id
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = CreateService(context);

        animal.Name = "King Simba";
        animal.Age = 8;

        service.Update(animal);

        Animal? updatedAnimal = context.Animals.Find(animal.Id);

        Assert.NotNull(updatedAnimal);
        Assert.Equal("King Simba", updatedAnimal.Name);
        Assert.Equal(8, updatedAnimal.Age);
    }
}

public class FakeLoggerService : ILoggerService
{
    public void Log(string message)
    {
        // Do nothing during tests
    }
}