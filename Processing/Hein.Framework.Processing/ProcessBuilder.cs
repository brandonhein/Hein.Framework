using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.Framework.Processing
{
    /// <summary>
    /// Process Builder. Using a builder pattern to create your 'dynamic' process to execute
    /// </summary>
    public class ProcessBuilder<T> where T : IProcessContext
    {
        private readonly Dictionary<int, ProcessStep<T>> _steps;
        private int _order;
        private readonly T _context;
        public T Context { get { return _context; } }
        /// <summary>
        /// Boolean Flag to let you know the process ran from start to finish
        /// </summary>
        public bool SuccessfullyRan { get; private set; }
        /// <summary>
        /// The Step Name if the process stopped, because of a false result when a step executed
        /// </summary>
        public string ProcessStoppedAt { get; private set; }
        /// <summary>
        /// Performance data on execution per step.
        /// </summary>
        public ProcessPerformance Performance { get; private set; }

        /// <summary>
        /// Process Builder. Using a builder pattern to create your 'dynamic' process to execute
        /// </summary>
        public ProcessBuilder(T context)
        {
            _context = context;
            _steps = new Dictionary<int, ProcessStep<T>>();
        }

        /// <summary>
        /// Execute all process steps
        /// </summary>
        public async Task ExecuteAsync()
        {
            var stepPerformances = new List<ProcessStepPerformance>();
            var orderedSteps = _steps.OrderBy(x => x.Key);
            foreach (var current in orderedSteps)
            {
                var step = current.Value;
                if (string.IsNullOrEmpty(ProcessStoppedAt))
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    var shouldExecute = await step.ShouldExecuteAsync(_context);
                    if (shouldExecute)
                    {
                        var shouldContinue = await step.ExecuteAsync(_context);
                        if (!shouldContinue)
                        {
                            ProcessStoppedAt = step.StepName;
                        }
                    }

                    stopWatch.Stop();

                    stepPerformances.Add(new ProcessStepPerformance(
                        current.Key, 
                        step.StepName, 
                        shouldExecute ? ProcessStepResult.Executed : ProcessStepResult.Skipped, 
                        stopWatch.ElapsedMilliseconds));
                }
                else
                {
                    stepPerformances.Add(new ProcessStepPerformance(current.Key, step.StepName, ProcessStepResult.Excluded, 0));
                }
            }

            SuccessfullyRan = string.IsNullOrEmpty(ProcessStoppedAt);
            Performance = new ProcessPerformance(_context.ProcessName, stepPerformances);
        }

        /// <summary>
        /// Add a step to the process.  Add steps in the order you wish for them to run
        /// </summary>
        public ProcessBuilder<T> AddStep(ProcessStep<T> step)
        {
            _order++;
            _steps.Add(_order, step);
            return this;
        }
    }
}
