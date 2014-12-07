using UnityEngine;
using System.Collections;

public class SpinWheel : MonoBehaviour {

	public float spinSpeed = 30f;
	private int textLayer;

	void Start() {
		transform.Rotate(Vector3.up * Random.Range(0, 360));
		textLayer = LayerMask.NameToLayer("Text");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == textLayer) {
			collider.gameObject.GetComponent<CreditText>().Die();
		}
	}
}
