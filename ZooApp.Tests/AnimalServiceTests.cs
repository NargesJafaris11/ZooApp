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

    [Fact]
    public void GetAll_ShouldReturnAllAnimals()
    {
        ZooDbContext context = CreateContext();

        context.Animals.Add(new Animal { Name = "Simba", Species = "Lion", Age = 5 });
        context.Animals.Add(new Animal { Name = "Dumbo", Species = "Elephant", Age = 12 });
        context.SaveChanges();

        AnimalService service = new AnimalService(context);
        
        List<Animal> animals = service.GetAll();
        
        Assert.Equal(2, animals.Count);
    }
    
    [Fact]
    public void GetById_ShouldReturnAnimal_WhenAnimalExists()
    {
        ZooDbContext context = CreateContext();

        Animal animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = new AnimalService(context);
        
        Animal? result = service.GetById(animal.Id);
        
        Assert.NotNull(result);
        Assert.Equal("Simba", result!.Name);
    }
    
    [Fact]
    public void Add_ShouldAddAnimalToDatabase()
    {
        ZooDbContext context = CreateContext();

        AnimalService service = new AnimalService(context);

        Animal animal = new Animal
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
        ZooDbContext context = CreateContext();

        Animal animal = new Animal
        {
            Name = "Dumbo",
            Species = "Elephant",
            Age = 12
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = new AnimalService(context);
        
        service.Delete(animal.Id);
        
        Assert.Empty(context.Animals);
    }
    
    [Fact]
    public void Update_ShouldUpdateAnimal()
    {
        // Arrange
        ZooDbContext context = CreateContext();

        Animal animal = new Animal
        {
            Name = "Simba",
            Species = "Lion",
            Age = 5
        };

        context.Animals.Add(animal);
        context.SaveChanges();

        AnimalService service = new AnimalService(context);

        // Act
        animal.Age = 8;
        animal.Name = "King Simba";

        service.Update(animal);

        Animal? updatedAnimal = context.Animals.Find(animal.Id);

        // Assert
        Assert.NotNull(updatedAnimal);
        Assert.Equal("King Simba", updatedAnimal!.Name);
        Assert.Equal(8, updatedAnimal.Age);
    }
}