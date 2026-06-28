using Microsoft.EntityFrameworkCore;
using ZooApp.Data;
using ZooApp.Models;
using ZooApp.Services;

namespace ZooApp.Tests;

public class AnimalServiceTests
{
    private static ZooDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ZooDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ZooDbContext(options);
    }

    private static AnimalService CreateService(ZooDbContext context)
    {
        return new AnimalService(context, new FakeLoggerService());
    }

    [Fact]
    public void GetAll_ShouldReturnAllAnimals()
    {
        var context = CreateContext();

        context.Animals.Add(new Animal { Name = "Simba", Species = "Lion", Age = 5 });
        context.Animals.Add(new Animal { Name = "Dumbo", Species = "Elephant", Age = 12 });
        context.SaveChanges();

        var service = CreateService(context);

        var animals = service.GetAll();

        Assert.Equal(2, animals.Count);
    }

    [Fact]
    public void GetById_ShouldReturnAnimal_WhenAnimalExists()
    {
        var context = CreateContext();

        var animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        var service = CreateService(context);

        var result = service.GetById(animal.Id);

        Assert.NotNull(result);
        Assert.Equal("Simba", result.Name);
    }

    [Fact]
    public void Add_ShouldAddAnimalToDatabase()
    {
        var context = CreateContext();

        var service = CreateService(context);

        var animal = new Animal
        {
            Name = "Alex",
            Species = "Lion",
            Age = 8
        };

        service.Add(animal);

        Assert.Equal(1, context.Animals.Count());
    }

    [Fact]
    public void Delete_ShouldRemoveAnimalFromDatabase()
    {
        var context = CreateContext();

        var animal = new Animal
        {
            Name = "Dumbo",
            Species = "Elephant",
            Age = 12
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        var service = CreateService(context);

        service.Delete(animal.Id);

        Assert.Empty(context.Animals);
    }

    [Fact]
    public void Update_ShouldUpdateAnimal()
    {
        var context = CreateContext();

        var animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        var service = CreateService(context);

        animal.Age = 8;
        animal.Name = "King Simba";

        service.Update(animal);

        var updatedAnimal = context.Animals.Find(animal.Id);

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