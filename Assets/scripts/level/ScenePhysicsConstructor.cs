using UnityEngine;

public class ScenePhysicsConstructor : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Baddies"), LayerMask.NameToLayer("Bullets"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Baddies"), LayerMask.NameToLayer("Level"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Level"), LayerMask.NameToLayer("Bullets"));
	}
}
