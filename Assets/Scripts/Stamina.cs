using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

	public SpriteRenderer rawImageStamina;
	public Transform transformRawImageStamina;
	public GameObject tiredButton;
	bool tired ;
	float stamina;
	float staminaTotal;
	float y;
	float z;
	float xBarra;

	void Start () {
		stamina = PlayerPrefs.GetFloat ("Stamina", 1000);
		staminaTotal = PlayerPrefs.GetFloat ("Stamina", 1000);
		rawImageStamina.color = Color.green;
		this.atualizaBarra();
	}

	void FixedUpdate(){
		if (this.tired) {
			stamina = stamina + staminaTotal/300;
			if (stamina > 300)
			{			
				this.tiredButton.SetActive(false);
				tired = false;
				rawImageStamina.color = Color.green;
			}
		}else if (stamina <= staminaTotal-5) {
			stamina = stamina + staminaTotal/200;
		}else{
			stamina = staminaTotal;
		}
		this.atualizaBarra ();

	}
	
	// Update is called once per frame
	public void lounch(){
		stamina = stamina - 500;
		if (stamina <= 0) {
			stamina = 0;
			tired = true;
			this.tiredButton.SetActive(true);
			rawImageStamina.color = Color.red;

		}
		this.atualizaBarra ();
		
	}

	void atualizaBarra()
	{
		this.transformRawImageStamina.localScale  = new Vector3(1,stamina / staminaTotal,1);
	}


	public bool getTired(){
		return this.tired;	
	}

	public void upgradeStamina(int qtd)
	{
		this.staminaTotal = this.staminaTotal + qtd;
	}

	public void lounchExplosive()
	{
		stamina = stamina - 250;
		if (stamina <= 0) {
			stamina = 0;
			tired = true;
			this.tiredButton.SetActive(true);
			rawImageStamina.color = Color.red;

		}
		this.atualizaBarra ();
	}
}
