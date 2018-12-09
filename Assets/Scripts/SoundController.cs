using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundController : MonoBehaviour {

    public AudioMixer am;

    // Use this for initialization
    void Start () {
        am.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }
	
}
