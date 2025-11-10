using Bank.App.Commands;
using Bank.App.Facade;
using Bank.App.Repositories;
using Bank.App.Strategies;
using Bank.ConsoleManager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bank
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var menuService = serviceProvider.GetRequiredService<ConsoleMenuManager>();
            menuService.ShowMainMenu();
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BankAccountFacade>();
            services.AddSingleton<CategoryFacade>();
            services.AddSingleton<OperationFacade>();
            services.AddSingleton<AnalyticsFacade>();
            services.AddSingleton<IBalanceRecalculationStrategy, ConservativeRecalculationStrategy>();
            services.AddTransient<SimpleRecalculationStrategy>();
            services.AddTransient<ConservativeRecalculationStrategy>();
            services.AddSingleton<FinancialAccountingFacade>(provider =>
                new FinancialAccountingFacade(
                    provider.GetRequiredService<BankAccountFacade>(),
                    provider.GetRequiredService<CategoryFacade>(),
                    provider.GetRequiredService<OperationFacade>(),
                    provider.GetRequiredService<AnalyticsFacade>(),
                    provider.GetRequiredService<IBalanceRecalculationStrategy>()
                ));
            services.AddSingleton<ImportFacade>(provider =>
                new ImportFacade(provider.GetRequiredService<FinancialAccountingFacade>()));
            services.AddSingleton<ExportFacade>();
            services.AddSingleton<IBankAccountRepository, BankAccountRepositoryProxy>();
            services.AddTransient<ImportDataCommand>();
            services.AddTransient<ConsoleMenuManager>();

        }
    }
}
