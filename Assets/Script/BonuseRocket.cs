using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonuseRocket : MonoBehaviour {

    public ShipControll Ship;
    public SoundMamager sm;

    // Use this for initialization
    void Start()
    {
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControll>();
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ship"))
        {
            Destroy(gameObject);
            Ship.rockerCount += 3;
        }
        sm.PlaySound(4);
    }
}
