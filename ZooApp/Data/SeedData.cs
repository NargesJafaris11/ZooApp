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
                "Bear",
                "Eagle",
                "Penguin",
                "Snake",
                "Crocodile",
                "Shark",
                "Salmon"))
            .RuleFor(a => a.Age, f => f.Random.Int(1, 25))
            .RuleFor(a => a.CategoryId, (f, a) =>
            {
                return a.Species switch
                {
                    "Lion" => categories.First(c => c.Name == "Mammal").Id,
                    "Tiger" => categories.First(c => c.Name == "Mammal").Id,
                    "Elephant" => categories.First(c => c.Name == "Mammal").Id,
                    "Giraffe" => categories.First(c => c.Name == "Mammal").Id,
                    "Zebra" => categories.First(c => c.Name == "Mammal").Id,
                    "Monkey" => categories.First(c => c.Name == "Mammal").Id,
                    "Bear" => categories.First(c => c.Name == "Mammal").Id,

                    "Eagle" => categories.First(c => c.Name == "Bird").Id,
                    "Penguin" => categories.First(c => c.Name == "Bird").Id,

                    "Snake" => categories.First(c => c.Name == "Reptile").Id,
                    "Crocodile" => categories.First(c => c.Name == "Reptile").Id,

                    "Shark" => categories.First(c => c.Name == "Fish").Id,
                    "Salmon" => categories.First(c => c.Name == "Fish").Id,

                    _ => categories.First(c => c.Name == "Mammal").Id
                };
            });

        List<Animal> animals = faker.Generate(20);

        context.Animals.AddRange(animals);
        context.SaveChanges();
    }
}