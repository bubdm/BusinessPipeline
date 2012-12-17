namespace BusinessPipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Pipeline
    {
        private readonly List<PipelineStep> _steps;

        public Pipeline(params PipelineStep[] pipelineSteps)
        {
            _steps = new List<PipelineStep>(pipelineSteps);
            if (_steps.Contains(null))
            {
                throw new InvalidOperationException("The pipeline cannot make use of null steps.");
            }
        }

        public virtual PipelineExecutionResult Execute(PipelineContext context)
        {
            List<StepExecutionResult> stepExecutionResults = new List<StepExecutionResult>();
            StepExecutionResult result = null;
            _steps.TakeWhile(x => 
                {
                    result = x.Execute(context);
                    stepExecutionResults.Add(result);
                    return result.Success;
                });
            return new PipelineExecutionResult(stepExecutionResults);
        }
    }

    public class PipelineContext
    {
        protected readonly Dictionary<string, object> Properties = new Dictionary<string, object>();

        public void SetProperty(string name, object property)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (property == null) throw new ArgumentNullException("property");

            Properties[name] = property;
        }
    }

    public abstract class PipelineStep
    {     
        public abstract StepExecutionResult Execute(PipelineContext context);
    }

    public class PipelineExecutionResult
    {
        public ReadOnlyCollection<StepExecutionResult> StepResults { get; private set; }

        public PipelineExecutionResult(IList<StepExecutionResult> stepExecutionResults)
        {
            StepResults = new ReadOnlyCollection<StepExecutionResult>(stepExecutionResults);
        }
    }

    public class StepExecutionResult
    {
        public bool Success { get; private set; }

        public StepExecutionResult(PipelineStep step, bool success)
        {
            Success = success;
        }
    }
}
