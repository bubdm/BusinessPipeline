namespace BusinessPipeline.Tests
{
    using System;
    using Xunit;

    public class PipelineFacts
    {
        private readonly dynamic _context;
        private readonly MockStep1 _step1;
        private readonly MockStep2 _step2;
        private readonly FailingMockStep3 _failingMockStep3;
        private readonly Pipeline _emptyPipeline;
        private readonly Pipeline _nonEmptyPipeline;

        public PipelineFacts()
        {
            _context = new PipelineContext();

            _step1 = new MockStep1();
            _step2 = new MockStep2();
            _failingMockStep3 = new FailingMockStep3();
            
            _emptyPipeline = new Pipeline();
            _nonEmptyPipeline = new Pipeline(_step1, _step2);
        }

        [Fact]
        public void Initializing_a_pipeline_with_no_steps_are_ok()
        {
            Assert.NotNull(_emptyPipeline);
        }

        [Fact]
        public void Initializing_a_pipeline_with_some_steps_are_ok()
        {
            Assert.NotNull(_nonEmptyPipeline);
        }

        [Fact]
        public void Initiating_a_pipeline_with_one_or_more_null_steps_should_throw()
        {
            Assert.Throws<InvalidOperationException>(() => new Pipeline(null, null));
        }

        [Fact]
        public void Executing_the_non_empty_pipeline_should_succeed()
        {
            
            var result = _nonEmptyPipeline.Execute(_context);
            
            Assert.True(result.Succeeded);
            Assert.Equal(123, _context.Value1);
            Assert.Equal(123, _step2.ReadValueFromContext);
        }

        [Fact]
        public void Executing_a_pipeline_with_an_failing_step_should_fail()
        {
            _nonEmptyPipeline.AddStep(_failingMockStep3);
            var result = _nonEmptyPipeline.Execute(_context);
            Assert.False(result.Succeeded);
        }
    }

    public class MockStep1 : PipelineStep
    {
        public override StepExecutionResult Execute(dynamic context)
        {
            context.Value1 = 123;
            return Ok();
        }
    }

    internal class MockStep2 : PipelineStep
    {
        public int ReadValueFromContext { get; private set; }

        public override StepExecutionResult Execute(dynamic context)
        {
            ReadValueFromContext = context.Value1;
            return Ok();
        }
    }

    internal class FailingMockStep3 : PipelineStep
    {
        public override StepExecutionResult Execute(dynamic context)
        {
            return Fail();
        }
    }
}