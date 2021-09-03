using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplEnemy : MonoBehaviour {

    public int Life_points;
    public GameObject bullet;
    public float shootDelay;
    public Transform shootPoint;
    public ShipControll Ship;
    public bool isDead = false;
    public GameObject coin;

    public SoundMamager sm;

    void Update()
    {
        if (Life_points == 0 && !isDead)
        {
            Boom();
        }
        if (transform.position.x <= -10 || transform.position.y < -7 || transform.position.y > 7 || transform.position.x > 13)
        {
            Destroy(gameObject);
        }
    }

    void Boom()
    {
        isDead = true;
        Destroy(gameObject);
        sm.PlaySound(0);
        SpawnCoin();
        Ship.AddScore(120);
    }

    void SpawnCoin()
    {
        GameObject c = Instantiate(coin, transform.position, Quaternion.identity) as GameObject;

    }

    void Start()
    {
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControll>();
        InvokeRepeating("Shoot", 2, shootDelay);
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, shootPoint.position, Quaternion.identity) as GameObject;
        Destroy(b, 6);
    }

    public void Damage(int dmg)
    {
        Life_points -= dmg;
        if (Life_points < 0)
            Life_points = 0;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("shipB"))
        {
            Damage(Ship.bulletDamage);
            Destroy(coll.gameObject);
            sm.PlaySound(1);
            
        }
        if (coll.gameObject.CompareTag("enemyB"))
        {
            Damage(1);
            Destroy(coll.gameObject);
            sm.PlaySound(1);
        }
    }
}
