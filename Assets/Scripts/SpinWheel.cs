using UnityEngine;
using System.Collections;

public class SpinWheel : MonoBehaviour {

	public float spinSpeed = 30f;
	private int textLayer;
	private int playerLayer;
	private bool enabled = false;

	void Start() {
//		transform.Rotate(Vector3.up * Random.Range(0, 360));
		textLayer = LayerMask.NameToLayer("Text");
		playerLayer = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		if(enabled) {
			Debug.Log ("Hit");
			if(collider.gameObject.layer == textLayer) {
				collider.gameObject.GetComponent<CreditText>().Die();
			} else if(collider.gameObject.layer == playerLayer) {
				collider.gameObject.GetComponent<PlayerMovement>().Die();
			}
		}
	}
	
	public void Enable() { 
		enabled = true;
	}
}
