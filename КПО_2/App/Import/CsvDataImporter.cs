using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Import
{
    public class CsvDataImporter : DataImporter
    {
        public override List<BankAccount> ParseBankAccounts(string text)
        {
            var accounts = new List<BankAccount>();
            var lines = text.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            for (int i = 1; i < lines.Length; i++)
            {
                var val = lines[i].Split(',');
                if (val.Length ==3)
                {
                    var account = new BankAccount
                    {
                        Id = int.Parse(val[0].Trim()),
                        Name = val[1].Trim(),
                        Balance = int.Parse(val[2].Trim())
                    };

                    accounts.Add(account);
                }
            }
            return accounts;
        }
        public override List<Category> ParseCategories(string text)
        {
            var categories = new List<Category>();
            var lines = text.Split('\n').Where(line => !string.IsNullOrWhiteSpace (line)).ToArray();
            for (int i = 1; i < lines.Length; i++)
            {
                var val = lines[i].Split(',');
                if (val.Length == 3)
                {
                    var category = new Category 
                    {
                        Id = int.Parse(val[0].Trim()),
                        Type = Enum.Parse<CategoryType>(val[1].Trim(), true),
                        Name = val[2].Trim(),
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }
        public override List<Operation> ParseOperations(string text)
        {
            var operations = new List<Operation>();
            var lines = text.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            for (int i = 1; i < lines.Length; i++)
            {
                var val = lines[i].Split(',');
                if (val.Length >= 6)
                {
                    var operation = new Operation
                    {
                        Id = int.Parse (val[0].Trim()),
                        Type = Enum.Parse<CategoryType>(val[1].Trim(), true),
                        BankAccountId = int.Parse (val[2].Trim()),
                        Amount = int.Parse (val[3].Trim()),
                        Date = DateOnly.Parse (val[4].Trim()),
                        CategoryId = int.Parse (val[5].Trim()),
                        Description = val.Length > 6 ? val[6].Trim() : null
                    };
                    operations.Add(operation);
                }
            }
            return operations;
        }
    }

}
