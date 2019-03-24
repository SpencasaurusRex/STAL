using System;
using System.Collections.Generic;

namespace Core.Interpreter
{
	public class PeekBuffer<T>
	{
        readonly IEnumerator<T> input;
		
		// Represents items that have been read from input, but not from this PeekBuffer
        readonly Queue<T> inputQueue;

        public PeekBuffer(IEnumerator<T> input)
		{
			this.input = input;
			inputQueue = new Queue<T>();
		}

        public bool TryPeek(out T item)
        {
            return TryPeek(0, out item);
        }

        public bool TryPeek(int index, out T item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}

			if (index < inputQueue.Count)
			{
				// The input queue already has that char in it
				item = inputQueue.ToArray()[index];
                return true;
            }

			int peeked = inputQueue.Count;
			do
			{
                if (TryNextItem(out item))
                {
                    inputQueue.Enqueue(item);
                }
                else
                {
                    return false;
                }
			}
			while (peeked++ < index);

            return true;
        }

		public bool TryRead(out T item)
		{
            if (inputQueue.Count > 0 || TryPeek(out T _))
            {
                item = inputQueue.Dequeue();
                return true;
            }

            item = default(T);
            return false;
        }

        bool TryNextItem(out T next)
		{
            if (input.MoveNext())
            {
                next = input.Current;
                return true;
            }
            input.Dispose();
            next = default(T);
            return false;
        }
	}
}
