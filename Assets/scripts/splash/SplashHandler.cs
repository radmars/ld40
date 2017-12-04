using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashHandler : MonoBehaviour
{
	public string nextScene;

	void Update()
	{
		if (Input.anyKey)
		{
			SceneManager.LoadScene("emarcotte-test");
		}
	}
}
