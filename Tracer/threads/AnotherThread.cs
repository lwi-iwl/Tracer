using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Tracer.Methods;

namespace Tracer.Threads
{
    [Serializable]

    public class AnotherThread
    {
        private int _id;
        private int _time;
        private List <Method> _methods = new List<Method>();
        [XmlIgnore]
        private Stack<Method> _methodStack = new Stack<Method>();

        [XmlAttribute("id")]
        [JsonPropertyName("id")]
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        [XmlAttribute("time")]
        [JsonPropertyName("time")]
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
        [JsonPropertyName("method")]
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
        [XmlIgnore]
        [JsonIgnore]
        public Stack<Method> MethodStack
        {
            get
            {
                return _methodStack;
            }

            set 
            {
            }
        }

    }
}
