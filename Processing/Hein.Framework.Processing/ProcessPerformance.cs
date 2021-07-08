using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Processing
{
    /// <summary>
    /// Process Performance class containing Process name, and a total Elapsed Milliseconds from step performances
    /// <para>Json Serializer Friendly</para>
    /// </summary>
    public class ProcessPerformance
    {
        /// <summary>
        /// Process Performance class containing Process name, and a total Elapsed Milliseconds from step performances
        /// </summary>
        public ProcessPerformance(string processName, IEnumerable<ProcessStepPerformance> stepPerformances)
        {
            ProcessName = processName;
            ElapsedMilliseconds = stepPerformances.Sum(x => x.ElapsedMilliseconds);
            StepPerformances = stepPerformances.OrderBy(x => x.StepNumber).AsEnumerable();
        }

        /// <summary>
        /// Process Name. Same as <seealso cref="IProcessContext.ProcessName"/>
        /// </summary>
        public string ProcessName { get; }
        /// <summary>
        /// Total Elapsed Milliseconds from individual Step Performances
        /// </summary>
        public long ElapsedMilliseconds { get; }
        /// <summary>
        /// Individual Step Performance
        /// </summary>
        public IEnumerable<ProcessStepPerformance> StepPerformances { get; }
    }

    /// <summary>
    /// Step Performance Metric
    /// </summary>
    public class ProcessStepPerformance
    {
        /// <summary>
        /// Step Performance Metric
        /// </summary>
        public ProcessStepPerformance(int stepNumber, string stepName, ProcessStepResult result, long elapsedMilliseconds)
        {
            StepNumber = stepNumber;
            StepName = stepName;
            Result = result;
            ElapsedMilliseconds = elapsedMilliseconds;
        }

        /// <summary>
        /// Order Number of Step
        /// </summary>
        public int StepNumber { get; }
        /// <summary>
        /// Step Name of Step.  Same as <seealso cref="ProcessStep{T}.StepName"/>
        /// </summary>
        public string StepName { get; }
        /// <summary>
        /// Result of step.  Skipped/Executed/Excluded
        /// </summary>
        public ProcessStepResult Result { get; }
        /// <summary>
        /// Elapsed Milliseconds from Step Execution Time
        /// </summary>
        public long ElapsedMilliseconds { get; }
    }

    /// <summary>
    /// Process Step Status/Result
    /// </summary>
    public enum ProcessStepResult
    {
        /// <summary>
        /// Skipped means the ShouldExecuteAsync method returned false for the step, and therefore skipped this step from executing
        /// </summary>
        Skipped,
        /// <summary>
        /// Executed means ShouldExecuteAsync method returned true for the step, and executed the step
        /// </summary>
        Executed,
        /// <summary>
        /// Excluded means a step before had ExecuteAsync method return false. Therefore, performance metrics were excluded
        /// </summary>
        Excluded
    }
}
