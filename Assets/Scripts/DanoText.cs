using UnityEngine;
using System.Collections;

public class DanoText : MonoBehaviour {
	public TextMesh number;
	public TextMesh shadow;
	public Louncher louncher;
	public GameObject thiss;
	public int time = 0;
	public int duracao=0;

	void Start () {
		this.setNumber(PlayerPrefs.GetInt("Dano",1));
	}

	void Update(){
		this.fading ();
	}
	
	// Update is called once per frame

	public void setNumber(int number){
		this.number.text = number.ToString();
		this.shadow.text = number.ToString ();
	}

	public void fading(){
		time = time + 1;
		if (time >duracao){
			number.color = new Color (number.color.r, number.color.g, 255, number.color.a-1);
			shadow.color = new Color (shadow.color.r, shadow.color.g, 255,number.color.a-1);
			if(number.color.a<=0){
				Destroy (thiss);
			}
			time = 0;
		}
	}

}
