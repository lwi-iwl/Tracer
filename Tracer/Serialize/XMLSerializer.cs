using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Tracer.Results;

namespace Tracer.Serialize
{
    public class XMLSerializer:Serializator
    {
        private TempResult _tempResult = new TempResult();
        private FormatTranslator _formatTranslator = new FormatTranslator();
        //public string Serialize(TraceResult traceResult, FormatTranslator formatTranslator)
        public string Serialize(TraceResult traceResult)
        {

            _tempResult.AnotherThreads = _formatTranslator.toEditable(traceResult);
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TempResult));
            string xml = "";

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
            return xml;
        }
    }
}
