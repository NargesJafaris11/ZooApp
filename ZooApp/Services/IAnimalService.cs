using ZooApp.Models;

namespace ZooApp.Services;

public interface IAnimalService
{
    List<Animal> GetAll();
    Animal? GetById(int id);

    List<Category> GetCategories();

    void Add(Animal animal);
    void Update(Animal animal);
    void Delete(int id);
}