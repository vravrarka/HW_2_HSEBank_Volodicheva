using Bank.App.Commands;
using Bank.App.Facade;
using Bank.App.Decorators;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq.Expressions;

namespace Bank.ConsoleManager
{
    public class ConsoleMenuManager
    {
        private readonly FinancialAccountingFacade _financialFacade;
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;
        private readonly ExportFacade _exportFacade;
        private readonly ImportFacade _importFacade;
        public ConsoleMenuManager(
            FinancialAccountingFacade financialFacade,
            AnalyticsFacade analyticsFacade,
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade,
            ExportFacade exportFacade,
            ImportFacade importFacade)
        {
            _financialFacade = financialFacade;
            _analyticsFacade = analyticsFacade;
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
            _exportFacade = exportFacade;
            _importFacade = importFacade;
        }
        public void ShowMainMenu()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("    ВШЭ-Банк: Система финансового учета");
                    Console.WriteLine("1.Управление счетами");
                    Console.WriteLine("2.Управление категориями");
                    Console.WriteLine("3.Управление операциями");
                    Console.WriteLine("4.Аналитика и отчеты");
                    Console.WriteLine("5.Импорт/Экспорт данных");
                    Console.WriteLine("6.Выйти из программы");
                    Console.Write("Выберите пункт меню: ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            ShowBankAccountsMenu();
                            break;
                        case "2":
                            ShowCategoriesMenu();
                            break;
                        case "3":
                            ShowOperationsMenu();
                            break;
                        case "4":
                            ShowAnalyticsMenu();
                            break;
                        case "5":
                            ShowImportExportMenu();
                            break;
                        case "6":
                            Console.WriteLine("Программа завершена!");
                            return;
                        default:
                            ShowError("Некорректный выбор, попробуйте снова");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Возникла ошибка: {ex.Message}");
            }
        }
        private void ShowBankAccountsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("    Управление счетами");
                _bankAccountFacade.DisplayAllAccounts();
                Console.WriteLine("\n1.Создать счет");
                Console.WriteLine("2.Удалить счет");
                Console.WriteLine("3.Назад");
                Console.Write("Выберите пункт меню: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateAccountWithCommand();
                        break;
                    case "2":
                        DeleteBankAccount();
                        break;
                    case "3":
                        return;
                    default:
                        ShowError("Некорректный выбор, попробуйте снова");
                        WaitForContinue();
                        break;
                }
            }
        }
        private void ShowCategoriesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("   Управление категориями");
                _categoryFacade.DisplayAllCategories();
                Console.WriteLine("\n1.Создать категорию доходов");
                Console.WriteLine("2.Создать категорию расходов");
                Console.WriteLine("3.Назад");
                Console.Write("Выберите пункт меню: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateCategory(CategoryType.Income);
                        break;
                    case "2":
                        CreateCategory(CategoryType.Expense);
                        break;
                    case "3":
                        return;
                    default:
                        ShowError("Некорректный выбор, попробуйте снова");
                        break;
                }
            }
        }
        private void ShowOperationsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("    Управление операциями ");
                _operationFacade.DisplayAllOperations();
                Console.WriteLine("\n1.Создать операцию дохода");
                Console.WriteLine("2.Создать операцию расхода");
                Console.WriteLine("3.Отменить операцию");
                Console.WriteLine("4.Назад");
                Console.Write("Выберите пункт меню: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateOperation(CategoryType.Income);
                        break;
                    case "2":
                        CreateOperation(CategoryType.Expense);
                        break;
                    case "3":
                        CancelOperation();
                        break;
                    case "4":
                        return;
                    default:
                        ShowError("Некорректный выбор, попробуйте снова");
                        break;
                }
            }
        }
        private void ShowAnalyticsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("    Аналитика и отчеты");
                Console.WriteLine("1.Разница доходов и расходов за период");
                Console.WriteLine("2.Группировка доходов по категориям");
                Console.WriteLine("3.Группировка расходов по категориям");
                Console.WriteLine("4.Общая сводка по категориям");
                Console.WriteLine("5.Общая статистика по счету");
                Console.WriteLine("6. Проверить целостность данных счета"); 
                Console.WriteLine("7. Проверить все счета");
                Console.WriteLine("8. Принудительный пересчет баланса");
                Console.WriteLine("9.Назад");
                Console.Write("Выберите пункт меню: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowBalanceDifference();
                        break;
                    case "2":
                        ShowIncomeByCategory();
                        break;
                    case "3":
                        ShowExpensesByCategory();
                        break;
                    case "4":
                        ShowCategorySummary();
                        break;
                    case "5":
                        ShowTotalStatistics();
                        break;
                    case "6": 
                        VerifyAccountIntegrity();
                        break;
                    case "7": 
                        VerifyAllAccountsIntegrity();
                        break;
                    case "8": 
                        ForceRecalculateBalance();
                        break;
                    case "9":
                        return;
                    default:
                        ShowError("Некорректный выбор, попробуйте снова");
                        break;
                }

                WaitForContinue();
            }
        }
        private void VerifyAccountIntegrity()
        {
            Console.Write("Введите ID счета для проверки: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Неверный ID счета");
                return;
            }

            _financialFacade.VerifyAndRecalculateBalance(accountId);
        }
        private void VerifyAllAccountsIntegrity()
        {
            _financialFacade.VerifyAllAccounts();
        }

        private void ForceRecalculateBalance()
        {
            Console.Write("Введите ID счета для принудительного пересчета: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Неверный ID счета");
                return;
            }

            _financialFacade.ForceRecalculateBalance(accountId);
        }

        private void ShowImportExportMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("    Импорт/Экспорт данных");
                Console.WriteLine("1.Экспорт в CSV");
                Console.WriteLine("2.Экспорт в JSON");
                Console.WriteLine("3.Экспорт в YAML");
                Console.WriteLine("4.Импорт данных");
                Console.WriteLine("5.Назад");
                Console.Write("Выберите пункт меню: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ExportData("csv");
                        break;
                    case "2":
                        ExportData("json");
                        break;
                    case "3":
                        ExportData("yaml");
                        break;
                    case "4":
                        ImportData();
                        break;
                    case "5":
                        return;
                    default:
                        ShowError("Некорректный выбор, попробуйте снова");
                        WaitForContinue();
                        break;
                }
            }
        }
        private void ExecuteTimedCommand(Command command)
        {
            var timedCommand = new TimedCommandDecorator(command);
            timedCommand.Execute();
            WaitForContinue();
        }
        private void CreateAccountWithCommand()
        {
            Console.Write("Введите название счета: ");
            var name = Console.ReadLine()?.Trim();

            Console.Write("Введите начальный баланс: ");
            if (int.TryParse(Console.ReadLine(), out int balance))
            {
                var command = new CreateAccountCommand(_financialFacade, name ?? "Новый счет", balance);
                ExecuteTimedCommand(command);
            }
            else
            {
                ShowError("Некорректная сумма баланса");
                WaitForContinue();
            }
        }
        
        private void DeleteBankAccount()
        {
            Console.Write("Введите ID счета для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                _financialFacade.DeleteAccount(accountId);
            }
            else
            {
                ShowError("Некорректный ID счета");
            }

            WaitForContinue();
        }
        private void CreateCategory(CategoryType type)
        {
            Console.Write("Введите название категории: ");
            var name = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(name))
            {
                _financialFacade.CreateCategory(type, name);
            }
            else
            {
                ShowError("Название категории не может быть пустым");
            }

            WaitForContinue();
        }
        private void CreateOperation(CategoryType type)
        {
            try
            {
                Console.Write("Введите ID счета: ");
                if (!int.TryParse(Console.ReadLine(), out int accountId))
                {
                    ShowError("Некорректный ID счета");
                    return;
                }

                Console.Write("Введите сумму: ");
                if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
                {
                    ShowError("Некорректная сумма");
                    return;
                }

                Console.Write("Введите ID категории: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    ShowError("Некорректный ID категории");
                    return;
                }

                Console.Write("Введите описание (опционально): ");
                var description = Console.ReadLine()?.Trim();

                var date = DateOnly.FromDateTime(DateTime.Now);

                _financialFacade.CreateOperation(type, accountId, amount, date, categoryId, description);
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при создании операции: {ex.Message}");
            }

            WaitForContinue();
        }
        private void CancelOperation()
        {
            Console.Write("Введите ID операции для отмены: ");
            if (int.TryParse(Console.ReadLine(), out int operationId))
            {
                _financialFacade.CancelOperation(operationId);
            }
            else
            {
                ShowError("Некорректный ID операции");
            }

            WaitForContinue();
        }
        private void ShowBalanceDifference()
        {
            try
            {
                Console.Write("Введите ID счета: ");
                if (!int.TryParse(Console.ReadLine(), out int accountId))
                {
                    ShowError("Некорректный ID счета");
                    return;
                }
                DateOnly start;
                DateOnly end;
                Console.Write("Введите начальную дату (гггг-мм-дд) или нажмите Enter для месяца назад: ");
                var startInput = Console.ReadLine();
                if (string.IsNullOrEmpty(startInput))
                {
                    start = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
                }
                else
                {
                    if (!DateOnly.TryParse(startInput, out start))
                    {
                        ShowError("Некорректный формат даты. Используйте формат гггг-мм-дд");
                        return;
                    }
                }

                Console.Write("Введите конечную дату (гггг-мм-дд) или нажмите Enter для сегодня: ");
                var endInput = Console.ReadLine();
                if (string.IsNullOrEmpty(endInput))
                {
                    end = DateOnly.FromDateTime(DateTime.Now);
                }
                else
                {
                    if (!DateOnly.TryParse(endInput, out end))
                    {
                        ShowError("Некорректный формат даты. Используйте формат гггг-мм-дд");
                        return;
                    }
                }
                if (start > end)
                {
                    ShowError("Начальная дата не может быть позже конечной даты");
                    return;
                }
                _financialFacade.CalculateBalanceDifference(accountId, start, end);
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при анализе баланса: {ex.Message}");
            }
        }
        private void ShowIncomeByCategory()
        {
            Console.Write("Введите ID счета: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Некорректный ID счета");
                return;
            }
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                ShowError($"Счет с ID {accountId} не существует");
                return;
            }
            _financialFacade.GetIncomeOperationsByCategory(accountId);
        }
        private void ShowExpensesByCategory()
        {
            Console.Write("Введите ID счета: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Некорректный ID счета");
                return;
            }
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                ShowError($"Счет с ID {accountId} не существует");
                return;
            }
            _financialFacade.GetExpenseOperationsByCategory(accountId);
        }
        private void ShowCategorySummary()
        {
            Console.Write("Введите ID счета: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Некорректный ID счета");
                return;
            }
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                ShowError($"Счет с ID {accountId} не существует");
                return;
            }
            _financialFacade.GetCategorySummary(accountId);
        }
        private void ShowTotalStatistics()
        {
            Console.Write("Введите ID счета: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                ShowError("Некорректный ID счета");
                return;
            }
            var account = _bankAccountFacade.GetBankAccount(accountId);
            if (account == null)
            {
                ShowError($"Счет с ID {accountId} не существует");
                return;
            }
            var totalIncome = _financialFacade.GetTotalIncome(accountId);
            var totalExpenses = _financialFacade.GetTotalExpenses(accountId);
            var balance = totalIncome - totalExpenses;
            Console.WriteLine($"\nОбщая статистика по счету {accountId}:");
            Console.WriteLine($"  Общий доход: {totalIncome}");
            Console.WriteLine($"  Общий расход: {totalExpenses}");
            Console.WriteLine($"  Текущий баланс: {balance}");
        }
        private void ExportData(string format)
        {
            Console.Write("Введите путь для сохранения: ");
            var path = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var command = new ExportDataCommand(_financialFacade, format, path);
                    ExecuteTimedCommand(command);
                    ShowSuccess("Данные успешно экспортированы!");
                }
                catch (Exception ex)
                {
                    ShowError($"Ошибка при экспорте: {ex.Message}");
                }
            }
            else
            {
                ShowError("Путь не может быть пустым");
                WaitForContinue();
            }
        }
        private void ImportData()
        {

            try
            {
                Console.Clear();
                Console.WriteLine("    Импорт данных");
                Console.WriteLine("1.Импорт счетов");
                Console.WriteLine("2.Импорт категорий");
                Console.WriteLine("3.Импорт операций");
                Console.WriteLine("4.Назад");
                Console.Write("Выберите тип данных для импорта: ");

                var choice = Console.ReadLine();
                if (choice == "4") return;

                Console.Write("Введите путь к файлу: ");
                var path = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    ShowError("Файл не существует или путь указан неверно");
                    WaitForContinue();
                    return;
                }
                string dataType = choice switch
                {
                    "1" => "accounts",
                    "2" => "categories",
                    "3" => "operations",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(dataType))
                {
                    var command = new ImportDataCommand(_financialFacade, _importFacade, path, dataType);
                    ExecuteTimedCommand(command);
                }
                else
                {
                    ShowError("Некорректный выбор типа данных");
                    WaitForContinue();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при импорте: {ex.Message}");
            }
        }

        private void WaitForContinue()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void ShowError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

