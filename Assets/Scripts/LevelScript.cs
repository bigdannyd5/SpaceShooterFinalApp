using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

// just loads the appropriate levels on the level select screen
public class LevelScript : MonoBehaviour
{

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;


    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;



    // Use this for initialization
    void Start()
    {



        level1Button = level1.GetComponent<Button>();
        level2Button = level2.GetComponent<Button>();
        level3Button = level3.GetComponent<Button>();
        level4Button = level4.GetComponent<Button>();



        //


        level1Button.onClick.AddListener(LoadLevel1);
        level2Button.onClick.AddListener(LoadLevel2);
        level3Button.onClick.AddListener(LoadLevel3);
        level4Button.onClick.AddListener(LoadLevel4);

     
    }

 
    private void LoadLevel1()
    {
        SceneManager.LoadScene(3);

    }



    private void LoadLevel2()
    {
       
        SceneManager.LoadScene(5);
        
    }


    private void LoadLevel3()
    {
        SceneManager.LoadScene(6);

    }

    private void LoadLevel4()
    {
        SceneManager.LoadScene(7);

    }


}
