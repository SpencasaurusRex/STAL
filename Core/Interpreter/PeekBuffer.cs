using System;
using System.Collections.Generic;

namespace STAL.Core.Interpreter
{
	public class PeekBuffer<T>
	{
		readonly IEnumerator<T> input;

		int currentIndex = 0;
		int peekedIndex = 0;
		T[] items;
		int size = 32;

		public PeekBuffer(IEnumerator<T> input)
		{
			this.input = input;
			items = new T[size];
		}

		public bool TryPeek(out T item) => TryPeek(0, out item);

		public bool TryPeek(int index, out T item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}

			// We haven't read that index from the input yet
			while (currentIndex + index >= peekedIndex)
			{
				if (!ReadFromInput())
				{
					item = default(T);
					return false;
				}
			}
			
			item = items[(currentIndex + index) % size];
			return true;
		}

		public bool TryRead(out T item)
		{
			if (currentIndex == peekedIndex)
			{
				if (!ReadFromInput())
				{
					item = default(T);
					return false;
				}
			}
			item = items[currentIndex++ % size];
			return true;
		}

		bool ReadFromInput()
		{
			if (input.MoveNext())
			{
				items[peekedIndex++ % size] = input.Current;
				return true;
			}
			input.Dispose();
			return false;
		}
	}
}

