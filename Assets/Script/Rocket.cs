using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public SoundMamager sm;
    void Start()
    {
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemy"))
        {
            coll.gameObject.GetComponent<simplEnemy>().Damage(10);
            Destroy(gameObject);
            sm.PlaySound(0);

        }
        if (coll.gameObject.CompareTag("enemyB"))
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            sm.PlaySound(0);
        }
        if (coll.gameObject.CompareTag("mine"))
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            sm.PlaySound(0);
        }
    }
}
