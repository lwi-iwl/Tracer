using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tracer.Methods
{
    public class ReadOnlyMethod
    {
        private string _name;
        private string _className;
        private int _time;
        private IReadOnlyCollection<ReadOnlyMethod> _readOnlyMethods;

        public ReadOnlyMethod(string name, string className, int time, List<ReadOnlyMethod> methods)
        {
            _readOnlyMethods = new ReadOnlyCollection<ReadOnlyMethod>(methods);
            _name = name;
            _className = className;
            _time = time;
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
