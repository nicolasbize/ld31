using UnityEngine;
using System.Collections;

public class Slicer : MonoBehaviour {

	private GameObject wheel;
	private GameObject holder;
	private bool enabled;
	private int currentStep = 0;
	private Vector3 originalPosition;
	private Vector3 wheelOrigPosition;

	// Use this for initialization
	void Start () {
		wheel = transform.FindChild("Spinwheel").gameObject;
		holder = transform.FindChild("Holder").gameObject;
		originalPosition = transform.position;
		wheelOrigPosition = wheel.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled) {
			if(currentStep == 0) {
				transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
				if(Mathf.Abs(transform.position.y - originalPosition.y) > 1) {
					currentStep = 1;
				}
			} else if (currentStep == 1) {
				wheel.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
				if(Mathf.Abs(wheel.transform.position.y - wheelOrigPosition.y) > 5) {
					currentStep = 2;
					wheel.GetComponent<SpinWheel>().Enable();
				}
			}
		}
	}
	
	public void Enable() {
		enabled = true;
	}
	
	
}
