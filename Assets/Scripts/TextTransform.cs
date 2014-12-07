using UnityEngine;
using System.Collections;

public class TextTransform : MonoBehaviour {

	
	// Because we want to just drag and drop the meshes,
	// we need to do some setup for everything to work nicely.
	public GameObject SparkEmitter;
	public GameObject TextExplosion;
	
	// Use this for initialization
	void Start () {
		GameObject credits = GameObject.Find("Credits");
		var parent = new GameObject();
		parent.name = gameObject.name.ToUpper();
		parent.layer = LayerMask.NameToLayer("Text");
		parent.transform.position = transform.position;
		parent.transform.parent = credits.transform;
		transform.parent = parent.transform;
		BoxCollider collider = parent.AddComponent<BoxCollider>();
		Vector3 p = transform.localPosition;
		collider.size = gameObject.renderer.bounds.size;
		collider.center = new Vector3(p.x, p.y + collider.size.y / 2, p.z);
		GameObject spark = Instantiate(SparkEmitter) as GameObject;
		spark.transform.parent = parent.transform;
		spark.name = "Emitter";
		spark.transform.localPosition = Vector3.zero;
		Vector3 ls = spark.transform.localScale;
		spark.transform.localScale = new Vector3(collider.size.x, ls.y, ls.z);
		Rigidbody rb = parent.AddComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
		gameObject.name = "Text";
		
		// add behaviour last as it needs to refer the other components
		CreditText ctext = parent.AddComponent<CreditText>();
		ctext.textExplosion = TextExplosion;
		ctext.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
