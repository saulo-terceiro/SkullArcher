using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour {

	public Canvas canvas;

	void Start () {
		this.canvas.enabled = false;
	}



	public void PauseLevel(){
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			this.canvas.enabled = true;
		}
		else
		{
			this.canvas.enabled = false;
			Time.timeScale = 1;
		}
	}


	public void LoadLevel(string name){
		Time.timeScale = 1;
		Application.LoadLevel (name);
	}







	public void QuitGame(string name){
		Application.Quit ();
	}

	void CreateCanvas(){
		Instantiate (canvas);
	}

}
