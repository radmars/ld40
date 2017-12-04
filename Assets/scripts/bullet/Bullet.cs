using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody2D body;

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

        var player = collision.transform.GetComponent<PlayerInputController>();

        //TODO Show game over when lives reach 0
        if (player != null)
        {
            player.lives--;
            player.SetLivesText();

			GameObject[] bullets = GameObject.FindGameObjectsWithTag ("bullet");
			foreach (GameObject bullet in bullets) {
				bullet.SetActive (false);
			}

            if(player.lives == 0)
            {
                player.die();
            }
            else
            {
                player.hitSound.Play();
            }
        }

        var ball = collision.transform.GetComponent<Ball>();
        if (ball != null)
        {
            ball.pickupSound.Play();
        }
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
