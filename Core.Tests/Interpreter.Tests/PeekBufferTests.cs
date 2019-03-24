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
		public void TryPeek_OutOfOrder()
        {
            var enumerator = "ABC".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);

            Assert.IsTrue(buffer.TryPeek(0, out var peeked));
            Assert.AreEqual('A', peeked);
            Assert.IsTrue(buffer.TryPeek(2, out peeked));
            Assert.AreEqual('C', peeked);
            Assert.IsTrue(buffer.TryPeek(1, out peeked));
            Assert.AreEqual('B', peeked);
        }

        [TestMethod]
        public void TryPeek_Sequential()
        {
            var enumerator = "123".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);
            
            Assert.IsTrue(buffer.TryPeek(0, out var peeked));
            Assert.AreEqual('1', peeked);
            Assert.IsTrue(buffer.TryPeek(1, out peeked));
            Assert.AreEqual('2', peeked);
            Assert.IsTrue(buffer.TryPeek(2, out peeked));
            Assert.AreEqual('3', peeked);
        }

        [TestMethod]
        public void TryPeek_NoMoreItems()
        {
            var enumerator = "".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);
            Assert.IsFalse(buffer.TryPeek(out var _));
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TryPeek_NegativeIndex()
		{
            var enumerator = "1".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);

            buffer.TryPeek(-1, out char _);
		}

		[TestMethod]
		public void TryRead_Sequential()
		{
            var enumerator = "123".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);

            Assert.IsTrue(buffer.TryRead(out var item));
            Assert.AreEqual('1', item);
            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('2', item);
            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('3', item);

        }

		[TestMethod]
		public void TryRead_NoMoreItems()
		{
            var enumerator = "".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);
            
            Assert.IsFalse(buffer.TryRead(out var _));
		}

		[TestMethod]
		public void PeekBeforeRead()
		{
            var enumerator = "1234".GetEnumerator();
            PeekBuffer<char> buffer = new PeekBuffer<char>(enumerator);

            Assert.IsTrue(buffer.TryPeek(out var item));
            Assert.AreEqual('1', item);
            Assert.IsTrue(buffer.TryPeek(1, out item));
            Assert.AreEqual('2', item);

            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('1', item);
            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('2', item);

            Assert.IsTrue(buffer.TryPeek(out item));
            Assert.AreEqual('3', item);
            Assert.IsTrue(buffer.TryPeek(1, out item));
            Assert.AreEqual('4', item);

            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('3', item);
            Assert.IsTrue(buffer.TryRead(out item));
            Assert.AreEqual('4', item);
        }
    }
}
