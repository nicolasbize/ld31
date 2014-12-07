using UnityEngine;
using System.Collections;

public class PinkButton : MonoBehaviour {

	public GameObject Explosion;
	public GameObject Screen;

	void OnTriggerEnter(Collider collider) {
		
		if (collider.gameObject.name == "mrscubechar") {
			gameObject.GetComponent<AudioSource>().Play();
			Destroy (collider.gameObject);
			Destroy (gameObject);
			Instantiate(Explosion, transform.position, Quaternion.identity);
			Screen.GetComponent<ScreenShake>().Enable();
		}
	}
}
