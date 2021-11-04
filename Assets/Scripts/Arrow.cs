using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Arrow : MonoBehaviour
{
	public List<int> listIndexEnemies = new List<int>();
	public AudioClip jab;
	public Rigidbody2D barrel;
	public SpriteRenderer thisSpriteRenderer;
	Louncher louncher;
	Stamina stamina;
	float x1 = 0f;
	float y1 = 0f;
	float x2;
	float y2;
	private int touchMaxIndex = -1;
	private int touchSettled;
	bool down = true; //checking if the barrel is already louched;
	public CircleCollider2D collider;
	float velocityLimit;
	bool first = true;
	private bool pushArcher = false;
	int dano;
	int arrowLife;
	private bool explosiveArrow = false;
	public GameObject explosive;
	public bool isSecondArrow;
	public Vector2 forceSecondArrow;
	public GameObject secondArrow;
	protected bool secondArrowActive = false;
	

	

	// Use this for initialization
	void Start ()
	{
		
		louncher = FindObjectOfType (typeof(Louncher)) as Louncher;
		Controller.Instance.setArrow(this.gameObject);
		this.thisSpriteRenderer.enabled = false;
		stamina = FindObjectOfType (typeof(Stamina)) as Stamina;
		if (isSecondArrow)
		{
			this.dano = PlayerPrefs.GetInt("Damage", 1);
			int layer = PlayerPrefs.GetInt("Layer", 1);
			this.arrowLife = layer - 1;
			barrel.AddForce(forceSecondArrow);
			this.thisSpriteRenderer.enabled = true;
			barrel.gravityScale = 1;
			down = false;
			this.collider.enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.barrel.velocity.y != 0 || this.barrel.velocity.x != 0)
		{
			float angle = Mathf.Atan2(this.barrel.velocity.y, this.barrel.velocity.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
		}
		
		if (stamina.getTired ()==false) {
			this.ifOk ();
		}
		

		
	}
	void ifOk(){
		if (down)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (i > touchMaxIndex )
				{
					x1 = Input.touches[i].position.x;
					y1 = Input.touches[i].position.y;
					touchMaxIndex=i;
				}else if (i==touchMaxIndex)
				{
					x2 = Input.touches[i].position.x;
					y2 = Input.touches[i].position.y;
					float yDelta = y2 - y1;
					float xDelta = x2 - x1;
					float pitagoras = Mathf.Sqrt((xDelta * xDelta) + (yDelta * yDelta));
					if (pitagoras > 20 &&  !pushArcher)
					{
						this.calculateArrowHelp();
						SkelletonAnimator.Instance.push();
						pushArcher = true;
					}
				}
			}

			if (touchMaxIndex >= 0 && Input.touchCount < 1)
			{
				this.calculate();
			}
			
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.pushFisrtClick();
			}
			else if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				this.release();
			}else if (Input.GetKey(KeyCode.Mouse0))
			{
				this.pushing();
				this.calculateArrowHelp();
			}
			
		}
	}

	public void calculateArrowHelp()
	{

		float yDelta = y2 - y1;
		float xDelta = x2 - x1;
		float angle = Mathf.Atan2(yDelta, xDelta) * Mathf.Rad2Deg;
		float pitagoras = Mathf.Sqrt((xDelta * xDelta) + (yDelta * yDelta));
		float porc = 1;
		this.velocityLimit = PlayerPrefs.GetInt("Range", 400);

		if (pitagoras < velocityLimit)
		{
			porc = pitagoras / velocityLimit;
		}
		Controller.Instance.arrowHelp.transform.localScale = new Vector3(1* porc,1,1);
		Controller.Instance.arrowHelp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Controller.Instance.arrowHelp.SetActive(true);

	}

	public void setActiveSecondArrow()
	{
		if (!secondArrowActive)
		{
			this.secondArrowActive = true;
			stamina.lounchExplosive();
		}
		
	}
	
	void pushFisrtClick(){
		x1 = Input.mousePosition.x;
		y1 = Input.mousePosition.y;
	}
	
	void release(){
		x2 = Input.mousePosition.x;
		y2 = Input.mousePosition.y;
		SkelletonAnimator.Instance.release();
		this.calculate();
		Controller.Instance.arrowHelp.SetActive(false);
	}

	void pushing()
	{
		x2 = Input.mousePosition.x;
		y2 = Input.mousePosition.y;
		float yDelta = y2 - y1;
		float xDelta = x2 - x1;
		float pitagoras = Mathf.Sqrt((xDelta * xDelta) + (yDelta * yDelta));

		if (pitagoras > 40 &&  !pushArcher)
		{
			this.calculateArrowHelp();
			SkelletonAnimator.Instance.push();
			pushArcher = true;
		}

	}

	void calculate()
	{
		
		this.dano = PlayerPrefs.GetInt("Damage", 1);
		int layer = PlayerPrefs.GetInt("Layer", 1);
		int secondArrowValue = PlayerPrefs.GetInt("SecondArrow", 0);
		this.arrowLife = layer - 1;
		this.velocityLimit = PlayerPrefs.GetInt("Range", 400);
		/*
		int extraArrows = PlayerPrefs.GetInt("ExtraArrows", 0);
		*/
		float yDelta = y2 - y1;
		float xDelta = x2 - x1;
		float pitagoras = Mathf.Sqrt((xDelta * xDelta) + (yDelta * yDelta));
		if (pitagoras > 20 &&Time.timeScale!=0)
		{
			SkelletonAnimator.Instance.release();

			if (velocityLimit < pitagoras)
			{
				float porc = velocityLimit / pitagoras;
				yDelta = yDelta * porc;
				xDelta = xDelta * porc;
				pitagoras = velocityLimit;
			}
			Vector2 angle = new Vector2(xDelta, yDelta);
			barrel.AddForce(angle);
			this.thisSpriteRenderer.enabled = true;
			barrel.gravityScale = 1;
			down = false;
			this.collider.enabled = true;
			stamina.lounch();
			if (secondArrowValue > 0 && this.secondArrowActive)
			{
				for (int i = 0; i < secondArrowValue; i++)
				{
					float yDelta2 = y2 - y1;
					float xDelta2 = x2 - x1;
					yDelta2 = yDelta2 + (200*(i+1));
					float pitagoras2 = Mathf.Sqrt((xDelta2 * xDelta2) + (yDelta2 * yDelta2));
					float porc2 = pitagoras / pitagoras2;
					yDelta2 = yDelta2 * porc2;
					xDelta2 = xDelta2 * porc2;
					Vector2 angle2 = new Vector2(xDelta2, yDelta2);
					this.secondArrow.GetComponent<Arrow>().forceSecondArrow = angle2;
					this.secondArrow.GetComponent<Arrow>().isSecondArrow = true;
					Instantiate(secondArrow);
				}	
				


			}
			louncher.spawn();
		}



	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		GameObject collisioner = other.gameObject;
		if(collisioner.CompareTag("enemy"))
		{
			int enemyIndex = collisioner.GetComponent<Enemy>().getIndex();
			bool damagedEnemy = collisioner.GetComponent<Enemy>().getDamagedEnemy();
			if (!this.listIndexEnemies.Contains(enemyIndex) && !damagedEnemy)
			{
				collisioner.SendMessage ("damage",this.dano);
				Controller.Instance.playSound(jab);
				this.checkExplosive();
				if (arrowLife == 0)
				{
					Die();
				}
				else
				{
					this.listIndexEnemies.Add(enemyIndex);
					arrowLife--;
				}
			}
			
		}else if (collisioner.CompareTag ("chao")) {
			this.checkExplosive();
			Die ();
		}
	}

	void checkExplosive()
	{
		if (explosiveArrow)
		{
			explosive.transform.position = this.transform.position;
			Instantiate(explosive);
			this.Die();
		}
	}

	void Die()
	{
		
		Destroy (this.gameObject);
	}
	
	public void setActiveExplosiveArrow()
	{
		if (!explosiveArrow)
		{
			this.explosiveArrow = true;
			stamina.lounch();
		}
		

	}


	
}
