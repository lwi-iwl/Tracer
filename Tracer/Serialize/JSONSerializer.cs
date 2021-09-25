using System.Text.Json;
using Tracer.Results;

namespace Tracer.Serialize
{
    public class JSONSerializer: Serializator
    {
        private TempResult _tempResult = new TempResult();
        private FormatTranslator _formatTranslator = new FormatTranslator();
        private JsonSerializerOptions Settings = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            WriteIndented = true
        };
        //public string Serialize(TraceResult traceResult, FormatTranslator formatTranslator)
        public string Serialize(TraceResult traceResult)
        {
            _tempResult.AnotherThreads = _formatTranslator.toEditable(traceResult);
            string json = JsonSerializer.Serialize<TempResult>(_tempResult, Settings);
            return json;
        }
    }
}
