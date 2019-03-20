using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Interpreter
{
	public class PeekBuffer
	{
		StreamReader input;
		
		// Represents characters that have been read from input, but not from this PeekBuffer
		Queue<char> inputQueue;
		
		public PeekBuffer(StreamReader input)
		{
			this.input = input;
			inputQueue = new Queue<char>();
		}

		public char Peek(int index = 0)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("Peek index must be positive");
			}
			if (EndOfStream)
			{
				throw new EndOfStreamException();
			}

			if (index < inputQueue.Count)
			{
				// The input queue already has that char in it
				return inputQueue.ToArray()[index];
			}

			int peeked = inputQueue.Count;
			char nextChar;
			do
			{
				nextChar = NextChar();
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
			return NextChar();
		}

		public bool EndOfStream => inputQueue.Count == 0 && input.EndOfStream;

		private char NextChar()
		{
			return (char)input.Read();
		}
	}
}
