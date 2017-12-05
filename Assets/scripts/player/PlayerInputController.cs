using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{

	private Rigidbody2D body;
	public GameObject planeGeometry;
    public Text livesText;
    public int lives;
	public GameObject tether;
	public Ball theBall;
	public SpringJoint2D joint;
	public float fireForce = 10;
	private Vector3 ballStartingPosition;
    public AudioSource deathSound;
    public AudioSource hitSound;
    private float deathTimeInterval = 3.0f;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		SetLivesText();
		ballStartingPosition = theBall.transform.localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Release();
		}

		var inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		inputDirection = Vector3.Scale (inputDirection, new Vector3 (1.2f, 1.2f, 1.2f));
		body.AddForce(inputDirection, ForceMode2D.Impulse);
		planeGeometry.transform.rotation = Quaternion.Euler(new Vector3(-90 + Mathf.Clamp(body.velocity.y  * 3, -10, 10), body.velocity.x * -3, 0));
		tether.transform.up = tether.transform.position - theBall.transform.position;
	}

	public void AttachBall()
	{
		theBall.Tether(ballStartingPosition, transform);
		joint.connectedBody = theBall.body;
		tether.SetActive(true);
		joint.enabled = true;
	}

	private void Release()
	{
		joint.connectedBody = null;
		joint.enabled = false;
		theBall.Release((theBall.transform.position - transform.position).normalized * fireForce);
		tether.SetActive(false);
		StartCoroutine(BallTravel());
	}

	private IEnumerator BallTravel()
	{
		yield return theBall.WaitUntilOffScreen();
		AttachBall();
	}

	public void SetLivesText()
	{
		livesText.text = lives + "$";
	}

    public void die()
    {
        StartCoroutine(doDeath());
    }
    
    private IEnumerator doDeath() {
        planeGeometry.SetActive(false);
        GameObject death = GameObject.Find("Death Sound");
        if (death)
        {
            AudioSource ds = death.GetComponent<AudioSource>();
            ds.Play();
            yield return new WaitForSeconds(deathTimeInterval);

            SceneManager.LoadScene("splash-menu");
        }

    }
}
