using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public List<int> listIndexEnemies = new List<int>();
    public int danoExplosion;
    public CircleCollider2D collider;
    public GameObject UpperObject;
    public AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        this.danoExplosion = PlayerPrefs.GetInt("ExplosiveArrow", 0);
        Controller.Instance.playSound(explosion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisioner = other.gameObject;
        if (collisioner.CompareTag("enemy"))
        {
            int enemyIndex = collisioner.GetComponent<Enemy>().getIndex();
            if (!this.listIndexEnemies.Contains(enemyIndex))
            {
                collisioner.SendMessage("damage", this.danoExplosion);
                this.listIndexEnemies.Add(enemyIndex);
            }
        }
    }

    public void EnableCollision()
    {
        this.collider.enabled = true;
    }

    public void DesableCollision()
    {
        this.collider.enabled = false;
    }

    public void Die()
    {
        Destroy(this.UpperObject);
    }
}
