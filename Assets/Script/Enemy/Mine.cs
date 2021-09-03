using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {
    public GameObject Ship;
    public float Speed;
    public float distToActivee;
    public SoundMamager sm;
    // Use this for initialization
    void Start () {
        Ship = GameObject.Find("shipPlayer");
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position,Ship.transform.position) <= distToActivee && !Ship.GetComponent<ShipControll>().isOver)
        {
            transform.position = Vector2.MoveTowards(transform.position, Ship.transform.position, Time.deltaTime * Speed);
        }
        if(Vector2.Distance(transform.position, Ship.transform.position) <= 0.7f && !Ship.GetComponent<ShipControll>().isOver)
        {
            sm.PlaySound(0);
            Ship.GetComponent<ShipControll>().Damage(3);
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("shipB"))
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
            sm.PlaySound(0);
            Ship.GetComponent<ShipControll>().AddScore(100);
        }
        if (coll.gameObject.CompareTag("enemyB"))
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
            sm.PlaySound(0);
        }
    }
}
