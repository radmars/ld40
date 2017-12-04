using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(RailMover))]
public class Baddie : MonoBehaviour
{
    public Ball theBall;
    public bool isActive;
	private RailMover mover;
	public RailMover Mover
	{
		get {
			if (!mover) {
				mover = GetComponent<RailMover>();
			}
			return mover;
		}
		private set
		{

		}
	}

    public void Freshen()
    {
        this.GetComponent<Rigidbody2D>().simulated = true;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), theBall.GetComponent<Collider2D>(), false);
        isActive = true;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject == theBall.gameObject)
		{
			Physics2D.IgnoreCollision(collision.otherCollider, collision.collider);
			Attach();
		}
	}

    public virtual void Attach()
	{
		var moveScript = Mover;
		if (moveScript != null)
		{
			moveScript.isStopped = true; ;
		}
        isActive = false;
        GetComponent<Rigidbody2D>().simulated = false;
        theBall.AttachBaddie(this);
	}
}
