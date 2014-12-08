using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

	private AudioSource introMusic;
	private AudioSource mainMusic;
	private AudioSource endMusic;
	private string currentSong;

	void Awake() {
		introMusic = transform.Find("IntroMusic").gameObject.GetComponent<AudioSource>();
		mainMusic = transform.Find("MainMusic").gameObject.GetComponent<AudioSource>();
		endMusic = transform.Find("EndMusic").gameObject.GetComponent<AudioSource>();
	}

	public void Play(string s){
		if (s != currentSong) {
			currentSong = s;
			StopAllSongs();
			if(s == "intro") {
				introMusic.Play();
			} else if (s == "main") {
				mainMusic.Play();
			} else {
				endMusic.Play();
			}
		}
		
	}
	
	
	public void StopAllSongs(){
		if(introMusic != null) {
			introMusic.Stop ();
		}
		if(mainMusic != null) {
			mainMusic.Stop ();
		}
		if(endMusic != null) {
			endMusic.Stop ();
		}
	}
	
}
