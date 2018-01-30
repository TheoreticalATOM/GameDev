using System.Collections.Generic;

namespace SDE
{
    public static class CSharpExtensions
    {
		/// <summary>
		/// Will dequeue an item and enqueue it again, moving the front most item to the back
		/// </summary>
		/// <returns>Returns the dequeued item</returns>
        public static T DequeueAndEnqueueToBack<T>(this Queue<T> queue)
        {
            T value = queue.Dequeue();
            queue.Enqueue(value);
            return value;
        }
    }
}
