using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.App.Commands;

namespace Bank.App.Decorators
{
    public class TimedCommandDecorator : Command
    {
        private readonly Command _decoratedCommand;
        public TimedCommandDecorator(Command decoratedCommand)
        {
            _decoratedCommand = decoratedCommand ?? throw new ArgumentNullException(nameof(decoratedCommand));
        }
        public override void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _decoratedCommand.Execute();
                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                throw;
            }
        }
        public override void Undo()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _decoratedCommand.Undo();
                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                throw;
            }
        }
    }
}
