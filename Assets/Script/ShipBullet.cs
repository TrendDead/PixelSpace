using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour {

    public SoundMamager sm;
    void Start()
    {
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemyB"))
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            sm.PlaySound(1);
        }
    }
}
