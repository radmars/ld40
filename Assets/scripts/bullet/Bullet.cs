using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public Vector3 direction;

	private void OnCollisionEnter(Collision collision)
	{
		gameObject.SetActive(false);
	}
	public void Update()
	{
		transform.position = transform.position + direction * Time.deltaTime * speed;
	}
}
