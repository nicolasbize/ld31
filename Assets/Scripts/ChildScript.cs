using UnityEngine;
using System.Collections;

public class ChildScript : MonoBehaviour {

	private float force;
	private float my;
	private float gravity = 0.1f;
	
	// Use this for initialization
	void Start () {
		force = Random.Range (3, 5);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 p = transform.localPosition;
		if(p.y < -6) {
			my = force;
		}
		my -= gravity;
		transform.Translate (Vector3.up * my * Time.deltaTime);
		
	}
}
