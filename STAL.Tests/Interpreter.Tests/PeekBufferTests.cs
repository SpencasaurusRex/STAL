using Microsoft.VisualStudio.TestTools.UnitTesting;
using STAL.Interpreter;
using System.IO;
using System.Text;

namespace STAL.Tests.Interpreter.Tests
{
	[TestClass]
	public class PeekBufferTests
	{
		[TestMethod]
		public void PeekTest_Alternating()
		{
			var streamReader = new MockStreamReader("ABCDEFG");
			PeekBuffer buffer = new PeekBuffer(streamReader);
			
			Assert.AreEqual('A', buffer.Peek(0));
			Assert.AreEqual('C', buffer.Peek(2));
			Assert.AreEqual('B', buffer.Peek(1));
			Assert.AreEqual('D', buffer.Peek(4));
		}
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
