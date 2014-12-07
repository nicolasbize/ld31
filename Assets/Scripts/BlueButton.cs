using UnityEngine;
using System.Collections;

public class BlueButton : MonoBehaviour {

	public GameObject Explosion;
	public GameObject Screen;
	
	void Update() {
		if (transform.position.y < 35) {
			Vector3 p = transform.position;
			//transform.position = new Vector3(p.x, 35, p.z);
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		
		if (collider.gameObject.name == "mrboxchar") {
			Destroy (collider.gameObject);
			Destroy (gameObject);
			Instantiate(Explosion, transform.position, Quaternion.identity);
			for(int i=0; i<20; i++) {
				Instantiate(Explosion, new Vector3(Random.Range (-15, 15), Random.Range (5, 35), 0), Quaternion.identity);
			}
			Screen.GetComponent<ScreenShake>().SwitchMesh();
		}
	}
}
