using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Processing
{
    public class ProcessBuilder<T> where T : IProcessContext
    {
        private readonly T _context;
        private readonly Dictionary<int, ProcessStep<T>> _steps;
        private int _order;
        protected T Context { get { return _context; } }
        public bool SuccessfullyRan { get; private set; }
        public string ProcessStoppedAt { get; private set; }

        public ProcessBuilder(T context)
        {
            _context = context;
            _steps = new Dictionary<int, ProcessStep<T>>();
        }

        public void Execute()
        {
            var orderedSteps = _steps.OrderBy(x => x.Key);
            foreach (var current in orderedSteps)
            {
                var step = current.Value;
                if (step.ShouldExecute(_context))
                {
                    var shouldContinue = step.Execute(_context);
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
