using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.App.Repositories;
using Bank.MainClasses;

namespace Bank.App.Facade
{
    public class CategoryFacade
    {
        private readonly DataRepository _repository;

        public CategoryFacade()
        {
            _repository = DataRepository.Instance;
        }
        public void AddCategory(Category category)
        {
            _repository.Categories.Add(category);
            Console.WriteLine($"Добавлена категория: {category.Name} ({category.Type})");
        }
        public Category GetCategory(int id)
        {
            return _repository.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _repository.Categories;
        }

        public IEnumerable<Category> GetCategoriesByType(CategoryType type)
        {
            return _repository.Categories.Where(c => c.Type == type);
        }
        public void DisplayAllCategories()
        {
            Console.WriteLine("\nВсе категории:\n");
            foreach (var category in _repository.Categories)
            {
                Console.WriteLine($"ID: {category.Id}, Тип: {category.Type}, Название: {category.Name}");
            }
        }
    }
}
