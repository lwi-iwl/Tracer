using System;
using System.Collections.Generic;
using Tracer.Methods;
using Tracer.Threads;

namespace Tracer.Results
{
    public class FormatTranslator
    {

        private void NextMethod(Method method, int start, Stack<List<ReadOnlyMethod>> listStack)
        {
            if (method.Methods.Count != 0)
            {
                foreach (var anotherMethod in method.Methods)
                {
                    List<ReadOnlyMethod> readOnlyMethods = new List<ReadOnlyMethod>();
                    listStack.Push(readOnlyMethods);
                    NextMethod(anotherMethod, start, listStack);
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
        public List<ReadOnlyThread> ToReadOnly(TempResult tempResult)
        {
            int start = Environment.TickCount;
            List<ReadOnlyThread> readOnlyThreads = new List<ReadOnlyThread>();
            foreach (var anotherThread in tempResult.AnotherThreads)
            {
                Stack<List<ReadOnlyMethod>> listStack = new Stack<List<ReadOnlyMethod>>();
                List<ReadOnlyMethod> readOnlyThreadMethods = new List<ReadOnlyMethod>();
                listStack.Push(readOnlyThreadMethods);
                foreach (var anotherMethod in anotherThread.Methods)
                {
                    List<ReadOnlyMethod> readOnlyMethods = new List<ReadOnlyMethod>();
                    listStack.Push(readOnlyMethods);
                    NextMethod(anotherMethod, start, listStack);
                }
                anotherThread.Time = 0;
                foreach (var anotherMethod in listStack.Peek())
                    anotherThread.Time = anotherThread.Time + anotherMethod.Time;
                ReadOnlyThread readOnlyThread = new ReadOnlyThread(anotherThread.Id, anotherThread.Time, listStack.Pop());
                readOnlyThreads.Add(readOnlyThread);
            }
            return readOnlyThreads;

        }

        private void NextReadOnlyMethod(ReadOnlyMethod readOnlyMethod, Stack<List<Method>> listStack)
        {
            if (readOnlyMethod.ReadOnlyMethods.Count != 0)
            {
                foreach (var anotherMethod in readOnlyMethod.ReadOnlyMethods)
                {
                    List<Method> methods = new List<Method>();
                    listStack.Push(methods);
                    NextReadOnlyMethod(anotherMethod, listStack);
                }
                Method method = new Method();
                method.Name = readOnlyMethod.Name;
                method.ClassName = readOnlyMethod.ClassName;
                method.Time = readOnlyMethod.Time;
                method.Methods = listStack.Pop();
                listStack.Peek().Add(method);
            }
            else
            {
                Method method = new Method();
                method.Name = readOnlyMethod.Name;
                method.ClassName = readOnlyMethod.ClassName;
                method.Time = readOnlyMethod.Time;
                method.Methods = listStack.Pop();
                listStack.Peek().Add(method);
            }
        }

        public List<AnotherThread> toEditable(TraceResult traceResult)
        {
            List<AnotherThread> anotherThreads = new List<AnotherThread>();
            foreach (var readOnlyThread in traceResult.ReadOnlyThreads)
            {
                Stack<List<Method>> listStack = new Stack<List<Method>>();
                List<Method> threadMethods = new List<Method>();
                listStack.Push(threadMethods);
                foreach (var anotherMethod in readOnlyThread.ReadOnlyMethods)
                {
                    List<Method> methods = new List<Method>();
                    listStack.Push(methods);
                    NextReadOnlyMethod(anotherMethod, listStack);
                }
                AnotherThread anotherThread = new AnotherThread();
                anotherThread.Id = readOnlyThread.Id;
                anotherThread.Time = readOnlyThread.Time;
                anotherThread.Methods = listStack.Pop();
                anotherThreads.Add(anotherThread);
            }
            return anotherThreads;
        }
    }
}
