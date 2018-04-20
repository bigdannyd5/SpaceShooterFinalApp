using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public GameObject Back;
    public Button backButton;

    // Use this for initialization
    void Start()
    {
        backButton = Back.GetComponent<Button>();
        backButton.onClick.AddListener(BackToMain);

    }

    // loads the main menu scene
    private void BackToMain()
    {
        SceneManager.LoadScene(1);
    }

}
