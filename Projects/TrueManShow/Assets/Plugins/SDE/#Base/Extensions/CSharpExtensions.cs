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

        public static void Shuffle<T>(this T[] list, System.Random rng)
        {
            int n = list.Length;
            while (n-- > 1)
            {
                int k = rng.Next(n + 1);
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }

        public static bool Contains<T>(this T[] list, T value)
        {
            foreach (T item in list)
            {
                if (item.Equals(value))
                    return true;
            }
            return false;
        }

        public delegate bool DelItemComparision<T>(T item);
        public static T Find<T>(this T[] list, DelItemComparision<T> comparison)
        {
            foreach (T item in list)
            {
                if (comparison(item))
                    return item;
            }
            return default(T);
        }
    }
}
