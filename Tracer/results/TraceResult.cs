using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    class TraceResult
    {
        private IReadOnlyList<ReadOnlyThread> _readOnlyThreads;

        public TraceResult(List<ReadOnlyThread> readOnlyThreads)
        {
            this._readOnlyThreads = new ReadOnlyCollection<ReadOnlyThread>(readOnlyThreads);
        }

        public IReadOnlyList<ReadOnlyThread> ReadOnlyThread{
            get{
                return _readOnlyThreads;    
            }
        }

    }
}
