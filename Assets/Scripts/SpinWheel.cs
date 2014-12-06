using UnityEngine;
using System.Collections;

public class SpinWheel : MonoBehaviour {

	public float spinSpeed = 30f;

	void Start() {
		transform.Rotate(Vector3.up * Random.Range(0, 360));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
	}
}
