using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {


	public float speed = 2f;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.left * speed * Time.deltaTime);
		if(transform.position.x < -80f) {
			Vector3 p = transform.position;
			transform.position = new Vector3(-p.x, p.y, p.z);
		}
	}
}
