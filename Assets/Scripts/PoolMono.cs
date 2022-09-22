using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }

    public bool AutoExpand { get; set; }

    public Transform Container { get; }

    private List<T> _pool;

    public PoolMono(T prefab, int size)
    {
        this.Prefab = prefab;
        this.Container = null;

        CreatePool(size);
    }

    public PoolMono(T prefab, int size, Transform container)
    {
        this.Prefab = prefab;
        this.Container = container;

        CreatePool(size);
    }

    private void CreatePool(int size)
    {
        this._pool = new List<T>();

        for (int i = 0; i < size; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        T createdObject = Object.Instantiate(this.Prefab, this.Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (T mono in _pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out T element))
        {
            return element;
        }

        if (this.AutoExpand)
        {
            return this.CreateObject(true);
        }

        throw new System.Exception("There is no free elements");
    }
}
