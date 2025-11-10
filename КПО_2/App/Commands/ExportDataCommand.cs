using Bank.App.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Commands
{
    public class ExportDataCommand : Command
    {
        private readonly FinancialAccountingFacade _facade;
        private readonly string _format;
        private readonly string _filePath;
        public ExportDataCommand(FinancialAccountingFacade facade, string format, string filePath)
        {
            _facade = facade;
            _format = format.ToLower();
            _filePath = filePath;
        }
        public override void Execute()
        {
            var exportFacade = new ExportFacade(_facade);
            switch (_format)
            {
                case "csv":
                    exportFacade.ExportToCsv(_filePath);
                    break;
                case "json":
                    exportFacade.ExportToJson(_filePath);
                    break;
                case "yaml":
                    exportFacade.ExportToYaml(_filePath);
                    break;
                default:
                    Console.WriteLine($"Неизвестный формат экспорта: {_format}");
                    break;
            }
        }
        public override void Undo() { }
    }
}
