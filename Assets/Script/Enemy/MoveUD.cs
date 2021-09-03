using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUD : MonoBehaviour {

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
        if (transform.position.y < -3)
        {
            moveDir = new Vector2(-1, 1);
        }
        if (transform.position.y > 3)
        {
            moveDir = new Vector2(-1, -1);
        }
    }

    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void OnDestroy()
    {
        if (!isQuit && Time.timeScale == 1 && !Ship.isOver)
        {
            GameObject p = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            p.GetComponent<ParticleSystem>().Play();
            Destroy(p, p.GetComponent<ParticleSystem>().duration);
        }
    }
}
