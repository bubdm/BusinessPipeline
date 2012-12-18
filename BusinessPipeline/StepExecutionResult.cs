namespace BusinessPipeline
{
    public class StepExecutionResult
    {
        public PipelineStep Step { get; private set; }
        public bool Success { get; private set; }

        public StepExecutionResult(PipelineStep step, bool success)
        {
            Step = step;
            Success = success;
        }
    }
}