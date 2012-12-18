namespace BusinessPipeline
{
    public abstract class PipelineStep
    {     
        public abstract StepExecutionResult Execute(dynamic context);

        protected StepExecutionResult Ok()
        {
            return new StepExecutionResult(this, true);
        }

        protected StepExecutionResult Fail()
        {
            return new StepExecutionResult(this , false);
        }
    }
}