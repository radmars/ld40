using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public Vector3 direction;
	public Rigidbody body;

	public void Start()
	{
		body = GetComponent<Rigidbody>();
		body.angularVelocity = Vector3.zero;
		body.rotation = Quaternion.identity;
		body.velocity = Vector3.zero;
	}

	private void OnCollisionEnter(Collision collision)
	{
		gameObject.SetActive(false);
		body.rotation = Quaternion.identity;
		body.velocity = Vector3.zero;
	}

	public void Update()
	{
		transform.position = transform.position + direction * Time.deltaTime * speed;
	}

	private void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}
}
