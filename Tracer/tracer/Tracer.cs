using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{

    class Tracer : ITracer
    {
        private TempResult _tempResult = new TempResult();
        private object _locker = new object();
        public void StartTrace() 
        {
            lock (_locker)
            {
                int start = Environment.TickCount;
                AnotherThread tempThread = new AnotherThread();
                Method method = new Method();
                bool found = false;
                foreach (var anotherThread in _tempResult.AnotherThreads)
                {
                    if (anotherThread.Id == Environment.CurrentManagedThreadId)
                    {
                        found = true;
                        tempThread = anotherThread;
                        if (tempThread.MethodStack.Count() != 0)
                        {
                            Method oldMethod = tempThread.MethodStack.Peek();
                            oldMethod.Methods.Add(method);
                        }
                        else
                        {
                            tempThread.Methods.Add(method);
                        }
                    }
                }
                if (!found)
                {
                    tempThread.Id = Environment.CurrentManagedThreadId;
                    tempThread.Time = start;
                    tempThread.Methods.Add(method);
                    _tempResult.AnotherThreads.Add(tempThread);
                }

                StackTrace stackTrace = new StackTrace();
                stackTrace = new StackTrace();
                method.Name = stackTrace.GetFrame(1).GetMethod().Name;
                method.ClassName = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name;
                method.Time = start;
                Console.WriteLine(method.Name);
                Console.WriteLine(method.ClassName);
                Console.WriteLine(method.Time);
                tempThread.MethodStack.Push(method);
            }
        }

        // вызывается в конце замеряемого метода 
        public void StopTrace()
        {
            lock (_locker)
            {
                int start = Environment.TickCount;
                foreach (var anotherThread in _tempResult.AnotherThreads)
                {
                    if (anotherThread.Id == Environment.CurrentManagedThreadId)
                    {
                        Method method = anotherThread.MethodStack.Pop();
                        method.Time = method.Time - start;
                    }
                }
            }
        }

        public void printResult() 
        {
            foreach (var anotherThread in _tempResult.AnotherThreads)
            {
                Console.WriteLine(anotherThread.Id);
                Console.Write("[");
                foreach (var anotherMethod in anotherThread.Methods)
                {
                    Console.WriteLine(anotherMethod.Name);
                    Console.Write("[");
                    foreach (var anMethod in anotherMethod.Methods)
                        Console.WriteLine(anMethod.Name);
                    Console.Write("]");
                }
                Console.Write("]");
            }
        }

        public void printResultTR(TraceResult traceResult)
        {
            foreach (var anotherThread in traceResult.ReadOnlyThread)
            {
                Console.WriteLine(anotherThread.Id);
                Console.Write("[");
                foreach (var anotherMethod in anotherThread.ReadOnlyMethods)
                {
                    Console.WriteLine(anotherMethod.Name);
                    Console.Write("[");
                    foreach (var anMethod in anotherMethod.ReadOnlyMethods)
                        Console.WriteLine(anMethod.Name);
                    Console.Write("]");
                }
                Console.Write("]");
            }
        }

        public void nextMethod(Method method, Stack<List<ReadOnlyMethod>> listStack) 
        {
            if (method.Methods.Count != 0)
            {
                foreach (var anotherMethod in method.Methods)
                {
                    List <ReadOnlyMethod> readOnlyMethods = new List<ReadOnlyMethod>();
                    listStack.Push(readOnlyMethods);
                    nextMethod(anotherMethod, listStack);
                }
                ReadOnlyMethod readOnlyMethod = new ReadOnlyMethod(method.Name, method.ClassName, method.Time, listStack.Pop());
                listStack.Peek().Add(readOnlyMethod);
            }
            else
            {
                ReadOnlyMethod readOnlyMethod = new ReadOnlyMethod(method.Name, method.ClassName, method.Time, listStack.Pop());
                listStack.Peek().Add(readOnlyMethod);
            }
        }

        // получить результаты измерений  
        public TraceResult GetTraceResult() 
        {
            List<ReadOnlyThread> readOnlyThreads = new List<ReadOnlyThread>();
            foreach (var anotherThread in _tempResult.AnotherThreads)
            {
                Stack<List<ReadOnlyMethod>> listStack = new Stack<List<ReadOnlyMethod>>();
                List<ReadOnlyMethod> readOnlyThreadMethods = new List<ReadOnlyMethod>();
                listStack.Push(readOnlyThreadMethods);
                foreach (var anotherMethod in anotherThread.Methods)
                {
                    List<ReadOnlyMethod> readOnlyMethods = new List<ReadOnlyMethod>();
                    listStack.Push(readOnlyMethods);
                    nextMethod(anotherMethod, listStack);
                }
                ReadOnlyThread readOnlyThread = new ReadOnlyThread(anotherThread.Id, anotherThread.Time, listStack.Pop());
                readOnlyThreads.Add(readOnlyThread); 
            }
            TraceResult traceResult = new TraceResult(readOnlyThreads);
            return traceResult;
        }

    }
}
