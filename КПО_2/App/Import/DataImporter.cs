using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Import
{
    public abstract class DataImporter
    {
        public List<BankAccount> ImportBankAccounts(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл {filePath} не найден");
                return new List<BankAccount>();
            }
            string fileContent = ReadFile(filePath);
            Console.WriteLine($"Файл успешно прочитан!");
            List<BankAccount> accounts = ParseBankAccounts(fileContent);
            ValidateBankAccounts(accounts);
            return accounts;
        }
        public List<Category> ImportCategories(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл {filePath} не найден");
                return new List<Category>();
            }
            string fileContent = ReadFile(filePath);
            Console.WriteLine($"Файл успешно прочитан!");
            List<Category> categories = ParseCategories(fileContent);
            ValidateCategories(categories);
            return categories;
        }
        public List<Operation> ImportOperations(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл {filePath} не найден");
                return new List<Operation>();
            }
            string fileContent = ReadFile(filePath);
            Console.WriteLine($"Файл успешно прочитан!");
            List<Operation> operations = ParseOperations(fileContent);
            ValidateOperations(operations);
            return operations;
        }
        protected virtual string ReadFile(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.Default, true);
            return reader.ReadToEnd();
        }
        public abstract List<BankAccount> ParseBankAccounts(string content);
        public abstract List<Category> ParseCategories(string content);
        public abstract List<Operation> ParseOperations(string content);
        public virtual void ValidateBankAccounts(List<BankAccount> accounts)
        {
            foreach (var account in accounts)
            {
                if (string.IsNullOrWhiteSpace(account.Name))
                {
                    throw new ArgumentException("Название счета не может быть пустым");
                }
                if (account.Balance < 0)
                {
                    throw new ArgumentException("Баланс счета не может быть отрицательным");
                }
            }
        }
        public virtual void ValidateCategories(List<Category> categories)
        {
            foreach (var category in categories)
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    throw new ArgumentException("Название категории не может быть пустым");
                }
            }
        }
        public virtual void ValidateOperations(List<Operation> operations)
        {
            foreach (var operation in operations)
            {
                if (operation.Amount <= 0)
                {
                    throw new ArgumentException("Сумма операции должна быть положительной");
                }
            }
        }
    }
}
