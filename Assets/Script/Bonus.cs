using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public GameObject[] bonuses;
    public int lifePoints = 4;
    public bool isDead = false;
    public SoundMamager sm;

    void Start()
    {
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
    }

    void Boom () {
        isDead = true;
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        GameObject b = Instantiate(bonus, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        sm.PlaySound(0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("shipB"))
        {
            lifePoints--;
            Destroy(coll.gameObject);
            sm.PlaySound(1);
        }
        if (coll.gameObject.CompareTag("shipR"))
        {
            lifePoints=0;
            Destroy(coll.gameObject);
            

        }
    }

    void Update()
    {
        if(lifePoints == 0 && !isDead)
        {
            Boom();
        }
    }

	}
