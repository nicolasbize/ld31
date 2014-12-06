using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {
	
	public LayerMask mouseLayer;
	public LayerMask textLayer;
	public Transform hook;
	private Transform hand;
	private bool hooked;
	public Transform grapHook;
	
	void Start () {
		hand = transform.Find ("Hand");
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Vector3 hookStart = transform.position + new Vector3(0, 1, 0);
//		Debug.DrawRay(ray.origin, ray.direction * 100);
		
		if (Physics.Raycast(ray, out hitInfo, 100f, mouseLayer)) {
//			Debug.DrawLine(ray.origin, hitInfo.point);
			Ray hookRay = new Ray(hookStart, hitInfo.point - transform.position);
			Debug.DrawRay(hookRay.origin, hookRay.direction * 10);
			RaycastHit hitHook;
			if (!hooked && Physics.Raycast (hookRay, out hitHook, 10f, textLayer)) {
				hook.position = Vector3.Lerp (hitHook.point, hookStart, 0.5f);
				float ratio = hitHook.distance / 2;
				hook.localScale = new Vector3(0.1f, ratio, 0.1f);
				hook.LookAt(hitHook.point);
				hook.RotateAround(hook.position, Vector3.forward, 90);
				hook.renderer.enabled = true;
				
				grapHook.position = hitHook.point;
				grapHook.renderer.enabled = true;
				if (Input.GetMouseButton(0) && !hooked) {
					hooked = true;
					grapHook.hingeJoint.connectedBody = hook.gameObject.rigidbody;
					hook.hingeJoint.connectedBody = hand.gameObject.rigidbody;
				}
			} else {
				if (!hooked) {
					hook.renderer.enabled = false;
					grapHook.renderer.enabled = false;
				}
			}
			
		}
	}
}
