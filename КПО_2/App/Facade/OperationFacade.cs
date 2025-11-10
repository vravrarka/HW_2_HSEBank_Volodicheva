using Bank.App.Repositories;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Facade
{
    public class OperationFacade
    {
        private readonly DataRepository _repository;
        private readonly BankAccountFacade _bankAccountFacade;
        public OperationFacade(BankAccountFacade bankAccountFacade)
        {
            _repository = DataRepository.Instance;
            _bankAccountFacade = bankAccountFacade;
        }
        public void AddOperation(Operation operation)
        {
            var account = _bankAccountFacade.GetBankAccount(operation.BankAccountId);
            if (account == null)
            {
                Console.WriteLine($"Ошибка: Счет с ID {operation.BankAccountId} не найден");
                return;
            }
            if (operation.Type == CategoryType.Income) { account.Balance += operation.Amount; }
            else if (operation.Type == CategoryType.Expense) { account.Balance -= operation.Amount; }
            _repository.Operations.Add(operation);
            Console.WriteLine($"Добавлена операция: {operation.Description} - {operation.Amount} ({operation.Type})");
        }
        public void DeleteOperation(Operation operation)
        {
            _repository.Operations.Remove(operation);
        }
        public IEnumerable<Operation> GetAllOperations()
        {
            return _repository.Operations;
        }
        public Operation GetOperation(int id)
        {
            return _repository.Operations.FirstOrDefault(o => o.Id == id);
        }
        public IEnumerable<Operation> GetOperationsByAccount(int accountId)
        {
            return _repository.Operations.Where(o => o.BankAccountId == accountId);
        }
        public void DisplayAllOperations()
        {
            Console.WriteLine("\nВсе операции: \n");
            foreach (var operation in _repository.Operations)
            {
                Console.WriteLine($"ID: {operation.Id}, Тип: {operation.Type}, Сумма: {operation.Amount}, Дата: {operation.Date:dd.MM.yyyy}, Описание: {operation.Description}");
            }
        }
    }
}