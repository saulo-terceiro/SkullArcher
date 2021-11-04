using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine.UI;

public class ControllerSpawn : MonoBehaviour
{

	public List<Fase> fases = new List<Fase>();
	private int qtdEnemies;
	int time=0;
	int faseIndex = 0;
	int grupoIndex = 0;
	int vezesRespawnInimigo=0;
	bool passouDeFase;
	private bool inimigoOn = false;
	private bool lose = false;
	public int tempoCheckNewEnemy = 0;
	private bool choosingSkill = false;
	
	private static ControllerSpawn _instance;

	
	public static ControllerSpawn Instance
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
	void Start ()
	{
		time = 0;
		passouDeFase = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		if (Time.timeScale == 1 && !passouDeFase && !lose) {
			this.Control ();
		}else if (passouDeFase && qtdEnemies==0 && tempoCheckNewEnemy>10 && !choosingSkill)
		{
			Debug.Log(tempoCheckNewEnemy);
			fases[faseIndex-1].chooseSkill.SetActive(true);
			Score.Instance.addVida();
			Controller.Instance.passLevel();
			tempoCheckNewEnemy = 0;
			choosingSkill = true;
		}else if (passouDeFase && qtdEnemies == 0)
		{
			tempoCheckNewEnemy++;
		}
	}

	void Control(){
		time = time + 1;
		if ((time > fases[faseIndex].grupos[grupoIndex].tempoDeRespawn) || vezesRespawnInimigo==0&& grupoIndex==0 ){
			time = 0;
			int x = Random.Range (0, fases[faseIndex].grupos[grupoIndex].inimigos.Count);
			fases[faseIndex].grupos[grupoIndex].inimigos[x].spawnEnemy();
			vezesRespawnInimigo++;
			if (vezesRespawnInimigo >= fases[faseIndex].grupos[grupoIndex].tamanho)
			{
				vezesRespawnInimigo = 0;
				grupoIndex++;
				if (grupoIndex >= fases[faseIndex].grupos.Count)
				{
					if (faseIndex+1<  fases.Count)
					{
						faseIndex++;
						passouDeFase = true;
					}
					grupoIndex = 0;
				}
			}
			
		}
	}

	public void zerarFase()
	{
		this.grupoIndex = 0;
		this.vezesRespawnInimigo = 0;
		if (passouDeFase)
		{
			passouDeFase = false;
			faseIndex = faseIndex - 1;
		}
		lose = true;
	}
	
	

	public void tryAgain()
	{
		lose = false;
	}

	public void choosedSkill()
	{
		passouDeFase = false;
		fases[faseIndex-1].chooseSkill.SetActive(false);
		choosingSkill = false;

	}
	
	public void addEnemy()
	{
        this.qtdEnemies++;
        

	}
	
	public void delEnemy()
	{
		this.qtdEnemies--;
	}
	
	

}
