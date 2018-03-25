using System.Collections.Generic;

namespace SDE
{
    public static class CSharpExtensions
    {
        // _________________________________________________
        // Actions
        public static void TryInvoke(this System.Action action)
        {
            if (action != null)
                action();
        }
        public static void TryInvoke<T>(this System.Action<T> action, T value)
        {
            if (action != null)
                action(value);
        }

        // _________________________________________________
        // Queues
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
        public static void DequeueAll<T>(this Queue<T> queue, System.Action<T> AfterPop)
        {
            while (queue.Count > 0)
                AfterPop(queue.Dequeue());
        }

        public static void Enqueue<T>(this Queue<T> queue, T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                queue.Enqueue(array[i]);
        }

        // _________________________________________________
        // Stacks
        public static void PopAll<T>(this Stack<T> stack, System.Action<T> AfterPop)
        {
            while (stack.Count > 0)
                AfterPop(stack.Pop());
        }

        // _________________________________________________
        // Arrays
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

        public delegate bool DelItemComparision<T>(T item);
        public static bool Contains<T>(this T[] list, DelItemComparision<T> comparision)
        {
            return Find<T>(list, comparision) != null;

        }
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
