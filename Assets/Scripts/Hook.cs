using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {
	
	public LayerMask mouseLayer;
	public LayerMask textLayer;
	private bool hooked;
	private Ray hookRay;
	private float hookLength = 10;
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Vector3 hookStart = transform.position + new Vector3(0, 1, 0);
//		Debug.DrawRay(ray.origin, ray.direction * 100);
		
		if (Physics.Raycast(ray, out hitInfo, 100f, mouseLayer)) {
//			Debug.DrawLine(ray.origin, hitInfo.point);
			Ray hray = new Ray(hookStart, hitInfo.point - transform.position);
//			Debug.DrawRay(hray.origin, hray.direction * hookLength);
			RaycastHit hitHook;
			if (!hooked && Physics.Raycast (hookRay, out hitHook, hookLength, textLayer)) {
				hookRay = new Ray(hray.origin, hray.direction * hitHook.distance);
				Debug.DrawRay(hray.origin, hray.direction * hitHook.distance);
			} else {

			}
			
		}
	}
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red; 
		Gizmos.DrawRay(hookRay);
	}
}
