using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	public Text instruction;
	private float typingSpeed = 1f;
	private float curTimer = 0f;
	private int curLength = 0;
	private bool done = false;
	
	private string introText = "In a screen far, far away...\n" +
		"Our two heroes resided in the game's memory.\n" +
			"Mrs Cube and Mr Box had fallen deeply in love.\n" +
			"Their lives were filled with hope and joy.\n" +
			"They were expecting their first pixels.\n" +
			"What color would they be?\n" +
			"How many bytes would they measure?\n" +
			"But amongst all those questions, Mrs Cube became afraid..\n" +
			"What if she had a dead pixel...\n" +
			"How could they live with so little space on one screen?\n" +
			"Out of despair, Mrs Square ventured into the unknown...\n" +
			"By pushing the forbidden \"End Game\" Button...";
	
	// Update is called once per frame
	void FixedUpdate () {
		curTimer += 0.4f;
		Debug.Log(curTimer);
		if(curTimer > typingSpeed) {
			curTimer = 0;
			curLength++;
			string curText = introText.Substring(0, curLength);
			instruction.text = curText + "█";
			if(curText.EndsWith("\n")) {
				curTimer = -20f;
			}
			if (curLength == introText.Length) {
				done = true;
				
			}
		}
	}
}
