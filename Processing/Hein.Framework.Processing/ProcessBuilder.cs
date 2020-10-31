using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.Framework.Processing
{
    public class ProcessBuilder<T> where T : IProcessContext
    {
        private readonly Dictionary<int, ProcessStep<T>> _steps;
        private int _order;
        private readonly T _context;
        public T Context { get { return _context; } }
        public bool SuccessfullyRan { get; private set; }
        public string ProcessStoppedAt { get; private set; }

        public ProcessBuilder(T context)
        {
            _context = context;
            _steps = new Dictionary<int, ProcessStep<T>>();
        }

        public async Task ExecuteAsync()
        {
            var orderedSteps = _steps.OrderBy(x => x.Key);
            foreach (var current in orderedSteps)
            {
                var step = current.Value;
                if (await step.ShouldExecuteAsync(_context))
                {
                    var shouldContinue = await step.ExecuteAsync(_context);
                    if (!shouldContinue)
                    {
                        ProcessStoppedAt = step.StepName;
                        break;
                    }
                }
            }

            SuccessfullyRan = string.IsNullOrEmpty(ProcessStoppedAt);
        }

        public ProcessBuilder<T> AddStep(ProcessStep<T> step)
        {
            _order++;
            _steps.Add(_order, step);
            return this;
        }
    }
}
