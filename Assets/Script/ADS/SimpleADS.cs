﻿using UnityEngine;
using UnityEngine.Advertisements;

public class SimpleADS : MonoBehaviour
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
			if(PlayerPrefs.GetInt("noads")!=1){
            Advertisement.Show();
            PlayerPrefs.SetInt("DC", 0);
			}
        }
    }
}
