using System;
using UnityEngine;
using System.Collections;

public class Louncher : MonoBehaviour
{
	public Animator animatorPlayer;
	public GameObject missile;
	public GameObject redBallon;
	public GameObject blueBallon;
	public GameObject blueBallonMove;


	public DanoText dano;
	
	void Start () {
		Invoke ("CreateBarrel",0f);
	}



	public void spawn(){
		Invoke ("CreateBarrel", 0f);
	}


	void CreateBarrel(){
		Instantiate (missile);
	}


	public void spawnText(Vector3 position){
		this.dano.transform.position = position;
		Invoke ("CreateText",0f);
	}

	void CreateText(){
		Instantiate (dano);
	}
	
	void CreateRedBalloon()
	{
		this.redBallon.transform.position = this.redBallon.GetComponent<Enemy>().initialPosition;
		Instantiate (redBallon);
	}
	
	public void spawnRedBalloon(){
		Invoke ("CreateRedBalloon",0f);
	}

	
	void CreateBlueBalloon(){
		this.blueBallon.transform.position = this.blueBallon.GetComponent<Enemy>().initialPosition;
		Instantiate (blueBallon);
	}
	
	void CreateBlueBalloonMove(){
		this.blueBallonMove.transform.position = this.blueBallonMove.GetComponent<Enemy>().initialPosition;
		Instantiate (blueBallonMove);
	}


	


	public void spawnBlueBallon()
	{
		Invoke ("CreateBlueBalloon",0f);
	}

	public void spawnBlueBallonMove()
	{
		Invoke ("CreateBlueBalloonMove",0f);
	}
}
