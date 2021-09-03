using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public bool Pause = false;
    public bool sound = true;
    public GameObject PausePanel;
    public Color[] soundColor;
    public Text soundBtn;

    public void Start()
    {
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
    }
    public void PauseM()
    {
        if(!Pause)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0.0000001f;
            Pause = true;
            return;
        }
        if (Pause)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            Pause = false;
            return;
        }
    }

    public void Retart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenuBtn()
    {
        Application.LoadLevel("MainMenu");
        
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
