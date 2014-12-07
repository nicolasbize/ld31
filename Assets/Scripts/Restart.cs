using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public GameObject spinParent;
	public GameObject slicer;
	private float curTimer = 0f;
	private int curStep = 0;
	private bool running = false;
	private GameObject[] slicers = new GameObject[12];
	public GameObject player;
	public GameObject credits;
	public GameObject screen;
	public bool inGame = false;

	void Update() {
		if (running) {
			if (curStep < 6) {
				slicers[curStep].GetComponent<Slicer>().Enable();
				slicers[11-curStep].GetComponent<Slicer>().Enable();
			}
			curTimer += 0.1f;
			if (curTimer > 13f) {
				curStep++;
				curTimer = 0f;
			}
		}
		
		// skip intro
		if(Input.GetKeyDown(KeyCode.Escape) && !inGame) {
			RestartGame(true);
		}
		
	}

	// serialize all the text's positions to restaure it in case of death
	public void IntroDone(){
		inGame = true;
	}
	
	public void RestartGame(bool respawnPlayer) {
		IntroDone();
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Ennemy")) {
			Destroy(obj);
		}
		RemoveIntroElements(respawnPlayer);
		if(respawnPlayer) {
			PlacePlayer();
		}
		PlaceSlicers();
		PlaceCredits();
		curTimer = 0f;
		curStep = 0;
		running = true;
	}
	
	private void RemoveIntroElements(bool respawnPlayer) {
		GetComponent<Intro>().Finish();
		GameObject obj = GameObject.Find("EndGamePink");
		if(obj != null) { Destroy(obj); }
		obj = GameObject.Find ("mrscubechar");
		if(obj != null) { Destroy(obj); }
		obj = GameObject.Find ("mrboxchar");
		if(obj != null && respawnPlayer) { Destroy(obj); }
		obj = GameObject.Find ("Credits");
		if(obj != null) { Destroy(obj); }
		screen.GetComponent<ScreenShake>().Disable();
		
	}
	
	public void PlacePlayer() {
		Instantiate(player, new Vector3(0, 3.65f, 0), Quaternion.identity);
	}
	
	private void PlaceCredits() {
		Instantiate(credits, new Vector3(-1, 57.4f, 4), Quaternion.identity);
	}
	
	private void PlaceSlicers() {
		for(int i=0; i<12; i++) {
			Vector3 v = spinParent.transform.position;
			slicers[i] = Instantiate(slicer, new Vector3(v.x + i * 3.8f, v.y, v.z), Quaternion.identity) as GameObject;
			slicers[i].transform.parent = spinParent.transform;
		}
	}
}
