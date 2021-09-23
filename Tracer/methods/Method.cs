using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    [XmlType("Method")]
    public class Method {
        private string _name;
        private string _className;
        private int _time;
        private List<Method> _methods = new List<Method>();

        [XmlAttribute("name")]
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

        [XmlAttribute("classname")]
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

        [XmlAttribute("time")]
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
        [XmlElement("method")]
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
