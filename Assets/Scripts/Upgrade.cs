using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {

	public TextMesh dano;
	public TextMesh range;
	public TextMesh stamina;

	// Use this for initialization

	void Start () {
		
	}
	
	public void upgradeRange(){
		int nivel = PlayerPrefs.GetInt ("rangeNivel", 1);
		int preco = (nivel * nivel) * 10;
		int coins = PlayerPrefs.GetInt ("coins", 0);
		if (coins > preco) {
			coins = coins - preco;
			PlayerPrefs.SetInt ("rangeNivel", nivel + 1);
			this.atualizaRange ();
		} else {
			//Mandar mensagem que não tem coins o suficiente;
		}
	}

	void atualizaRange(){
		int newRange = (PlayerPrefs.GetInt ("rangeNivel", 1) * 100) + 300;
		PlayerPrefs.SetInt ("range", newRange);
	}

	public void upgradeDano(){
		int nivel = PlayerPrefs.GetInt ("danoNivel", 1);
		int preco = (nivel * nivel) * 10;
		int coins = PlayerPrefs.GetInt ("coins", 0);
		if (coins > preco) {
			coins = coins - preco;
			PlayerPrefs.SetInt ("danoNivel", nivel + 1);
			this.danoRange ();
		} else {
			//Mandar mensagem que não tem coins o suficiente;
		}
	}

	void danoRange(){
		int newDano = (PlayerPrefs.GetInt ("danoNivel", 1) * 10) + 20;
		PlayerPrefs.SetInt ("dano", newDano);
	}

	public void LoadLevel(string name){
		Time.timeScale = 1;
		Application.LoadLevel (name);
	}
}
