using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
   private readonly Stack<T> pool = new Stack<T>();
   private readonly Func<T> factory;
   private readonly Action<T> onGet;
   private readonly Action<T> onReturn;

   public int countInactive => pool.Count;
   public ObjectPool(Func<T> _factory, Action<T> _onGet = null, Action<T> _onReturn = null, int _initialize = 0)
    {
        factory = _factory ?? throw new ArgumentException(nameof(factory));
        onGet = _onGet;
        onReturn = _onReturn;

        for(int i = 0; i < _initialize; i++)
        {
            T obj = factory();
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
            T obj = pool.Pop();
            if(null != obj)
            {
                UnityEngine.Object.Destroy(obj.gameObject);
            }
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
    public ObjectPool<T> CreatePool<T>(string _key, Func<T> _factory, Action<T> _onGet = null, Action<T> _onReturn = null, int _initialize = 0) where T : Component
    {
        if(pools.ContainsKey(_key))
        {
            Debug.LogWarning($"[PoolManager] Pool '{_key}' already exists.");
            return GetPool<T>(_key);
            
        }

        var pool = new ObjectPool<T>(_factory,_onGet,_onReturn,_initialize);
        pools[_key] = pool;
        return pool;

    }

    public ObjectPool<T> GetPool<T>(string _key) where T : Component
    {
        if(pools.TryGetValue(_key, out object pool))
        {
            return pool as ObjectPool<T>;
        }
        Debug.LogError($"[PoolManager] Pool '{_key}' not found.");
        return null;
    }

    public T Get<T>(string _key) where T : Component
    {
        return GetPool<T>(_key)?.Get();
    }
    
    public void Return<T>(string _key, T _obj) where T : Component
    {
       GetPool<T>(_key)?.Return(_obj);
    }

    public void ClearPool(string _key)
    {
        if(pools.TryGetValue(_key, out object pool))
        {
            (pool as ObjectPool<Component>)?.Clear();
            pools.Remove(_key);
        }
    }


}