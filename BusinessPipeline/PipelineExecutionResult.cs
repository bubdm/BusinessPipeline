namespace BusinessPipeline
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class PipelineExecutionResult
    {
        public ReadOnlyCollection<StepExecutionResult> StepResults { get; private set; }

        public bool Succeeded
        {
            get { return StepResults.All(x => x.Success); }
        }

        public PipelineExecutionResult(IList<StepExecutionResult> stepExecutionResults)
        {
            StepResults = new ReadOnlyCollection<StepExecutionResult>(stepExecutionResults);
        }
    }
}