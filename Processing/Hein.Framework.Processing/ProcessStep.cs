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
        /// </summary>
        public abstract bool ShouldExecute(T context);

        /// <summary>
        /// Runs/applies logic to the processcontext.  if it returns 'true' it will proceed to the next step. if 'false' it breaks out of the execution of steps
        /// </summary>
        public abstract bool Execute(T context);
    }
}
