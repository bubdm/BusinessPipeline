namespace BusinessPipeline
{
    using System;
    using System.Collections.Generic;

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

        public virtual IEnumerable<StepExecutionResult> Execute(PipelineContext context)
        {
            foreach (var step in _steps)
            {
                var result = step.Execute(context);
                //if (result.Halt)
            }
            return new List<StepExecutionResult>();
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
        private readonly bool _haltOnError;
        private PipelineStep _nextStep;

        protected PipelineStep(bool haltOnError)
        {
            _haltOnError = haltOnError;
        }

        public StepExecutionResult Execute(PipelineContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class StepExecutionResult
    {
        public bool Halt { get; private set; }

        public StepExecutionResult(bool halt)
        {
            Halt = halt;
        }
    }
}
