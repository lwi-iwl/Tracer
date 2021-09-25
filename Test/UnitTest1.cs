using ConsoleApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer.Main;
using System.Linq;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        private ITracer _tracer;
        private Foo _foo;

        [TestInitialize]
        public void Initialize()
        {
            _tracer = new MainTracer();
            _foo = new Foo(_tracer);
        }

        [TestMethod]
        public void SingleThreadTest()
        {
            _foo.WithOneNestedMethod();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(1, result.ReadOnlyThreads.Count);
        }

        [TestMethod]
        public void TwoThreadsTest()
        {
            _foo.NewThread();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(2, result.ReadOnlyThreads.Count);
        }

        [TestMethod]
        public void FourThreadsTest()
        {
            _foo.NewThreeTreads();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(4, result.ReadOnlyThreads.Count);
        }

        [TestMethod]
        public void ThreeNestedMethods()
        {
            _foo.WithThreeNestedMethod();
            var result = _tracer.GetTraceResult();

            Assert.AreEqual(1, result.ReadOnlyThreads.Count);
            Assert.AreEqual(1, result.ReadOnlyThreads.ElementAt(0).ReadOnlyMethods.Count);
            Assert.AreEqual(1, result.ReadOnlyThreads.ElementAt(0).ReadOnlyMethods.ElementAt(0).ReadOnlyMethods.Count);
            Assert.AreEqual(1, result.ReadOnlyThreads.ElementAt(0).ReadOnlyMethods.ElementAt(0).ReadOnlyMethods.ElementAt(0).ReadOnlyMethods.Count);
        }

        [TestMethod]
        public void ThreeUpperMethods()
        {
            _foo.WithThreeUpperMethod();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(3, result.ReadOnlyThreads.ElementAt(0).ReadOnlyMethods.ElementAt(0).ReadOnlyMethods.Count);
        }
    }
}
