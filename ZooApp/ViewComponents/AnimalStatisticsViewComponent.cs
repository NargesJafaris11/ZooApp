using Microsoft.AspNetCore.Mvc;
using ZooApp.Services;

namespace ZooApp.ViewComponents;

public class AnimalStatisticsViewComponent : ViewComponent
{
    private readonly IAnimalService _animalService;

    public AnimalStatisticsViewComponent(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    public IViewComponentResult Invoke()
    {
        var animals = _animalService.GetAll();

        ViewBag.TotalAnimals = animals.Count;
        ViewBag.TotalCategories = animals
            .Select(a => a.CategoryId)
            .Distinct()
            .Count();

        ViewBag.AverageAge = animals.Any()
            ? Math.Round(animals.Average(a => a.Age), 1)
            : 0;

        return View();
    }
}