namespace BusinessPipeline.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CSharp.RuntimeBinder;
    using Xunit;

    public class PipelineContextFacts
    {
        private readonly dynamic _context;

        public PipelineContextFacts()
        {
            _context = new PipelineContext();
            _context.Foo = "bar";
            _context.Svada = "dada";
            
        }

        [Fact]
        public void Should_work_as_generic_property_bag()
        {
            var bar = _context.Foo;

            Assert.Equal("bar", bar);
            Assert.Equal("dada", _context.Svada);
        }

        [Fact]
        public void Should_throw_on_querying_for_unset_properties()
        {
            Assert.Throws<RuntimeBinderException>(() => _context.Baz);
        }

        [Fact]
        public void Should_be_able_to_enumerate_property_bag()
        {
            var setProperties = ((IEnumerable<KeyValuePair<string, object>>) _context).OrderBy(x => x.Key).ToList();

            Assert.Equal(2, setProperties.Count);

            Assert.Equal("Foo", setProperties[0].Key);
            Assert.Equal("bar", setProperties[0].Value);

            Assert.Equal("Svada", setProperties[1].Key);
            Assert.Equal("dada", setProperties[1].Value);
        }
    }
}