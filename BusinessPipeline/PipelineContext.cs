namespace BusinessPipeline
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;

    public class PipelineContext : DynamicObject, IEnumerable<KeyValuePair<string,object>>
    {
        private readonly Dictionary<string, object> _objects = new Dictionary<string, object>(); 

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _objects.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _objects[binder.Name] = value;
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}