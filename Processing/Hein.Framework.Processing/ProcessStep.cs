using System.Threading.Tasks;

namespace Hein.Framework.Processing
{
    public abstract class ProcessStep<T> where T : IProcessContext
    {
        public string StepName { get; }
        protected ProcessStep(string stepName)
        {
            StepName = stepName;
        }

        /// <summary>
        /// Runs validation on the processcontext to determine if the current step should run or not
        /// <para>Default is set to true aka, always execute this step</para>
        /// </summary>
        public virtual Task<bool> ShouldExecuteAsync(T context) => Task.FromResult(true);

        /// <summary>
        /// Runs/applies logic to the processcontext.  if it returns 'true' it will proceed to the next step. if 'false' it breaks out of the execution of steps
        /// </summary>
        public abstract Task<bool> ExecuteAsync(T context);
    }
}
