using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {
	
	public LayerMask mouseLayer;
	public LayerMask textLayer;
	public Transform hookShade;
	
	private LineRenderer line;
	
	void Start() {
		line = gameObject.AddComponent<LineRenderer>();
		line.SetWidth(0.1f, 0.1f);
		line.SetVertexCount(2);
		line.material.color = Color.blue;
		line.enabled = false;
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
			if (Physics.Raycast (hookRay, out hitHook, 10f, textLayer)) {
				hookShade.position = Vector3.Lerp (hitHook.point, hookStart, 0.5f);
				float ratio = hitHook.distance / 2;
				hookShade.localScale = new Vector3(0.1f, ratio, 0.1f);
				Debug.Log (hitHook.distance);
				hookShade.LookAt(hitHook.point);
				hookShade.RotateAround(hookShade.position, Vector3.forward, 90);
				hookShade.renderer.enabled = true;
			} else {
				hookShade.renderer.enabled = false;
			}
			
		}
	}
}
