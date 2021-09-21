using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Tracer
{
    class Serializator
    {
        public void Serialize(TraceResult traceResult) 
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TraceResult));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, traceResult);
                    xml = sww.ToString(); // Your XML
                    Console.WriteLine(xml);
                }
            }
        }

    }
}
