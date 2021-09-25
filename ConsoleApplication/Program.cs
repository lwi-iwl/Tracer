using System.Threading;
using Tracer.Main;
using Tracer.Results;
using Tracer.Serialize;
using ConsoleApplication.Writer;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new MainTracer();
            Foo foo = new Foo(tracer);
            Thread thread1 = new Thread(foo.WithOneNestedMethod);
            Thread thread2 = new Thread(foo.WithThreeUpperMethod);
            Thread thread3 = new Thread(foo.NewThread);
            thread1.Start();
            thread2.Start();
            thread3.Start();
            foo.NewThread();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            TraceResult traceResult = tracer.GetTraceResult();
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

    }
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void NewThreeTreads()
        {
            _tracer.StartTrace();
            Thread thread5 = new Thread(_bar.InnerMethod);
            Thread thread6 = new Thread(_bar.InnerMethod);
            Thread thread7 = new Thread(_bar.InnerMethod);
            thread5.Start();
            thread6.Start();
            thread7.Start();
            thread5.Join();
            thread6.Join();
            thread7.Join();
            _tracer.StopTrace();
        }

        public void NewThread()
        {
            _tracer.StartTrace();
            Thread thread4 = new Thread(_bar.InnerMethod);
            thread4.Start();
            thread4.Join();
            _tracer.StopTrace();
        }

        public void WithOneNestedMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _bar.InnerMethod();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        public void WithTwoNestedMethod()
        {
            _tracer.StartTrace();
            WithOneNestedMethod();
            _tracer.StopTrace();
        }
        public void WithThreeNestedMethod()
        {
            _tracer.StartTrace();
            WithTwoNestedMethod();
            _tracer.StopTrace();
        }

        public void WithThreeUpperMethod()
        {
            _tracer.StartTrace();
            WithThreeNestedMethod();
            WithTwoNestedMethod();
            WithOneNestedMethod();
            _tracer.StopTrace();
        }

    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}
