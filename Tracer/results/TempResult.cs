using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Tracer.Threads;

namespace Tracer.Results
{
    [Serializable]
    public class TempResult
    {
        private List<AnotherThread> _anotherThreads = new List<AnotherThread>();
        [XmlArrayItem("thread", Type = typeof(AnotherThread))]
        [XmlArray(ElementName = "root")]
        [JsonPropertyName("threads")]
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
