using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public Vector3 direction;
	public Rigidbody2D body;

	public void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.angularVelocity = 0;
		body.rotation = 0;
		body.velocity = Vector3.zero;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		gameObject.SetActive(false);
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
