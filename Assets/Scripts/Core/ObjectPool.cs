using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new();

    public ObjectPool(T prefab, int prewarmCount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        Prewarm(prewarmCount);
    }

    private void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = CreateNew();
            Return(obj);
        }
    }

    private T CreateNew()
    {
        var obj = Object.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        return obj;
    }

    public T Get()
    {
        var obj = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();
        obj.gameObject.SetActive(true);
        
        if (obj is IPoolable poolable)
            poolable.OnTakenFromPool();
        
        return obj;
    }

    public void Return(T obj)
    {
        if (obj is IPoolable poolable)
            poolable.OnReturnedToPool();
        
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_parent);
        _pool.Enqueue(obj);
    }
}