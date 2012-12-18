namespace BusinessPipeline
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Collections.Generic;

    public class Pipeline
    {
        private readonly List<PipelineStep> _steps; 

        public IReadOnlyCollection<PipelineStep> Steps
        {
            get { return new ReadOnlyCollection<PipelineStep>(_steps); }
        }

        public Pipeline(params PipelineStep[] pipelineSteps)
        {
            _steps = new List<PipelineStep>(pipelineSteps);
            if (Steps.Contains(null))
            {
                throw new InvalidOperationException("The pipeline cannot make use of null steps.");
            }
        }

        public void AddStep(PipelineStep pipelineStep)
        {
            if (pipelineStep == null) throw new ArgumentNullException("pipelineStep");

            _steps.Add(pipelineStep);
        }

        public virtual PipelineExecutionResult Execute(PipelineContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var stepExecutionResults = new List<StepExecutionResult>();
            foreach (var result in Steps.Select(step => step.Execute(context)))
            {
                stepExecutionResults.Add(result);
                if (false == result.Success)
                {
                    break;
                }
            }
            return new PipelineExecutionResult(stepExecutionResults);
        }
    }
}
