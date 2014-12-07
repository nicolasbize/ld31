using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenShake : MonoBehaviour {

	public bool enabled = false;
	public bool switching = false;
	private float timer = 0f;
	private float timeLength = 200f;
	public Text instruction;
	public GameObject gameLogic;
	
	// Update is called once per frame
	void FixedUpdate () {
		if(enabled) {
			float max = 2 * timer / timeLength;
			if (max < 1) {
				instruction.text = "SYSTEM ERROR - OUT OF BOUNDS EXCEPTION - SYSTEM DEFENSE READY...";
			} else {
				instruction.text = "";
			}
			transform.localEulerAngles = new Vector3(270, 180, Random.Range(-max, max));
			timer += 0.5f;
			if(timer > timeLength) {
				enabled = false;
				transform.localEulerAngles = new Vector3(270, 180, 0);
				gameLogic.GetComponent<Restart>().IntroDone();
				gameLogic.GetComponent<Restart>().RestartGame(false);
			}
		}
		if(switching) {
			timer += 0.5f;
			if(timer > 10) {
				transform.FindChild("unharmed").gameObject.GetComponent<MeshRenderer>().enabled = false;
				transform.FindChild("brokenscreen").gameObject.GetComponent<MeshRenderer>().enabled = true;
			}
			
		}
	}
	
	public void SwitchMesh() {
		gameLogic.GetComponent<Restart>().CompleteGame();
		timer = 0f;
		switching = true;
		
	}
	
	public void Enable(){
		enabled = true;
		timer = 0f;
	}
	
	public void Disable(){
		enabled = false;
	}
}
