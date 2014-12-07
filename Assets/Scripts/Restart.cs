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
	public GameObject screen;
	private Vector3 restartSpawn = new Vector3(0, 4f, 0);
	private bool checkedpoint = false;
	public bool inGame = false;

	void Update() {
		if (running) {
			if (!checkedpoint && curStep < 6) {
				slicers[curStep].GetComponent<Slicer>().Enable();
				slicers[11-curStep].GetComponent<Slicer>().Enable();
			}
			if (checkedpoint) {
				for(int i=0; i<12; i++) {
					slicers[i].GetComponent<Slicer>().Enable();
				}
			}
			curTimer += 0.1f;
			if (curTimer > 8f) {
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
	
	public void SetCheckpoint(Vector3 spawn) {
		restartSpawn = spawn;
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Text")){
			g.GetComponent<CreditText>().Checkpoint();
		}
		checkedpoint = true;
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
		curTimer = 0f;
		curStep = 0;
		running = true;
	}
	
	public void CompleteGame() {
		running = false;
		RemoveIntroElements(false);
		Destroy(GameObject.Find ("Credits"));
		// play music
	}
	
	private void RemoveIntroElements(bool respawnPlayer) {
		GetComponent<Intro>().Finish();
		GameObject obj = GameObject.Find("EndGamePink");
		if(obj != null) { Destroy(obj); }
		obj = GameObject.Find ("mrscubechar");
		if(obj != null) { Destroy(obj); }
		obj = GameObject.Find ("mrboxchar");
		if(obj != null && respawnPlayer) { Destroy(obj); }
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Text")){
			g.GetComponent<CreditText>().Restart();
		}
		foreach(Transform t in spinParent.transform) {
			Destroy(t.gameObject);
		}
		screen.GetComponent<ScreenShake>().Disable();
		
	}
	
	public void PlacePlayer() {
		GameObject p = Instantiate(player, restartSpawn, Quaternion.identity) as GameObject;
		p.GetComponent<PlayerMovement>().gameLogic = gameObject;
		p.name = "mrboxchar";
	}
	
	private void PlaceSlicers() {
		for(int i=0; i<12; i++) {
			Vector3 v = spinParent.transform.position;
			slicers[i] = Instantiate(slicer, new Vector3(v.x + i * 3.8f, v.y, v.z), Quaternion.identity) as GameObject;
			slicers[i].transform.parent = spinParent.transform;
			
		}
	}
}
