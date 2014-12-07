using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject gameLogic;
	private int ennemyLayer;
	private int playerLayer;
	
	void Start() {
		ennemyLayer = LayerMask.NameToLayer("Ennemy");
		playerLayer = LayerMask.NameToLayer("Player");
	}
	
	
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == playerLayer) {
			gameLogic.GetComponent<Restart>().SetCheckpoint(transform.position);
			gameObject.GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		} else if(collider.gameObject.layer == ennemyLayer) {
			Destroy(gameObject);
		}

	}
}
