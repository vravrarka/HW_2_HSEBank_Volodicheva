using Bank.App.Factory;
using Bank.App.Repositories;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Facade
{
    public class AnalyticsFacade
    {
        private readonly DataRepository _repository;
        public AnalyticsFacade()
        {
            _repository = DataRepository.Instance;
        }
        public int CalculateBalanceDifference(int accountId, DateOnly start, DateOnly end)
        {
            var operations = _repository.Operations;
            var periodOperations = operations.Where(o =>
                o.BankAccountId == accountId &&
                o.Date >= start &&
                o.Date <= end);
            int totalIncome = periodOperations
                .Where(o => o.Type == CategoryType.Income)
                .Sum(o => (int)o.Amount);
            int totalExpenses = periodOperations
                .Where(o => o.Type == CategoryType.Expense)
                .Sum(o => (int)o.Amount);
            int difference = totalIncome - totalExpenses;
            Console.WriteLine($"Разница доходов/расходов для счета {accountId} за период {start} - {end}: {difference}");
            Console.WriteLine($"  Доходы: {totalIncome}, Расходы: {totalExpenses}");
            return difference;
        }
        public Dictionary<string, int> GetIncomeOperationsByCategory(int accountId)
        {
            var operations = _repository.Operations;
            var categories = _repository.Categories;
            var incomeOperations = operations
                .Where(o => o.BankAccountId == accountId && o.Type == CategoryType.Income);

            var incomeSummary = incomeOperations
                .GroupBy(o => o.CategoryId)
                .ToDictionary(
                    group => categories.FirstOrDefault(c => c.Id == group.Key)?.Name ?? "Неизвестная категория",
                    group => (int)group.Sum(o => o.Amount)
                )
                .OrderBy(x => x.Key) 
                .ToDictionary(x => x.Key, x => x.Value);
            Console.WriteLine($"\nОперации доходов для счета {accountId}:");
            if (incomeSummary.Any())
            {
                foreach (var (categoryName, amount) in incomeSummary)
                {
                    Console.WriteLine($"  {categoryName}: {amount}");
                }
            }
            else
            {
                Console.WriteLine("  нет операций доходов");
            }

            return incomeSummary;
        }
        public Dictionary<string, int> GetExpenseOperationsByCategory(int accountId)
        {
            var operations = _repository.Operations;
            var categories = _repository.Categories;
            var expenseOperations = operations
                .Where(o => o.BankAccountId == accountId && o.Type == CategoryType.Expense);

            var expenseSummary = expenseOperations
                .GroupBy(o => o.CategoryId)
                .ToDictionary(
                    group => categories.FirstOrDefault(c => c.Id == group.Key)?.Name ?? "Неизвестная категория",
                    group => (int)group.Sum(o => o.Amount)
                )
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
            Console.WriteLine($"\nОперации расходов для счета {accountId}:");
            if (expenseSummary.Any())
            {
                foreach (var (categoryName, amount) in expenseSummary)
                {
                    Console.WriteLine($"  {categoryName}: {amount}");
                }
            }
            else
            {
                Console.WriteLine("  нет операций расходов");
            }

            return expenseSummary;
        }
        public Dictionary<string, int> GetCategorySummary(int accountId)
        {
            var incomeSummary = GetIncomeOperationsByCategory(accountId);
            var expenseSummary = GetExpenseOperationsByCategory(accountId);
            var combinedSummary = new Dictionary<string, int>();
            foreach (var (categoryName, amount) in incomeSummary)
            {
                combinedSummary[$"Доход - {categoryName}"] = amount;
            }
            foreach (var (categoryName, amount) in expenseSummary)
            {
                combinedSummary[$"Расход - {categoryName}"] = amount;
            }
            return combinedSummary;
        }
        public int GetTotalIncome(int accountId)
        {
            var operations = _repository.Operations;
            int totalIncome = operations
                .Where(o => o.BankAccountId == accountId && o.Type == CategoryType.Income)
                .Sum(o => (int)o.Amount);
            Console.WriteLine($"Общий доход по счету {accountId}: {totalIncome}");
            return totalIncome;
        }
        public int GetTotalExpenses(int accountId)
        {
            var operations = _repository.Operations;
            int totalExpenses = operations
                .Where(o => o.BankAccountId == accountId && o.Type == CategoryType.Expense)
                .Sum(o => (int)o.Amount);
            Console.WriteLine($"Общий расход по счету {accountId}: {totalExpenses}");
            return totalExpenses;
        }
    }
}
