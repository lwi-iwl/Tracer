using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Methods
{
    [Serializable]

    public class Method {
        private string _name;
        private string _className;
        private int _time;
        private List<Method> _methods = new List<Method>();

        [XmlAttribute("name")]
        [JsonPropertyName("name")]
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
        [JsonPropertyName("classname")]
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
    }
}
