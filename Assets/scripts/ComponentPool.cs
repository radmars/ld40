using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> : MonoBehaviour where T : MonoBehaviour
{
	Dictionary<T, List<T>> pool;

	protected void Start()
	{
		pool = new Dictionary<T, List<T>>();

		foreach(var bulletPrefab in GetComponentsInChildren<T>())
		{
			pool[bulletPrefab] = new List<T>();
			bulletPrefab.gameObject.SetActive(false);
		}
	}

	public T GetInstance(T desired)
	{
		List<T> prefabPool;

		if (pool.TryGetValue(desired, out prefabPool))
		{
			foreach (T b in prefabPool)
			{
				if (!b.gameObject.activeSelf)
				{
					b.gameObject.SetActive(true);
					return b;
				}
			}
			var newInstance = Instantiate(desired);
			prefabPool.Add(newInstance);
			newInstance.transform.parent = gameObject.transform;
			return newInstance;
		}
		throw new Exception("No such prefab!");
	}
}
