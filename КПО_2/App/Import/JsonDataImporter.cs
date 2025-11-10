using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.App.Import
{
    public class JsonDataImporter : DataImporter
    {
        public override List<BankAccount> ParseBankAccounts(string text)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                var accounts = JsonSerializer.Deserialize<List<BankAccount>>(text, options);
                return accounts ?? new List<BankAccount>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка парсинга JSON: {ex.Message}");
                return new List<BankAccount>();
            }
        }
        public override List<Category> ParseCategories(string text)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                var categories = JsonSerializer.Deserialize<List<Category>>(text, options);
                return categories ?? new List<Category>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка парсинга JSON: {ex.Message}");
                return new List<Category>();
            }
        }
        public override List<Operation> ParseOperations(string text)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var operations = JsonSerializer.Deserialize<List<Operation>>(text, options);
                return operations ?? new List<Operation>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка парсинга JSON: {ex.Message}");
                return new List<Operation>();
            }
        }

    }
}
