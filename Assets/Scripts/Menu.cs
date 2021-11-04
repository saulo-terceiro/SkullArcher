using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization


	public void LoadLevel(string name){
		Time.timeScale = 1;
		Application.LoadLevel (name);
	}

	public void QuitGame(string name){
		Application.Quit ();
	}
		
}
