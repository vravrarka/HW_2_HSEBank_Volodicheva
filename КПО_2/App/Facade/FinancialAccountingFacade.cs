using Bank.App.Factory;
using Bank.App.Strategies;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Facade
{
    public class FinancialAccountingFacade
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly IBalanceRecalculationStrategy _balanceStrategy;
        public FinancialAccountingFacade(BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade,
            AnalyticsFacade analyticsFacade,
            IBalanceRecalculationStrategy balanceStrategy)
        {
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
            _analyticsFacade = analyticsFacade;
            _balanceStrategy = balanceStrategy;
        }
        public void CreateAccount(string name, int initialBalance = 0)
        {
            var creator = new BankAccountCreator(name, initialBalance);
            var account = (BankAccount)creator.CreateObject();
            _bankAccountFacade.AddAccount(account);
        }
        public void DeleteAccount(int accountId)
        {
            _bankAccountFacade.DeleteAccount(accountId);
            Console.WriteLine($"Счет с ID {accountId} удален через фасад");
        }
        public void CreateCategory(CategoryType type, string name)
        {
            var creator = new CategoryCreator(type, name);
            var category = (Category)creator.CreateObject();
            _categoryFacade.AddCategory(category);
        }
        public void CreateOperation(CategoryType type, int bankAccountId, int amount,
                                  DateOnly date, int categoryId, string? description = null)
        {
            var creator = new OperationCreator(type, bankAccountId, amount, date, categoryId, description);
            var operation = (Operation)creator.CreateObject();
            _operationFacade.AddOperation(operation);
        }
        public void CancelOperation(int operationId)
        {
            var operationToCancel = _operationFacade.GetOperation(operationId);
            if (operationToCancel != null)
            {
                var account = _bankAccountFacade.GetBankAccount(operationToCancel.BankAccountId);
                if (account == null)
                {
                    Console.WriteLine($"Ошибка: Счет с ID {operationToCancel.BankAccountId} не найден");
                    return;
                }
                if (operationToCancel.Type == CategoryType.Income)
                {
                    account.Balance -= operationToCancel.Amount;
                }
                else if (operationToCancel.Type == CategoryType.Expense)
                {
                    account.Balance += operationToCancel.Amount;
                }
                _operationFacade.DeleteOperation(operationToCancel);

                Console.WriteLine($"Операция с ID {operationId} отменена");
            }
            else
            {
                Console.WriteLine($"Ошибка: Операция с ID {operationId} не найдена");
            }
        }
        public Dictionary<string, int> GetIncomeOperationsByCategory(int accountId)
        {
            return _analyticsFacade.GetIncomeOperationsByCategory(accountId);
        }

        public Dictionary<string, int> GetExpenseOperationsByCategory(int accountId)
        {
            return _analyticsFacade.GetExpenseOperationsByCategory(accountId);
        }

        public int CalculateBalanceDifference(int accountId, DateOnly start, DateOnly end)
        {
            return _analyticsFacade.CalculateBalanceDifference(accountId, start, end);
        }
        public Dictionary<string, int> GetCategorySummary(int accountId)
        {
            return _analyticsFacade.GetCategorySummary(accountId);
        }

        public int GetTotalIncome(int accountId)
        {
            return _analyticsFacade.GetTotalIncome(accountId);
        }

        public int GetTotalExpenses(int accountId)
        {
            return _analyticsFacade.GetTotalExpenses(accountId);
        }
        public void VerifyAndRecalculateBalance(int accountId)
        {
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                Console.WriteLine($"Ошибка: Счет с ID {accountId} не найден");
                return;
            }

            var operations = _operationFacade.GetOperationsByAccount(accountId);
            var calculatedBalance = _balanceStrategy.RecalculateBalance(accountId, operations);
            var currentBalance = account.Balance;

            Console.WriteLine($"Проверка целостности счета {accountId}:");
            Console.WriteLine($"  Текущий баланс: {currentBalance}");
            Console.WriteLine($"  Рассчитанный баланс: {calculatedBalance}");

            if (currentBalance != calculatedBalance)
            {
                Console.WriteLine($"  Обнаружено несоответствие! Разница: {calculatedBalance - currentBalance}");
                Console.WriteLine("  Выполняется автоматический пересчет баланса...");

                account.Balance = calculatedBalance;
                Console.WriteLine($"  Баланс обновлен: {calculatedBalance}");
            }
            else
            {
                Console.WriteLine("  Целостность данных подтверждена");
            }
        }
        public void VerifyAllAccounts()
        {
            var accounts = _bankAccountFacade.GetAllBankAccounts();
            Console.WriteLine($"Проверка целостности всех счетов ({accounts.Count()} счетов)...");

            foreach (var account in accounts)
            {
                VerifyAndRecalculateBalance(account.Id);
            }
        }
        public void ForceRecalculateBalance(int accountId)
        {
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                Console.WriteLine($"Ошибка: Счет с ID {accountId} не найден");
                return;
            }

            var operations = _operationFacade.GetOperationsByAccount(accountId);
            var newBalance = _balanceStrategy.RecalculateBalance(accountId, operations);

            account.Balance = newBalance;
            Console.WriteLine($"Баланс счета {accountId} пересчитан: {newBalance}");
        }

    }
}
