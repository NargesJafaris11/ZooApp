using Bogus;
using ZooApp.Models;

namespace ZooApp.Data;

public static class SeedData
{
    public static void Initialize(ZooDbContext context)
    {
        if (context.Animals.Any())
        {
            return;
        }

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
            .RuleFor(a => a.Age, f => f.Random.Int(1, 25));

        List<Animal> animals = faker.Generate(20);

        context.Animals.AddRange(animals);
        context.SaveChanges();
    }
}