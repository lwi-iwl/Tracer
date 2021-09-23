using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    public class Method {
        private string _name;
        private string _className;
        private int _time;
        private List<Method> _methods = new List<Method>();

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }


        public string ClassName
        {
            get
            {
                return _className;
            }

            set
            {
                _className = value;
            }
        }

        public int Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

        public List<Method> Methods
        {
            get
            {
                return _methods;
            }

            set
            {
                _methods = value;
            }
        }
    }
}
