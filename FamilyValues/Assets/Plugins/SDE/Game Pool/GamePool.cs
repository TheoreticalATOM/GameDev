using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SDE.GamePool
{
    using SDE.Data;

    public class GamePool : MonoBehaviour, IRuntime
    {
        public class PooledObjectInstance
        {
            public GameObject Instance { get; private set; }
            System.Action mEvaluateMethod;

            public PooledObjectInstance(GameObject objInstance)
            {
                Instance = objInstance;

                mEvaluateMethod = () => { };

                IPoolable[] poolables = objInstance.GetComponents<IPoolable>();
                // if the object has a ipoolable, set the evaluation method to call the OnSpawned method,
                // otherwise, do nothing
                if (poolables.Length > 0)
                {
                    mEvaluateMethod = null;
                    foreach (IPoolable poolable in poolables)
                    {
                        poolable.OnCreated();
                        mEvaluateMethod += poolable.OnSpawned;
                    }
                }
                Instance.SetActive(false);
            }

            // @ Spawn
            public void Spawn(Vector3 position, Quaternion rotation)
            {
                Instance.transform.rotation = rotation;
                Spawn(position);
            }
            public void Spawn(Vector3 position)
            {
                Instance.transform.position = position;
                Instance.SetActive(true);

                mEvaluateMethod();
            }
        }

        public PoolData PoolDataConfiguration;
        public RuntimeSet GamePoolSet;

        public delegate bool ResetCriteria(GameObject pooledObject);

        private Dictionary<int, Queue<PooledObjectInstance>> mPool;

        /// Singleton
        public static GamePool Instance { get; private set; }

        // ------------------------------------------------
        // @ Mono B
        private void Awake()
        {
            GamePoolSet.Add(this);

            mPool = new Dictionary<int, Queue<PooledObjectInstance>>();
            PoolDataConfiguration.FillDictionary(ref mPool, CreatePool);
        }

        // ------------------------------------------------
        // @ Pool Managment
        #region Pool Managment
        public void CreatePool(GameObject obj, int amount)
        {
            Queue<PooledObjectInstance> pool = new Queue<PooledObjectInstance>();
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(obj);
                go.transform.parent = transform;
                pool.Enqueue(new PooledObjectInstance(go));
            }
            mPool.Add(obj.GetInstanceID(), pool);
        }
        public void CreatePool(PoolData.Candidate poolData)
        {
            CreatePool(poolData.Object, poolData.Amount);
        }

        /// <summary>
        /// Will destroy all the objects within the pool entry, and remove any 
        /// reference to the pool
        /// </summary>
        public void RemovePool(int prefabID)
        {
            /// if it doesn't exists return
            if (!HasPrefabID(prefabID))
                return;

            /// Destroy all of the current pool's objects
            Queue<PooledObjectInstance> pool = mPool[prefabID];
            while (pool.Count > 0)
                Destroy(pool.Dequeue().Instance);

            mPool.Remove(prefabID);
        }
        #endregion

        // ------------------------------------------------
        // @ Spawning
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefab, inst => inst.Spawn(position, rotation));
        }
        public GameObject Spawn(GameObject prefab, Vector3 position)
        {
            return Spawn(prefab, inst => inst.Spawn(position));
        }

        private GameObject Spawn(GameObject prefab, System.Action<PooledObjectInstance> spawnAction)
        {
            PooledObjectInstance obj = FetchObject(prefab.GetInstanceID());
            ValidateInstance(obj, prefab.name);
            spawnAction(obj);
            return obj.Instance;
        }

        private static void ValidateInstance<T>(PooledObjectInstance obj, T msg)
        {
            Assert.IsNotNull(obj, "GamePool does not contain: " + msg);
        }

        public void ResetPool(string layerMask)
        {
            ResetPool((obj) => obj.layer == LayerMask.NameToLayer(layerMask));
        }

        public void ResetPool(ResetCriteria resetCriteria)
        {
            foreach (var pool in mPool)
            {
                Queue<PooledObjectInstance> poolQueue = pool.Value;
                for (int i = 0; i < poolQueue.Count; i++)
                {
                    PooledObjectInstance inst = poolQueue.DequeueAndEnqueueToBack();
                    if (resetCriteria(inst.Instance))
                        inst.Instance.SetActive(false);
                }
            }
        }

        // ----------------------------------------
        // @ Getters
        public bool HasPrefabID(int id)
        {
            return !mPool.ContainsKey(id);
        }

        public PooledObjectInstance FetchObject(int prefabID)
        {
            if (mPool.ContainsKey(prefabID))
                return mPool[prefabID].DequeueAndEnqueueToBack();
            return null;
        }

    }
}