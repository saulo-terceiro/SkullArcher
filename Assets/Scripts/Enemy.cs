using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
	public float velocidade;
	public Vector2 velocity;
	public Rigidbody2D rb;
	Score scoreManager;
	public int points;
	public int dano;
	public int life;
	public int coins;
	public bool upAndDown;
	public bool randomY;
	public GameObject deadBallon;
	public bool hasDeadBallon;
	bool upAtMiddle;
	public Vector3 initialPosition;
	private ControllerSpawn controllerSpawn;
	public int myIndex;
	public int carryDamage;
	public GameObject proprio;
	private Controller controller;
	private bool damaged = false;


	public override bool Equals(object other)
	{
		if (other == null)
		{
			return false;
		}
		else
		{
			Enemy enemyCompare = (Enemy) other;
			if (enemyCompare.getIndex().Equals(this.myIndex))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	// Use this for initialization
	void Start()
	{
		ControllerSpawn.Instance.addEnemy();
		Controller.Instance.addEnemy(this);
		if (this.upAndDown) {
			float x = Random.Range (1, 100);
		}
		if (randomY) {
			float x = Random.Range (154, 513);
			transform.localPosition = new Vector3 (transform.position.x, x/100, transform.position.z);
		}
		Vector2 velocity = new Vector2(velocidade, 0);
		rb.velocity  = velocity;
		scoreManager = FindObjectOfType (typeof(Score)) as Score;
		if (this.carryDamage > 0)
		{
			this.Die(carryDamage-1);
		}

	}

	
	void Update(){
		Vector2 velocity = new Vector2(velocidade, rb.velocity.y);
		rb.velocity  = velocity;
	
		if (transform.position.x < -6.51f) {
			this.damageBanana ();
		}
		
		if (transform.position.y < 2.2f) {
			this.upAtMiddle = false;
		} else {
			this.upAtMiddle = true;
		}
		
		if (upAndDown) {
			if (upAtMiddle) {
				rb.gravityScale = 1;
			} else {
				rb.gravityScale = -1;
			}
		}

	}
	
	public int getCarryDamage()
	{
		return this.carryDamage;
	}

	public void setCarryDamage(int setCarryDamage)
	{
		this.carryDamage = setCarryDamage;
	}
	
	void createEnemy()
	{
		this.controller = FindObjectOfType (typeof(Controller)) as Controller;
		Vector3 proprioPosition = this.proprio.GetComponent<Enemy>().initialPosition;
		proprioPosition.x = 20.30f;
		this.proprio.transform.position = proprioPosition;
		this.controller.addEnemyIndex();
		this.proprio.GetComponent<Enemy>().setIndex(this.controller.getEnemieIndex());
		this.proprio.GetComponent<Enemy>().setCarryDamage(0);
		Instantiate (proprio);
	}
	
	public void spawnEnemy(){
		Invoke ("createEnemy",0f);
	}
		

	void damage(int damage){
		this.life = this.life - damage;
		if (life <= 0) {
			scoreManager.earnCoins (this.coins);
			scoreManager.earnPoints (this.points);
			this.Die(damage-1);	
		} 
	}

	void damageBanana(){
		this.scoreManager.Dano (this.dano);
	}
	

	void CreateDeadBalloon(){
		deadBallon.GetComponent<Enemy>().setIndex(this.myIndex);
		deadBallon.GetComponent<Enemy>().setCarryDamage(this.carryDamage);
		Instantiate (deadBallon);
	}

	public void spawnDeadBalloon(){
		Invoke ("CreateDeadBalloon",0f);
	}

	void Die(int damage)
	{
		if (this.hasDeadBallon)
		{
			Vector2 positionBallon2 = this.deadBallon.transform.position;
			this.deadBallon.transform.position = this.transform.position;
			this.setCarryDamage(damage);
			this.spawnDeadBalloon();
		}

		if (!damaged)
		{
			ControllerSpawn.Instance.delEnemy();
			damaged = true;

		}
		Controller.Instance.removeEnemy(this);
		Destroy (this.gameObject);
	}


	public void setIndex(int getEnemieIndex)
	{
		this.myIndex = getEnemieIndex;
	}

	public int getIndex()
	{
		return this.myIndex;
	}

	public bool getDamagedEnemy()
	{
		return this.damaged;
	}
}
