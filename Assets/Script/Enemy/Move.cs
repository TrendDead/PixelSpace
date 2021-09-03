using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float Speed;
    public Vector2 moveDir;
    public GameObject fx;
    private bool isQuit=false;
    public ShipControll Ship;

    void Start()
    {
        Ship = GameObject.Find("shipPlayer").GetComponent<ShipControll>();
    }
	void Update () {
        transform.Translate(moveDir*Time.deltaTime*Speed);
        if (transform.position.x <= -10 || transform.position.y < -7 || transform.position.y > 7 || transform.position.x > 13)
        {
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void OnDestroy()
    {
        if (!isQuit && Time.timeScale==1 && !Ship.isOver)
        {
            GameObject p = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            p.GetComponent<ParticleSystem>().Play();
            Destroy(p, p.GetComponent<ParticleSystem>().duration);
        }
    }
}
