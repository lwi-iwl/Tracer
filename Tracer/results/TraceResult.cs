using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tracer.Threads;

namespace Tracer.Results
{
    public class TraceResult
    {
        private IReadOnlyList<ReadOnlyThread> _readOnlyThreads;

        public TraceResult(List<ReadOnlyThread> readOnlyThreads)
        {
            this._readOnlyThreads = new ReadOnlyCollection<ReadOnlyThread>(readOnlyThreads);
        }

        public IReadOnlyList<ReadOnlyThread> ReadOnlyThreads{
            get{
                return _readOnlyThreads;    
            }
        }

    }
}
