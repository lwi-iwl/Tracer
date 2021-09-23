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
                StackTrace stackTrace = new StackTrace();
                stackTrace = new StackTrace();
                method.Name = "Unfinished " + stackTrace.GetFrame(1).GetMethod().Name;
                method.ClassName = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name;
                tempThread.MethodStack.Push(method);
                if (!found)
                {
                    tempThread.Id = Environment.CurrentManagedThreadId;
                    tempThread.Methods.Add(method);
                    _tempResult.AnotherThreads.Add(tempThread);
                }

                method.Time = Environment.TickCount;
            }
        }

        // вызывается в конце замеряемого метода 
        public void StopTrace()
        {
            AnotherThread tempThread = new AnotherThread(); 
            lock (_locker)
            {
                int start = Environment.TickCount;
                foreach (var anotherThread in _tempResult.AnotherThreads)
                {
                    if (anotherThread.Id == Environment.CurrentManagedThreadId)
                    {
                        tempThread = anotherThread;
                        Method method = anotherThread.MethodStack.Pop();
                        method.Time = start - method.Time;
                        method.Name = method.Name.Replace("Unfinished ", "");
                    }
                }
                
            }

        }


        private void nextMethod(Method method, int start, Stack<List<ReadOnlyMethod>> listStack) 
        {
            if (method.Methods.Count != 0)
            {
                foreach (var anotherMethod in method.Methods)
                {
                    List <ReadOnlyMethod> readOnlyMethods = new List<ReadOnlyMethod>();
                    listStack.Push(readOnlyMethods);
                    nextMethod(anotherMethod, start, listStack);
                }
                if (method.Name.Contains("Unfinished "))
                    method.Time = start - method.Time;
                ReadOnlyMethod readOnlyMethod = new ReadOnlyMethod(method.Name, method.ClassName, method.Time, listStack.Pop());
                listStack.Peek().Add(readOnlyMethod);
            }
            else
            {
                if (method.Name.Contains("Unfinished "))
                    method.Time = start - method.Time;
                ReadOnlyMethod readOnlyMethod = new ReadOnlyMethod(method.Name, method.ClassName, method.Time, listStack.Pop());
                listStack.Peek().Add(readOnlyMethod);
            }
        }

        // получить результаты измерений  
        public TraceResult GetTraceResult() 
        {
            int start = Environment.TickCount;
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
                    nextMethod(anotherMethod, start, listStack);
                }
                anotherThread.Time = 0;
                foreach (var anotherMethod in listStack.Peek())
                    anotherThread.Time = anotherThread.Time + anotherMethod.Time;
                ReadOnlyThread readOnlyThread = new ReadOnlyThread(anotherThread.Id, anotherThread.Time, listStack.Pop());
                readOnlyThreads.Add(readOnlyThread); 
            }
            TraceResult traceResult = new TraceResult(readOnlyThreads);
            return traceResult;
        }

    }
}
