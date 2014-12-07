using UnityEngine;
using System.Collections;

public class MrsCubeMovement : MonoBehaviour {


	private float speed = 4f;
	private float dir = 1f;
	private bool keepWalking = true;
	private bool willHit = false;
	
	// Update is called once per frame
	void Update () {
		if (keepWalking) {
			transform.Translate(Vector3.left * dir * speed * Time.deltaTime);
			if (Mathf.Abs(transform.position.x) > 18) {
				dir *= -1;
			}
		}
		
		if (willHit) {
			if (transform.position.x < 15.75f) {
				transform.Translate(Vector3.left * -2 * speed * Time.deltaTime);
			} else {
				transform.Translate(Vector3.up * 30f * Time.deltaTime);	
			}
		}
	}
	
	public void Stop() {
		keepWalking = false;
	}
	
	public void RunAndHit() {
		willHit = true;
	}
}
