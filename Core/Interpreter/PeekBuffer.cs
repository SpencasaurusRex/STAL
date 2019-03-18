using System.Collections.Generic;
using System.IO;

namespace Core.Interpreter
{
	internal class PeekBuffer
	{
		StreamReader input;
		
		// Represents character that have been read from input, but not from this PeekBuffer
		Queue<char> inputQueue;
		
		public PeekBuffer(StreamReader input)
		{
			this.input = input;
			inputQueue = new Queue<char>();
		}

		public char Peek(int index = 0)
		{
			if (EndOfStream)
			{
				throw new EndOfStreamException();
			}

			int peeked = 0;
			char nextChar;
			do
			{
				nextChar = NextChar;
				inputQueue.Enqueue(nextChar);
			}
			while (peeked++ < index);

			return nextChar;
		}

		public char Read()
		{
			if (EndOfStream)
			{
				throw new EndOfStreamException();
			}

			if (inputQueue.Count > 0)
			{
				return inputQueue.Dequeue();
			}
			return NextChar;
		}

		public bool EndOfStream => inputQueue.Count == 0 && input.EndOfStream;
		private char NextChar => (char)input.Read();
	}
}
