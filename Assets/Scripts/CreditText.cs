using UnityEngine;
using System.Collections;

public class CreditText : MonoBehaviour {

	private float dir = -1f;
	public bool enabled = true;
	public float speed = 3f;
	public bool isDying = false;
	private bool exploded = false;
	private Transform text;
	private Transform emitter;
	public GameObject textExplosion;
	
	private float deathTime = 1f;
	private bool shifting = false;
	private bool shifted = false;
	private Vector3 initialPos;
	

	// Use this for initialization
	void Start () {
		text = transform.FindChild("Text");
		emitter = transform.FindChild ("Emitter");
		initialPos = transform.position;
	}
	
	public void Restart() {
		transform.position = initialPos;
		shifting = false;
		shifted = false;
		exploded = false;
		enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enabled) {
			float s = isDying ? speed / 10 : speed;
			if (!shifting) {
				transform.Translate(Vector3.up * dir * s * Time.deltaTime);
				if (!shifted && transform.localPosition.y < -19f) {
					shifting = true;
                }
			} else {
				transform.Translate(Vector3.forward * dir * s * Time.deltaTime);
				if (transform.localPosition.z < -4f){
					shifting = false;
					shifted = true;
				}
			}
		}
		
		if (isDying && !exploded) {
			text.localEulerAngles = new Vector3(0, 180, Random.Range(-2, 2));
			deathTime -= 0.01f;
			if (deathTime < 0 && !exploded) {
				exploded = true;
				emitter.gameObject.GetComponent<ParticleSystem>().Stop();
				GameObject explode = Instantiate(textExplosion, gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(text.gameObject);
				gameObject.GetComponent<BoxCollider>().enabled = false;
				
			}
		}
	}
	
	public void Die(){
		isDying = true;
		emitter.gameObject.GetComponent<ParticleSystem>().Play();
	}
	
	public void Enable() {
		enabled = true;
	}

}
