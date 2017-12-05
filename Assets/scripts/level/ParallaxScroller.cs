using System;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
	public float scrollSpeed;
	private Vector2 savedOffset;
	private new Renderer renderer;
	public Material []materials;

	void Start()
	{
		renderer = GetComponent<Renderer>();
		savedOffset = renderer.material.GetTextureOffset("_MainTex");
	}

	void Update()
	{
		float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2(savedOffset.x, y);
		renderer.material.SetTextureOffset("_MainTex", offset);
	}

	void OnDisable()
	{
		renderer.material.SetTextureOffset("_MainTex", savedOffset);
	}

	internal void StartNewLevel()
	{
		int index = UnityEngine.Random.Range(0, materials.Length);
		Debug.Log("Index: " + index);
		renderer.material = materials[index];
	}
}