using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tracer
{
    class Program
    {
        Tracer tracer = new Tracer();
        static void Main(string[] args)
        {
            Program program = new Program();
            Thread myThread = new Thread(new ThreadStart(program.M4));
            myThread.Start(); 
            program.tracer.StartTrace();
            program.M2(program);
            program.M3(program);
            program.tracer.StopTrace();
            program.tracer.printResult();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(""); 
            TraceResult traceResult = program.tracer.GetTraceResult();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            program.tracer.printResultTR(traceResult);
            Serializator serializator = new Serializator();
            serializator.Serialize(traceResult);
        }

        void M2(Program program) {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        void M3(Program program)
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        void M4() 
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        void M5()
        {
            Console.WriteLine(Environment.CurrentManagedThreadId);
        }

        void M6()
        {
            Console.WriteLine(Environment.CurrentManagedThreadId);
        }

    }
}
