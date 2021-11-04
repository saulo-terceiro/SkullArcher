using UnityEngine;
using System.Collections;

public class Bananas : MonoBehaviour {
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame


	void OnCollisionEnter2D(Collision2D other){
		GameObject collisioner = other.gameObject;
		if (collisioner.CompareTag ("enemy")) {
			float x = -7.63f;
			float y = transform.position.y;
			Vector2 vector = new Vector2(x,y);
			rb.position = vector;

			collisioner.SendMessage ("damageBanana");
		}	
	}
}
