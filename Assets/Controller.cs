using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public int enemiesIndex;
    public GameObject tutorial;
    public int rangeTest;
    public int damageTest;
    public int layerTest;
    public float staminaTest;
    public int secondArrowTest;
    public Vector3 cameraPosition;
    public GameObject tryAgainCanvas;
    public GameObject gameOverRewardObject;
    public Text textLifeTryAgain;
    public Text textLifeGameOver;
    public Button buttonWatchAd;


    private bool watchedAd = false;


    public GameObject arrowHelp;

    public Button canvasExplosive;
    public Button canvasExtraArrow;

    public AudioSource audioSource;
    public AudioSource soundTrack;


    private GameObject arrowToBeLounched;

    private List<Enemy> enemies = new List<Enemy>();
    private static Controller _instance;

    public static Controller Instance
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


    //public int extraArrowsTest;
    public int explosiveArrow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Range",rangeTest);
        PlayerPrefs.SetInt("Damage",damageTest);
        PlayerPrefs.SetInt("Layer",layerTest);
        PlayerPrefs.SetInt("ExplosiveArrow",explosiveArrow);
        PlayerPrefs.SetFloat("Stamina", staminaTest);
        PlayerPrefs.SetInt("SecondArrow",secondArrowTest);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEnemy(Enemy gameObject)
    {
        this.enemies.Add(gameObject);   
    }

    public void removeEnemy(Enemy gameObject)
    {
        this.enemies.Remove(gameObject);
    }

    public void destroyAllEnemies(int vida)
    {
        ControllerSpawn.Instance.zerarFase();
        

        if (vida > 0)
        {
            this.tryAgainCanvas.SetActive(true);
            this.textLifeTryAgain.text = vida.ToString();

        }
        else if(!watchedAd)
        {
            this.textLifeGameOver.text = vida.ToString();
            this.gameOverRewardObject.SetActive(true);
            this.buttonWatchAd.interactable = true;
        }
        else
        {
            this.textLifeGameOver.text = vida.ToString();
            this.gameOverRewardObject.SetActive(true);
            this.buttonWatchAd.interactable = false;

        }
        
        for (int i = this.enemies.Count-1; i >=0; i--)
        {
            Enemy enemyIndex = this.enemies[i];
            this.removeEnemy(enemyIndex);
            Destroy(enemyIndex.gameObject);
            ControllerSpawn.Instance.delEnemy();
        }
    }

    public void finish()
    {
        Application.LoadLevel ("Menu");
    }

    public void gameOverReward()
    {
        this.gameOverRewardObject.SetActive(false);
        this.watchedAd = true;
        ControllerSpawn.Instance.tryAgain();
        Score.Instance.addVida();
    }
    
    public void tryAgain()
    {   
        ControllerSpawn.Instance.tryAgain();
        tryAgainCanvas.SetActive(false);
    }

    public void setExplosiveArrow()
    {
        this.arrowToBeLounched.gameObject.GetComponent<Arrow>().setActiveExplosiveArrow();
    }

    public void mute()
    {
        audioSource.mute = !audioSource.mute;
        soundTrack.mute = !soundTrack.mute;
    }

    public void playSound(AudioClip audioClip,Vector3 position)
    {
        if (!audioSource.mute)
        {
            AudioSource.PlayClipAtPoint (audioClip, position);
        }
    }
    
    public void playSound(AudioClip audioClip)
    {
        if (!audioSource.mute)
        {
            AudioSource.PlayClipAtPoint (audioClip, cameraPosition);
        }
    }
    public void setArrow(GameObject gameObject)
    {
        this.arrowToBeLounched = gameObject;
    }

    public void addEnemyIndex()
    {
        this.enemiesIndex++;
    }

    public int getEnemieIndex()
    {
        return enemiesIndex;
    }

    public void upgradeRange(int qtd)
    {
        int range = PlayerPrefs.GetInt("Range",400);
        PlayerPrefs.SetInt("Range",range+qtd);
    }
    
    public void upgradeDamage(int qtd)
    {
        int dano = PlayerPrefs.GetInt("Damage",1);
        PlayerPrefs.SetInt("Damage",dano+qtd);
    }
    
    public void upgradeLayer(int qtd)
    {
        int layer = PlayerPrefs.GetInt("Layer",1);
        PlayerPrefs.SetInt("Layer",layer+qtd);
    }
    
    public void upgradeSecondArrow(int qtd)
    {
        int SecondArrow = PlayerPrefs.GetInt("SecondArrow",0);
        PlayerPrefs.SetInt("SecondArrow",SecondArrow+qtd);
        this.canvasExtraArrow.interactable = true;
    }

    public void activeSecondArrow()
    {
        this.arrowToBeLounched.gameObject.GetComponent<Arrow>().setActiveSecondArrow();
    }
    

    /*public void upgradeExtraArrows(int qtd)
    {
        int extrasArrows = PlayerPrefs.GetInt("ExtraArrows",0);
        PlayerPrefs.SetInt("Layer",extrasArrows+qtd);
    }*/
    public void upgradeExplosive(int qtd)
    {
        int ExplosiveArrow = PlayerPrefs.GetInt("ExplosiveArrow",1);
        PlayerPrefs.SetInt("ExplosiveArrow",ExplosiveArrow+qtd);
        this.canvasExplosive.interactable = true;
    }
    

    public void destroyTutorial()
    {
        Destroy(tutorial);
    }


    public void passLevel()
    {
        this.watchedAd = false;
    }
}
