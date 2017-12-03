using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{

	private Rigidbody2D body;
	public GameObject planeGeometry;
    public Text livesText;
    public int lives;
	public GameObject tether;
	public Ball theBall;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
        SetLivesText();
	}

	// Update is called once per frame
	void Update()
	{
		var inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		body.AddForce(inputDirection, ForceMode2D.Impulse);
		planeGeometry.transform.rotation = Quaternion.Euler(new Vector3(-90, body.velocity.x * -3, 0));
		tether.transform.up = tether.transform.position - theBall.transform.position;

        //TODO: get hit lose lives
	}

    public void SetLivesText()
    {
        livesText.text = "X " + lives;
    }
}
