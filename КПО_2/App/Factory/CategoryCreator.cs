using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Factory
{
    public class CategoryCreator : ObjectCreator
    {
        private readonly string _name;
        private readonly CategoryType _type;
        public CategoryCreator(CategoryType type, string name)
        {
            _name = name;
            _type = type;
        }
        public override ObjectId FactoryMethod()
        {
            if (string.IsNullOrWhiteSpace(_name)) throw new ArgumentNullException("Category name cannot be empty");
            return new Category
            {  
                Name = _name,
                Type = _type
            };
        }
    }
}
