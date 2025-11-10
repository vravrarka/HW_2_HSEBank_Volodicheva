using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Export
{
    public class YamlDataVisitor : DataVisitor
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
        public void SaveToFiles (string basePath)
        {
            SaveAccountsToYaml($"{basePath}_accounts.yaml");
            SaveCategoriesToYaml($"{basePath}_categories.yaml");
            SaveOperationsToYaml($"{basePath}_operations.yaml");
        }
        private void SaveAccountsToYaml(string filePath)
        {
            var yaml = new StringBuilder();
            yaml.AppendLine("accounts:");
            foreach (var account in _accounts)
            {
                yaml.AppendLine($"    id: {account.Id}");
                yaml.AppendLine($"    name: \"{account.Name}\"");
                yaml.AppendLine($"    balance: {account.Balance}");
            }
            File.WriteAllText(filePath, yaml.ToString());
        }
        private void SaveCategoriesToYaml(string filePath)
        {
            var yaml = new StringBuilder();
            yaml.AppendLine("categories:");
            foreach (var category in _categories)
            {
                yaml.AppendLine($"    id: {category.Id}");
                yaml.AppendLine($"    type: {category.Type}");
                yaml.AppendLine($"    name: \"{category.Name}\"");
            }
            File.WriteAllText(filePath, yaml.ToString());
        }
        private void SaveOperationsToYaml(string filePath)
        {
            var yaml = new StringBuilder();
            yaml.AppendLine("operations:");
            foreach (var operation in _operations)
            {
                yaml.AppendLine($"    id: {operation.Id}");
                yaml.AppendLine($"    type: {operation.Type}");
                yaml.AppendLine($"    bankAccountId: {operation.BankAccountId}");
                yaml.AppendLine($"    amount: {operation.Amount}");
                yaml.AppendLine($"    date: {operation.Date:yyyy-MM-dd}");
                yaml.AppendLine($"    categoryId: {operation.CategoryId}");
                yaml.AppendLine($"    description: \"{operation.Description ?? ""}\"");
            }
            File.WriteAllText(filePath, yaml.ToString());
        }
    }
}
