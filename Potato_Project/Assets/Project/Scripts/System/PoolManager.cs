using System;
using System.Collections.Generic;
using UnityEngine;
public interface IPool
{
    void Clear();
}

public class ObjectPool<T> : IPool where T : Component
{
   private readonly Stack<T> pool = new Stack<T>();
   private readonly Func<T> factory;
   private readonly Action<T> onGet;
   private readonly Action<T> onReturn;

   public int countInactive => pool.Count;
   public ObjectPool(Func<T> factory, Action<T> onGet = null, Action<T> onReturn = null, int initialize = 0)
    {
        this.factory = factory ?? throw new ArgumentException(nameof(factory));
        this.onGet = onGet;
        this.onReturn = onReturn;

        for(int i = 0; i < initialize; i++)
        {
            T obj = this.factory();
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Get()
    {
        T obj = pool.Count > 0 ? pool.Pop() : factory();
        obj.gameObject.SetActive(true);
        onGet?.Invoke(obj);
        return obj;
    }

    public void Return(T obj)
    {
        if(null == obj) return;
        onReturn?.Invoke(obj);
        obj.gameObject.SetActive(false);
        pool.Push(obj);
        
    }

    public void Clear()
    {
        while(pool.Count > 0)
        {
            var obj = pool.Pop();
            if(obj != null)
                UnityEngine.Object.Destroy(obj.gameObject);
        }
    }

}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance {get; private set;}
    private readonly Dictionary<string, object> pools = new Dictionary<string, object>();

    private void Awake()
    {

        if(null != Instance && this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public ObjectPool<T> CreatePool<T>(string key, Func<T> factory, Action<T> onGet = null, Action<T> onReturn = null, int initialize = 0) where T : Component
    {
        if(pools.ContainsKey(key))
        {
            Debug.LogWarning($"[PoolManager] Pool '{key}' already exists.");
            return GetPool<T>(key);
        }
        var pool = new ObjectPool<T>(factory,onGet,onReturn,initialize);
        pools[key] = pool;
        return pool;

    }

    public ObjectPool<T> GetPool<T>(string key) where T : Component
    {
        if(pools.TryGetValue(key, out object pool))
        {
            return pool as ObjectPool<T>;
        }
        Debug.LogError($"[PoolManager] Pool '{key}' not found.");
        return null;
    }

    public T Get<T>(string key) where T : Component
    {
        return GetPool<T>(key)?.Get();
    }
    
    public void Return<T>(string key, T obj) where T : Component
    {
       GetPool<T>(key)?.Return(obj);
    }

   public void ClearPool(string key)
    {
        if(pools.TryGetValue(key, out object pool))
        {
            (pool as IPool)?.Clear();
            pools.Remove(key);
        }
    }


}