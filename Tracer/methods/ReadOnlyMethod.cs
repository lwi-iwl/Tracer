using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class ReadOnlyMethod
    {
        private string _name;
        private string _className;
        private int _time;
        private IReadOnlyCollection<ReadOnlyMethod> _readOnlyMethods;

        public ReadOnlyMethod(string name, string className, int time, List<ReadOnlyMethod> methods)
        {
            this._readOnlyMethods = new ReadOnlyCollection<ReadOnlyMethod>(methods);
            this._name = name;
            this._className = className;
            this._time = time;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string ClassName
        {
            get
            {
                return _className;
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
                return _readOnlyMethods;
            }
        }
    }
}
