using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.serialize;

namespace Tracer
{

    class Tracer : ITracer
    {
        private TempResult _tempResult = new TempResult();
        private object _locker = new object();
        private FormatTranslator _formatTranslator = new FormatTranslator();
        private JSONSerializator _jsonSerializator = new JSONSerializator();
        private XMLSerializator _xmlSerializator = new XMLSerializator();
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
        private void SetUnfinishedMethodsTime(Method method, int start)
        {
            if (method.Methods.Count != 0)
            {
                foreach (var anotherMethod in method.Methods)
                {
                    SetUnfinishedMethodsTime(anotherMethod, start);
                }
                if (method.Name.Contains("Unfinished "))
                    method.Time = start - method.Time;
            }
            else
            {
                if (method.Name.Contains("Unfinished "))
                    method.Time = start - method.Time;
            }
        }
        // получить результаты измерений  
        public TraceResult GetTraceResult() 
        {
            lock (_locker)
            {
                int start = Environment.TickCount;
                foreach (var anotherThread in _tempResult.AnotherThreads)
                {
                    foreach (var anotherMethod in anotherThread.Methods)
                    {
                        SetUnfinishedMethodsTime(anotherMethod, start);
                    }
                }

                TraceResult traceResult = new TraceResult(_formatTranslator.toReadOnly(_tempResult));
                return traceResult;
            }
        }

        public void XMLSerialize(TraceResult traceResult)
        {
            _xmlSerializator.Serialize(traceResult, _formatTranslator);
        }

        public void JSONSerialize(TraceResult traceResult)
        {
            _jsonSerializator.Serialize(traceResult, _formatTranslator);
        }

    }
}
