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

            if(player.lives == 0)
            {
                player.die();
            }
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
