using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMamager : MonoBehaviour {
    public GameObject sfx;
    public AudioClip[] audioClips;
	
   public void PlaySound(int soundNum)
    {
        GameObject s = Instantiate(sfx, Vector2.zero, Quaternion.identity) as GameObject;
        AudioSource AS = s.GetComponent<AudioSource>();

        AS.clip = audioClips[soundNum];
        AS.Play();
        Destroy(s, audioClips[soundNum].length);
    }
}
