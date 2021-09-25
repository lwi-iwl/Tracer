using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tracer.Methods;

namespace Tracer.Threads
{
    public class ReadOnlyThread
    {
        private int _id;
        private int _time;
        private IReadOnlyCollection<ReadOnlyMethod> _methods;

        public ReadOnlyThread(int id, int time, List<ReadOnlyMethod> methods) 
        {
            this._methods = new ReadOnlyCollection<ReadOnlyMethod>(methods);
            this._id = id;
            this._time = time;
        }
        public int Id
        {
            get
            {
                return _id;
            }
        }

        public int Time
        {
            get
            {
                return _time;
            }
        }

        public IReadOnlyCollection<ReadOnlyMethod> ReadOnlyMethods
        {
            get
            {
                return _methods;
            }
        }
    }
}
