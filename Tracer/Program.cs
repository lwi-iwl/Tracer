using System.Threading;
using Tracer.writer;

namespace Tracer
{
    class Program
    {
        ITracer tracer = new Tracer();
        static void Main(string[] args)
        {
            Program program = new Program();
            Thread myThread = new Thread(new ThreadStart(program.M4));
            myThread.Start(); 
            program.tracer.StartTrace();
            program.M2(program);
            program.tracer.StopTrace();
            TraceResult traceResult = program.tracer.GetTraceResult();
            var jsonSerializer = new JSONSerializer();
            var xmlSerializer = new XMLSerializer();
            var fileWriter = new FileWriter();
            var consoleWriter = new ConsoleWriter();
            string jsonresult = jsonSerializer.Serialize(traceResult);
            fileWriter.Write(jsonresult, "C:\\Users\\nikst\\tracerfiles\\tresult.json");
            consoleWriter.Write(jsonresult);
            string xmlresult = xmlSerializer.Serialize(traceResult);
            fileWriter.Write(xmlresult, "C:\\Users\\nikst\\tracerfiles\\tresult.xml");
            consoleWriter.Write(xmlresult);
            //string result = program.tracer.XMLSerialize(traceResult);
            //string result = program.tracer.JSONSerialize(traceResult);
        }

        void M2(Program program) {
            tracer.StartTrace();
            M3(program);
            Thread.Sleep(1000);
            tracer.StopTrace();
        }

        void M3(Program program)
        {
            tracer.StartTrace();
            Thread.Sleep(3000);
            tracer.StopTrace();
        }

        void M4() 
        {
            M5();
            M6();
        }

        void M5()
        {
            tracer.StartTrace();
            Thread.Sleep(4000);
            tracer.StopTrace();
        }

        void M6()
        {
            tracer.StartTrace();
            Thread.Sleep(1000);
            tracer.StopTrace();
        }

    }
}
