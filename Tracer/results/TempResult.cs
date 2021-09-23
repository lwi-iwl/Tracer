using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    public class TempResult
    {
        private List<AnotherThread> _anotherThreads = new List<AnotherThread>();
        [XmlArrayItem("thread", Type = typeof(AnotherThread))]
        [XmlArray(ElementName = "root")]
        public List<AnotherThread> AnotherThreads 
        {
            get 
            {
                return _anotherThreads;
            }

            set
            {
                _anotherThreads = value;
            }
        }
    }
}
