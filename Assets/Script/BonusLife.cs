using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLife : MonoBehaviour {

    public ShipControll Ship;
    public SoundMamager sm;

    // Use this for initialization
    void Start () {
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControll>();
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ship"))
        {
            Destroy(gameObject);
            if (Ship.Life_points < 9)
            {
                Ship.Life_points += 2;
            }
            else
            {
                Ship.Life_points = 10;
            }
            sm.PlaySound(4);
            Ship.ChangeLife();
        }
        	
	}
}
