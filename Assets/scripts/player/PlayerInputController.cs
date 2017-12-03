using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{

	private Rigidbody2D body;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		var inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		body.AddForce(inputDirection, ForceMode2D.Impulse);
	}
}
