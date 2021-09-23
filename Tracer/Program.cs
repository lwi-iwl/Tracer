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
            TraceResult traceResult = program.tracer.GetTraceResult();
            Serializator serializator = new Serializator();
            serializator.Serialize(traceResult);
        }

        void M2(Program program) {
            tracer.StartTrace();
            Thread.Sleep(3000);
            tracer.StopTrace();
        }

        void M3(Program program)
        {
            tracer.StartTrace();
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
