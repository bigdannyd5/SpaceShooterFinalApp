using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour
{
    //public GameObject 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayM()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopM()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }

    public void PauseM()
    {
        gameObject.GetComponent<AudioSource>().Pause();
    }

    public void UnPauseM()
    {
        gameObject.GetComponent<AudioSource>().UnPause();
    }
}
