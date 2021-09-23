using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tracer.serialize;

namespace Tracer
{
    class JSONSerializator
    {
        private TempResult _tempResult = new TempResult();

        private JsonSerializerOptions Settings = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            WriteIndented = true
        };
        public void Serialize(TraceResult traceResult, FormatTranslator formatTranslator)
        {
            _tempResult.AnotherThreads = formatTranslator.toEditable(traceResult);
            string json = JsonSerializer.Serialize<TempResult>(_tempResult, Settings);
            Console.WriteLine("");
            Console.WriteLine(json);
        }
    }
}
