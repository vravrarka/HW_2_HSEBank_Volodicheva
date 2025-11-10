using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.App.Export
{
    public class JsonDataVisitor : DataVisitor
    {
        private readonly List<BankAccount> _accounts = new();
        private readonly List<Category> _categories = new();
        private readonly List<Operation> _operations = new();
        public override void Visit(BankAccount account)
        {
            _accounts.Add(account);
        }
        public override void Visit(Category category)
        {
            _categories.Add(category);
        }
        public override void Visit(Operation operation)
        {
            _operations.Add(operation);
        }
        public void SaveToFiles(string basePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            File.WriteAllText($"{basePath}_accounts.json",
        JsonSerializer.Serialize(_accounts, options), Encoding.UTF8);
            File.WriteAllText($"{basePath}_categories.json",
                JsonSerializer.Serialize(_categories, options), Encoding.UTF8);
            File.WriteAllText($"{basePath}_operations.json",
                JsonSerializer.Serialize(_operations, options), Encoding.UTF8);
        }
    }
}
