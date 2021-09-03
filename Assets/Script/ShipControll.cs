using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControll : MonoBehaviour {

    public int Life_points;
    public Image[] lifePoints;
    public Color[] lifeColors;
    public float Speed;
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;
    public GameObject bullet;
    public GameObject rocket;
    public int rockerCount;
    public Text rocketCountText;
    public float shootDelay;
    public Transform[] shootPoints;
    public bool isFire;
    public bool isReadyToShoot;
    public int bulletDamage;
    public SoundMamager sm;
    public int coinsCount;
    public Text coinsCountT;
    public int scoreCount;
    public Text scoreCountT;
    public Sprite[] ships;
    public bool isOver;
    public GameObject gameOverPanel;
    public ParticleSystem DeadFX;
    public SimpleADS Ad;
    public int DeadCount=0;

    public GameObject[] lifePointsShip;

    void Start()
    {
        DeadCount=PlayerPrefs.GetInt("DC", 0);
        int ShipNum = MainMeny.shipNum;
        if (ShipNum == 0)
        {
            Ship1();
        }
       
            if (ShipNum == 1)
            {
                Ship2();
            }
            
                if (ShipNum == 2)
                {
                    Ship3();
                }
            
        
        GetComponent<SpriteRenderer>().sprite = ships[ShipNum];
        isReadyToShoot = true;
        isFire = false;
        coinsCount = PlayerPrefs.GetInt("coins", 0);
        sm = GameObject.Find("PlayZone").GetComponent<SoundMamager>();
        
    }

   void Ship1()
    {
        rockerCount = 3;
        shootDelay = 0.6f;
        Speed = 2f;
        Life_points = 10;
        lifePointsShip[0].SetActive(false);
        lifePointsShip[1].SetActive(false);
        lifePointsShip[2].SetActive(false);
        lifePointsShip[3].SetActive(false);
        lifePointsShip[4].SetActive(false);
    }
    void Ship2()
    {
        rockerCount = 6;
        shootDelay = 0.45f;
        Speed = 3f;
       Life_points = 12;
        lifePointsShip[0].SetActive(true);
        lifePointsShip[1].SetActive(true);
        lifePointsShip[2].SetActive(false);
        lifePointsShip[3].SetActive(false);
        lifePointsShip[4].SetActive(false);
    }
    void Ship3()
    {
        rockerCount = 15;
        shootDelay = 0.3f;
        Speed = 4f;
        Life_points = 15;
        lifePointsShip[0].SetActive(true);
        lifePointsShip[1].SetActive(true);
        lifePointsShip[2].SetActive(true);
        lifePointsShip[3].SetActive(true);
        lifePointsShip[4].SetActive(true);
    }

    void Save()
    {
        PlayerPrefs.SetInt("coins", coinsCount);
        PlayerPrefs.SetInt("NewScore", scoreCount);
        if(PlayerPrefs.HasKey("HS"))
        {
            PlayerPrefs.SetInt("HS", scoreCount);
        }
        else
        {
            int s = PlayerPrefs.GetInt("HS");
            if (MainMeny.hs < scoreCount)
            {
                PlayerPrefs.SetInt("HS", scoreCount);
            }
        }
        PlayerPrefs.SetInt("DC", DeadCount);
    }

    void OnApplicationQuit()
    {
        Save();
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime * Speed);
    }

    void Update()
    {
        scoreCountT.text = scoreCount.ToString();
        coinsCountT.text = coinsCount.ToString();
        rocketCountText.text = rockerCount.ToString();
        Vector2 curPos = transform.localPosition;
        curPos.y = Mathf.Clamp(transform.localPosition.y,minY,maxY);
        curPos.x = Mathf.Clamp(transform.localPosition.x, minX, maxX);
        transform.localPosition = curPos;

        if(isFire && isReadyToShoot)
        {
            Shoot();
        }

        if (Life_points <= 0&&!isOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {

        DeadCount++;
        isOver = true;
        sm.PlaySound(0);
        DeadFX.Play();
        Hide();
        Save();
        gameOverPanel.SetActive(true);
        if(DeadCount == 2)
        {
            Ad.ShowAd();
        }
    }

    void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ChangeLife()
    {
        for(int l = 0; l<lifePoints.Length; l++)
        {
            if (l < Life_points)
            {
                lifePoints[l].color = lifeColors[0];
            }
            else
            {
                lifePoints[l].color = lifeColors[1];
            }
        }
    }

    void Shoot()
    {

            foreach (Transform sp in shootPoints)
            {
                GameObject b = Instantiate(bullet, sp.position, Quaternion.identity) as GameObject;
            Destroy(b, 6);
                if(sp == shootPoints[shootPoints.Length - 1])
                {
                    StartCoroutine(ShootDelay());
                }
            }
        sm.PlaySound(3);
        AddScore(1);
    }

    public void RocketShoot()
    {
        if (rockerCount > 0) { 
        GameObject r = Instantiate(rocket, transform.position, Quaternion.identity) as GameObject;
        rockerCount--;
            sm.PlaySound(2);
            AddScore(10);
        }


    }

    IEnumerator ShootDelay()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(shootDelay);
        isReadyToShoot = true;
    }

    public void Fire(bool fire)
    {
        isFire = fire;
    }

    public void Damage(int dmg)
    {
        Life_points -= dmg;
        if (Life_points < 0)
            Life_points = 0;
        ChangeLife();
    }

    public void AddScore(int scoreToAdd)
    {
        scoreCount += scoreToAdd;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemyB"))
        {
            Damage(1);
            Destroy(coll.gameObject);
            sm.PlaySound(1);
        }
        if (coll.gameObject.CompareTag("coin"))
        {
            Destroy(coll.gameObject);
            coinsCount++;
            sm.PlaySound(4);
        }
        if (coll.gameObject.CompareTag("enemy"))
        {
            Destroy(coll.gameObject);
            Damage(2);
            sm.PlaySound(0);
        }
    }

}
