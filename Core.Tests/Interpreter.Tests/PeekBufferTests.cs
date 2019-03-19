using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Interpreter;
using System.IO;

namespace Core.Tests.Interpreter.Tests
{
	[TestClass]
	public class PeekBufferTests
	{
		[TestMethod]
		public void PeekTest_OutOfOrder()
		{
			var streamReader = new MockStreamReader("ABCDEFG");
			PeekBuffer buffer = new PeekBuffer(streamReader);
			
			Assert.AreEqual('A', buffer.Peek(0));
			Assert.AreEqual('C', buffer.Peek(2));
			Assert.AreEqual('B', buffer.Peek(1));
			Assert.AreEqual('D', buffer.Peek(4));
            Assert.AreEqual('G', buffer.Peek(7));
            Assert.AreEqual('F', buffer.Peek(6));
            Assert.AreEqual('E', buffer.Peek(5));
        }

        [TestMethod]
        public void PeekTest_Linear()
        {
            var streamReader = new MockStreamReader("123456");
            PeekBuffer buffer = new PeekBuffer(streamReader);
            Assert.AreEqual('1', buffer.Peek());
            Assert.AreEqual('2', buffer.Peek());
            Assert.AreEqual('3', buffer.Peek());
            Assert.AreEqual('4', buffer.Peek());
            Assert.AreEqual('5', buffer.Peek());
            Assert.AreEqual('6', buffer.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void PeekTest_EndOfStreamException()
        {
            var streamReader = new MockStreamReader("");
            PeekBuffer buffer = new PeekBuffer(streamReader);
            buffer.Peek();
        }

        [TestMethod]
        public void PeekTest_EndOfStream_True()
        {
            var streamReader = new MockStreamReader("");
            PeekBuffer buffer = new PeekBuffer(streamReader);

            Assert.AreEqual(true, buffer.EndOfStream);
        }

        [TestMethod]
        public void PeekTest_EndOfStream_False()
        {
            var streamReader = new MockStreamReader("1");
            PeekBuffer buffer = new PeekBuffer(streamReader);

            Assert.AreEqual(false, buffer.EndOfStream);

            buffer.Peek();

            Assert.AreEqual(false, buffer.EndOfStream);
        }

        // TODO: Read tests
    }

	public class MockStreamReader : StreamReader
	{
		public MockStreamReader(string str) : base(StreamFromString(str))
		{
		}

		public static Stream StreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
