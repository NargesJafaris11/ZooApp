using Bogus;
using ZooApp.Models;

namespace ZooApp.Data;

public static class SeedData
{
    public static void Initialize(ZooDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        List<Category> categories = new()
        {
            new Category { Name = "Mammal" },
            new Category { Name = "Bird" },
            new Category { Name = "Reptile" },
            new Category { Name = "Fish" }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();

        Faker<Animal> faker = new Faker<Animal>()
            .RuleFor(a => a.Name, f => f.Name.FirstName())
            .RuleFor(a => a.Species, f => f.PickRandom(
                "Lion",
                "Tiger",
                "Elephant",
                "Giraffe",
                "Zebra",
                "Monkey",
                "Bear"))
            .RuleFor(a => a.Age, f => f.Random.Int(1, 25))
            .RuleFor(a => a.CategoryId, f => categories[0].Id); // Mammal

        List<Animal> animals = faker.Generate(20);

        context.Animals.AddRange(animals);
        context.SaveChanges();
    }
}