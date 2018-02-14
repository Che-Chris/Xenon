using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {


	public string scene;
//    [SerializeField]
//    private Text loadingText;

	public bool loadOnStart;

	void Start() {
        if (loadOnStart) {
			this.BeginLoading ();
		}
	}
	// Prep the loader to begin loading asychronously
	public void BeginLoading() {

		// ...and start a coroutine that will load the desired scene.
		StartCoroutine(AsyncLoadNewScene());
	}

	// Load the next scene immediately
	public void LoadImmediate() {
		SceneManager.LoadScene (this.scene);
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	private IEnumerator AsyncLoadNewScene() {
		Debug.Log ("Loading...");
		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(4);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(this.scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}

}
