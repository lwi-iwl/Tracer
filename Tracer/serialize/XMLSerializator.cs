using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Tracer.serialize;

namespace Tracer
{
    class XMLSerializator
    {
        private TempResult _tempResult = new TempResult();
        public void Serialize(TraceResult traceResult, FormatTranslator formatTranslator)
        {

            _tempResult.AnotherThreads = formatTranslator.toEditable(traceResult);
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TempResult));
            var xml = "";

            using (var sww = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xsSubmit.Serialize(writer, _tempResult);
                    xml = sww.ToString();
                }
            }
            Console.Write(xml);
        }
    }
}
