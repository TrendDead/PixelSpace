using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {
    public GameObject[] Enemys;
    public GameObject Bonus;
    public float minDelay;
    public float maxDelay;
    public float minY;
    public float maxY;
	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Repeat () {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        Vector2 pos = new Vector2(transform.position.x, Random.Range(minY, maxY));
        GameObject e = Instantiate(Enemys[Random.Range(0, Enemys.Length)], pos, Quaternion.identity) as GameObject;
        int r = Random.Range(0, 100);
        Vector2 Bonus_pos = new Vector2(transform.position.x, Random.Range(minY, maxY));
       
        if (r <= 10)
        {
            GameObject b = Instantiate(Bonus, Bonus_pos, Quaternion.identity) as GameObject;
        }
        Repeat();
    }
}
