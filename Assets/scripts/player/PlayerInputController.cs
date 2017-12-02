using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour {

	private Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		var inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		body.AddForce(inputDirection, ForceMode.VelocityChange);
	}
}
