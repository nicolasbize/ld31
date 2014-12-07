using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float life = 30f;
	
	// Update is called once per frame
	void FixedUpdate () {
		life -= 0.1f;
		if(life < 0){
			Destroy(gameObject);
		}
		
	
	}
}
