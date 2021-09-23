using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Tracer
{
    class Serializator
    {
        private TempResult _tempResult = new TempResult();

        private void nextMethod(ReadOnlyMethod readOnlyMethod, Stack<List<Method>> listStack)
        {
            if (readOnlyMethod.ReadOnlyMethods.Count != 0)
            {
                foreach (var anotherMethod in readOnlyMethod.ReadOnlyMethods)
                {
                    List<Method> methods = new List<Method>();
                    listStack.Push(methods);
                    nextMethod(anotherMethod, listStack);
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

 
        public void Serialize(TraceResult traceResult)
        {
            List<AnotherThread> anotherThreads = new List<AnotherThread>();
            foreach (var readOnlyThread in traceResult.ReadOnlyThread)
            {
                Stack<List<Method>> listStack = new Stack<List<Method>>();
                List<Method> threadMethods = new List<Method>();
                listStack.Push(threadMethods);
                foreach (var anotherMethod in readOnlyThread.ReadOnlyMethods)
                {
                    List<Method> methods = new List<Method>();
                    listStack.Push(methods);
                    nextMethod(anotherMethod, listStack);
                }
                AnotherThread anotherThread = new AnotherThread();
                anotherThread.Id = readOnlyThread.Id;
                anotherThread.Time = readOnlyThread.Time;
                anotherThread.Methods = listStack.Pop();
                anotherThreads.Add(anotherThread);
            }

            TempResult tempResult = new TempResult();
            tempResult.AnotherThreads = anotherThreads;


            XmlSerializer xsSubmit = new XmlSerializer(typeof(TempResult));
            var xml = "";

            using (var sww = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                settings.NewLineOnAttributes = true;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xsSubmit.Serialize(writer, tempResult);
                    xml = sww.ToString();
                }
            }
            Console.Write(xml);
        }

    }
}
