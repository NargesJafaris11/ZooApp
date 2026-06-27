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
}