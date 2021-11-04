using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouncherEnemy : MonoBehaviour
{
    private Controller controller;

    public GameObject enemy;

    void Start ()
    {
        controller = FindObjectOfType (typeof(Controller)) as Controller;
    }
    void createEnemy()
    {
        this.enemy.transform.position = this.enemy.GetComponent<Enemy>().initialPosition;
        this.controller.addEnemyIndex();
        this.enemy.GetComponent<Enemy>().setIndex(this.controller.getEnemieIndex());
        this.enemy.GetComponent<Enemy>().setCarryDamage(0);
        Instantiate (enemy);
    }
	
    public void spawnEnemy(){
        Invoke ("createEnemy",0f);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
