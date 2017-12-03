using UnityEngine;

public class Baddie : MonoBehaviour
{
    public Ball theBall;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CoLLISION:");
        Debug.Log(theBall);
        Debug.Log("OBJ:");
        Debug.Log(collision.collider.gameObject);
        if (collision.collider.gameObject == theBall.gameObject)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider);
            Attach();
        }
    }
    public void Attach()
    {
        var moveScript = this.GetComponent<RailMover>();
        if (moveScript != null)
        {
            moveScript.isCompleted = true;;
        }

        theBall.AttachBaddie(this);        
    }
}
