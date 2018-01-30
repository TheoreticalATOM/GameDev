using System.Collections.Generic;
using UnityEngine;

namespace SDE.Data
{
    public interface IRuntime { }

    [CreateAssetMenu(fileName = "RuntimeSet", menuName = "SDE/Data/RuntimeSet")]
    public class RuntimeSet : ScriptableObject
    {
        LinkedList<IRuntime> mList = new LinkedList<IRuntime>();
        
        // Setters
        public void Add(IRuntime item)
        {
            mList.AddLast(item);
        }
        public void Remove(IRuntime item)
        {
            mList.Remove(item);
        }

        // Getters
        public T GetFirst<T>() where T : IRuntime
        {
            return (T)mList.First.Value;
        }
        public T GetLast<T>() where T : IRuntime
        {
            return (T)mList.Last.Value;
        }

        public bool IsEmpty { get { return mList.Count < 1; } }
        public LinkedList<IRuntime> List { get { return mList; } }
    }
}