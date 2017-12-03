using UnityEngine;

public class ScenePhysicsConstructor : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Baddies"), LayerMask.NameToLayer("Bullets"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Baddies"), LayerMask.NameToLayer("Bosses"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Bullets"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Bosses"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Baddies"), LayerMask.NameToLayer("Level"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Level"), LayerMask.NameToLayer("Bullets"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Level"), LayerMask.NameToLayer("Bosses"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Level"), LayerMask.NameToLayer("Default"));
	}
}
