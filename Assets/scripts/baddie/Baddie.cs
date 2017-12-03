using UnityEngine;

public class Baddie : MonoBehaviour
{
    public Ball theBall;

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
		var moveScript = this.GetComponent<RailMover>();
		if (moveScript != null)
		{
			moveScript.isCompleted = true; ;
		}

		theBall.AttachBaddie(this);
	}
}
