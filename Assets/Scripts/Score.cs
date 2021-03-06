using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public AudioClip bananaLose;
	public int qtdBananas;
	int vida;
	public TextMesh shadowBananas;
	public TextMesh numberBananas;
	public TextMesh shadowScore;
	public TextMesh numberScore;
	

	int points;
	private static Score _instance;

	public static Score Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
	}
	void Start () {
		
		vida = qtdBananas;
		this.AtualizarVida();
		numberScore.text = 0.ToString();
		shadowScore.text = 0.ToString();

	}
	


	public void Dano(int dano){
		vida = vida - 1;
		this.AtualizarVida();
		AudioSource.PlayClipAtPoint (bananaLose, transform.position);
		Controller.Instance.destroyAllEnemies(vida);

	}

	public void AtualizarVida()
	{
		numberBananas.text = vida.ToString();
		shadowBananas.text = vida.ToString();
	}

	public void addVida()
	{
		if (this.vida < 3)
		{
			this.vida = vida + 1;
			this.AtualizarVida();
		}
	}

	public void earnCoins(int coins){
		int totalCoins = PlayerPrefs.GetInt ("coins",0) + coins;
		PlayerPrefs.SetInt ("coins",totalCoins);
	}

	public void earnPoints(int points){
		this.points = this.points + points;
		numberScore.text = this.points.ToString ();
		shadowScore.text = this.points.ToString ();
		if(this.points>PlayerPrefs.GetInt("HighScore",0)){
			PlayerPrefs.SetInt ("HighScore", this.points);
		}
	}

}
