using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Import
{
    public class YamlDataImporter : DataImporter
    {
        public override List<BankAccount> ParseBankAccounts(string text)
        {
            var accounts = new List<BankAccount>();
            try
            {
                var lines = text.Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .ToArray();
                BankAccount currentAccount = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("Id:"))
                    {
                        if (currentAccount != null)
                        {
                            accounts.Add(currentAccount);
                        }
                        currentAccount = new BankAccount();
                        currentAccount.Id = int.Parse(line.Split(':')[1].Trim());
                    }
                    else if (currentAccount != null)
                    {
                        if (line.StartsWith("Name:"))
                        {
                            currentAccount.Name = ExtractYamlStringValue(line);
                        }
                        else if (line.StartsWith("Balance:"))
                        {
                            currentAccount.Balance = int.Parse(line.Split(':')[1].Trim());
                        }
                    }
                }
                if (currentAccount != null)
                {
                    accounts.Add(currentAccount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка парсинга YAML: {ex.Message}");
            }
            return accounts;
        }
        public override List<Category> ParseCategories(string text)
        {
            var categories = new List<Category>();
            try
            {
                var lines = text.Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .ToArray();
                Category currentCategory = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("Id:"))
                    {
                        if (currentCategory != null)
                        {
                            categories.Add(currentCategory);
                        }

                        currentCategory = new Category();
                        currentCategory.Id = int.Parse(line.Split(':')[1].Trim());
                    }
                    else if (currentCategory != null)
                    {
                        if (line.StartsWith("Name:"))
                        {
                            currentCategory.Name = ExtractYamlStringValue(line);
                        }
                        else if (line.StartsWith("Type:"))
                        {
                            var typeValue = line.Split(':')[1].Trim();
                            currentCategory.Type = Enum.Parse<CategoryType>(typeValue, true);
                        }
                    }
                }
                if (currentCategory != null)
                {
                    categories.Add(currentCategory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка парсинга YAML: {ex.Message}");
            }
            return categories;
        }
        public override List<Operation> ParseOperations(string text)
        {
            var operations = new List<Operation>();
            try
            {
                var lines = text.Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .ToArray();

                Operation currentOperation = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("Id:"))
                    {
                        if (currentOperation != null)
                        {
                            operations.Add(currentOperation);
                        }

                        currentOperation = new Operation();
                        currentOperation.Id = int.Parse(line.Split(':')[1].Trim());
                    }
                    else if (currentOperation != null)
                    {
                        if (line.StartsWith("Type:"))
                        {
                            var typeValue = line.Split(':')[1].Trim();
                            currentOperation.Type = Enum.Parse<CategoryType>(typeValue, true);
                        }
                        else if (line.StartsWith("BankAccountId:"))
                        {
                            currentOperation.BankAccountId = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("Amount:"))
                        {
                            currentOperation.Amount = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("Date:"))
                        {
                            var dateValue = line.Split(':')[1].Trim();
                            currentOperation.Date = DateOnly.Parse(dateValue);
                        }
                        else if (line.StartsWith("CategoryId:"))
                        {
                            currentOperation.CategoryId = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("Description:"))
                        {
                            currentOperation.Description = ExtractYamlStringValue(line);
                        }
                    }
                }
                if (currentOperation != null)
                {
                    operations.Add(currentOperation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка парсинга YAML: {ex.Message}");
            }
            return operations;
        }
        private string ExtractYamlStringValue(string line)
        {
            var parts = line.Split(':', 2);
            if (parts.Length < 2) return string.Empty;

            var val = parts[1].Trim();
            if ((val.StartsWith("\"") && val.EndsWith("\"")) ||
                (val.StartsWith("'") && val.EndsWith("'")))
            {
                val = val.Substring(1, val.Length - 2);
            }

            return val;
        }
    }
}
