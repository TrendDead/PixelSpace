using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMeny : MonoBehaviour {
    public static int hs;
    public int coins;
    public Image[] ships;
    public static int shipNum;
    public Image Selector;
    public Color[] SelectorColors;
    public Color[] shipColors;
    public GameObject priceInCoins;
    public GameObject PticeInDollars;
    public string[] PlayButtonTexts;
    public Text playBtnT;
    public Color[] playBtnColors;
    public Text coinsT;
    public Text HS_T;
    public bool[] shipUnlock;
    public bool sound = true;
    public Color[] soundColor;
    public Text soundBtn;
    public int inMenuCount;
    public GameObject WatchAdBtn;
	public GameObject ManyPay;
	public IapManeger buy;
	public GameObject RemoveADSBtn;



    // Use this for initialization
    void Start () {
		if (PlayerPrefs.GetInt ("noads") == 1) {
			RemoveADSBtn.SetActive (false);
		} 
		buy = GetComponent<IapManeger> ();
        inMenuCount = PlayerPrefs.GetInt("IM", 0);
		if (inMenuCount == 2) {
			WatchAdBtn.SetActive (true);
			PlayerPrefs.SetInt ("IM", 0);
		} else {
			inMenuCount++;
			PlayerPrefs.SetInt ("IM", inMenuCount);
		}
        Time.timeScale = 1f;
        if (AudioListener.volume == 0)
        {
            sound = false;
            soundBtn.color = soundColor[1];
        }
        else
        {
            sound = true;
            soundBtn.color = soundColor[0];
        }
        hs = PlayerPrefs.GetInt("HS", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
       if(PlayerPrefs.GetInt("ship")==1)
        {
            shipUnlock[1] = true;
        }
        else
        {
            shipUnlock[1] = false;
        }
		if(PlayerPrefs.GetInt("ship2")==1)
		{
			shipUnlock[2] = true;
		}
		else
		{
			shipUnlock[2] = false;
		}
        ChangeShip(1);

	}
	
	// Update is called once per frame
	void Update () {
        coinsT.text = coins.ToString();
        HS_T.text ="HIGHSCORE: " + hs.ToString();
	}

    public void ChangeShip(int num)
    {
        switch(num)
        {
		case 1:
			Selector.transform.position = ships [0].transform.position;
			ships [0].color = shipColors [0];
			ships [1].color = shipColors [1];
			ships [2].color = shipColors [1];
			shipNum = 0;
			playBtnT.text = PlayButtonTexts [0];
			playBtnT.color = playBtnColors [0];
			priceInCoins.SetActive (false);
			Selector.GetComponent<Image> ().color = SelectorColors [0];
			ManyPay.SetActive (false);
			
                break;
            case 2:
                Selector.transform.position = ships[1].transform.position;
                ships[0].color = shipColors[1];
                ships[1].color = shipColors[0];
                ships[2].color = shipColors[1];
                shipNum = 1;
                if (shipUnlock[1] == false)
                {
                    Selector.GetComponent<Image>().color = SelectorColors[1];
                    priceInCoins.SetActive(true);
                    playBtnT.text = PlayButtonTexts[1];
                    playBtnT.color = playBtnColors[1];
                }
                else
                {
                    Selector.GetComponent<Image>().color = SelectorColors[0];
                    priceInCoins.SetActive(false);
                    playBtnT.text = PlayButtonTexts[0];
                    playBtnT.color = playBtnColors[0];
                }
			ManyPay.SetActive (false);
                break;
		case 3:
			Selector.transform.position = ships [2].transform.position;
			ships [0].color = shipColors [1];
			ships [1].color = shipColors [1];
			ships [2].color = shipColors [0];
			shipNum = 2;
			priceInCoins.SetActive (false);
			if (shipUnlock [2] == false) {
				playBtnT.text = PlayButtonTexts [1];
				playBtnT.color = playBtnColors [1];
				ManyPay.SetActive (true);

			} else {
				playBtnT.text = PlayButtonTexts [0];
				playBtnT.color = playBtnColors [0];
				ManyPay.SetActive (false);
			}
                break;
        }
    }

    public void PlayBtn()
    {
        if(shipNum==0)
        {
            Application.LoadLevel("Play");
        }
        if(shipNum==1)
        {
            if(shipUnlock[1]==false)
            {
                if(coins>=200)
                {
                    shipUnlock[1] = true;
                    PlayerPrefs.SetInt("ship", 1);
                    ChangeShip(2);
                }
            }
            else
            {
                PlayerPrefs.SetInt("IM",inMenuCount);
                Application.LoadLevel("Play");
            }
        }
		if (shipNum == 2) {
			if (shipUnlock [2] == false) {
				buy.BuyShip ();
			} else {
				PlayerPrefs.SetInt("IM",inMenuCount);
				Application.LoadLevel("Play");
			}

		}

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Sound()
    {
        if (sound)
        {
            AudioListener.volume = 0;
            soundBtn.color = soundColor[1];
            sound = false;
            return;
        }
        if (!sound)
        {
            AudioListener.volume = 1;
            soundBtn.color = soundColor[0];
            sound = true;
            return;
        }
    }
}
