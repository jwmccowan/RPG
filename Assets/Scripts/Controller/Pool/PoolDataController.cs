using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDataController : MonoBehaviour
{
    class PoolData
    {
        public Queue<Poolable> pool;
        public GameObject prefab;
        public int maxCount = 1;
    }

    static Dictionary<string, PoolData> poolDataMap = new Dictionary<string, PoolData>();
    static PoolDataController instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("Pool Data Controller");
                DontDestroyOnLoad(obj);
                _instance = obj.AddComponent<PoolDataController>();
            }
            return _instance;
        }
    }
    static PoolDataController _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public static bool Enqueue(Poolable toPool)
    {
        if (toPool == null || toPool.isPooled == true || !poolDataMap.ContainsKey(toPool.key))
        {
            return false;
        }

        PoolData poolData = poolDataMap[toPool.key];
        if (poolData.pool.Count >= poolData.maxCount)
        {
            Destroy(toPool.gameObject);
            return false;
        }
        toPool.isPooled = true;
        toPool.transform.SetParent(instance.transform);
        toPool.gameObject.SetActive(false);
        poolData.pool.Enqueue(toPool);
        return true;
    }

    public static Poolable Dequeue(string key)
    {
        if (!poolDataMap.ContainsKey(key))
        {
            return null;
        }

        PoolData poolData = poolDataMap[key];
        if (poolData.pool.Count == 0)
        {
            return CreateInstance(key, poolData.prefab);
        }
        Poolable p = poolData.pool.Dequeue();
        p.isPooled = false;
        return p;
    }

    public static bool SetMaxCount(string key, int maxCount)
    {
        if (!poolDataMap.ContainsKey(key))
        {
            return false;
        }
        poolDataMap[key].maxCount = maxCount;
        return true;
    }

    public static bool AddKey(string key, GameObject prefab, int maxCount, int prepopulate)
    {
        if (poolDataMap.ContainsKey(key))
        {
            return false;
        }

        PoolData poolData = new PoolData
        {
            maxCount = maxCount,
            prefab = prefab,
            pool = new Queue<Poolable>(prepopulate)
        };

        poolDataMap.Add(key, poolData);

        for (int i = 0; i < prepopulate; i++)
        {
            Enqueue(CreateInstance(key, prefab));
        }

        return true;
    }

    public static void RemoveKey(string key)
    {
        if (poolDataMap.ContainsKey(key))
        {
            PoolData poolData = poolDataMap[key];
            while (poolData.pool.Count > 0)
            {
                Destroy(poolData.pool.Dequeue().gameObject);
            }
            poolDataMap.Remove(key);
        }
    }

    static Poolable CreateInstance(string key, GameObject prefab)
    {
        if (!poolDataMap.ContainsKey(key))
        {
            return null;
        }
        GameObject obj = Instantiate(prefab) as GameObject;
        Poolable p = obj.AddComponent<Poolable>();
        p.key = key;
        return p;
    }
}
