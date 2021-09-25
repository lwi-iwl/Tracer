
using Tracer.Results;

namespace Tracer.Serialize
{
    interface Serializator
    {
        public string Serialize(TraceResult traceResult);
    }
}
