using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Interpreter;
using System.IO;
using System;

namespace Core.Tests.Interpreter.Tests
{
	[TestClass]
	public class PeekBufferTests
	{
		[TestMethod]
		public void PeekTest_OutOfOrder()
		{
			var streamReader = new StringStreamReader("ABCDEFG");
			PeekBuffer buffer = new PeekBuffer(streamReader);
			Assert.AreEqual('A', buffer.Peek(0));
			Assert.AreEqual('C', buffer.Peek(2));
			Assert.AreEqual('B', buffer.Peek(1));
			Assert.AreEqual('D', buffer.Peek(3));
            Assert.AreEqual('G', buffer.Peek(6));
            Assert.AreEqual('F', buffer.Peek(5));
            Assert.AreEqual('E', buffer.Peek(4));
		}

        [TestMethod]
        public void PeekTest_Sequential()
        {
            var streamReader = new StringStreamReader("123456");
            PeekBuffer buffer = new PeekBuffer(streamReader);
            Assert.AreEqual('1', buffer.Peek(0));
            Assert.AreEqual('2', buffer.Peek(1));
            Assert.AreEqual('3', buffer.Peek(2));
            Assert.AreEqual('4', buffer.Peek(3));
            Assert.AreEqual('5', buffer.Peek(4));
            Assert.AreEqual('6', buffer.Peek(5));
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void PeekTest_EndOfStreamException()
        {
            var streamReader = new StringStreamReader("");
            PeekBuffer buffer = new PeekBuffer(streamReader);
            buffer.Peek();
        }

        [TestMethod]
        public void PeekTest_EndOfStream_True()
        {
            var streamReader = new StringStreamReader("");
            PeekBuffer buffer = new PeekBuffer(streamReader);

            Assert.IsTrue(buffer.EndOfStream);
        }

        [TestMethod]
        public void PeekTest_EndOfStream_False()
        {
            var streamReader = new StringStreamReader("1");
            PeekBuffer buffer = new PeekBuffer(streamReader);

            Assert.IsFalse(buffer.EndOfStream);

            buffer.Peek();

            Assert.IsFalse(buffer.EndOfStream);
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PeekTest_NegativeIndex()
		{
			var streamReader = new StringStreamReader("1");
			PeekBuffer buffer = new PeekBuffer(streamReader);

			buffer.Peek(-1);
		}

		[TestMethod]
		public void ReadTest_Sequential()
		{
			var streamReader = new StringStreamReader("12345");
			PeekBuffer buffer = new PeekBuffer(streamReader);

			Assert.AreEqual('1', buffer.Read());
			Assert.AreEqual('2', buffer.Read());
			Assert.AreEqual('3', buffer.Read());
			Assert.AreEqual('4', buffer.Read());
			Assert.AreEqual('5', buffer.Read());
		}

		[TestMethod]
		public void ReadTest_EndofStream_True()
		{
			var streamReader = new StringStreamReader("");
			PeekBuffer buffer = new PeekBuffer(streamReader);

			Assert.IsTrue(buffer.EndOfStream);
		}

		[TestMethod]
		public void ReadTest_EndOfStream_False()
		{
			var streamReader = new StringStreamReader("1");
			PeekBuffer buffer = new PeekBuffer(streamReader);

			Assert.IsFalse(buffer.EndOfStream);
		}

		[TestMethod]
		public void PeekReadTest_PeekRead()
		{
			var streamReader = new StringStreamReader("1234");
			PeekBuffer buffer = new PeekBuffer(streamReader);

			Assert.AreEqual('1', buffer.Peek());
			Assert.AreEqual('2', buffer.Peek(1));
			Assert.AreEqual('1', buffer.Read());
			Assert.AreEqual('2', buffer.Read());

			Assert.AreEqual('3', buffer.Peek());
			Assert.AreEqual('4', buffer.Peek(1));
			Assert.AreEqual('3', buffer.Read());
			Assert.AreEqual('4', buffer.Read());
		}

		[TestMethod]
		[ExpectedException(typeof(EndOfStreamException))]
		public void ReadTest_EndOfStreamException()
		{
			var streamReader = new StringStreamReader("");
			PeekBuffer buffer = new PeekBuffer(streamReader);
			
			buffer.Read();
		}
    }

	public class StringStreamReader : StreamReader
	{
		public StringStreamReader(string str) : base(StreamFromString(str))
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
