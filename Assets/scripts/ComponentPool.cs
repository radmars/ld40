using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> : MonoBehaviour where T : MonoBehaviour
{
	Dictionary<T, List<T>> pool;

	protected void Start()
	{
		pool = new Dictionary<T, List<T>>();

		foreach(var prefab in GetComponentsInChildren<T>())
		{
			pool[prefab] = new List<T>(new T[] { prefab });
			prefab.gameObject.SetActive(false);
		}
	}

	public T GetRandom()
	{
		var items = pool.Keys;
		var num = UnityEngine.Random.Range(0, items.Count);
		foreach(var key in items)
		{
			if(num == 0)
			{
				return GetInstance(key);
			}
			num--;
		}
		throw new Exception("Bugs everywhere");
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
