using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this script allows you to go back into the login screen and load some other user's info
public class ChangeUserScript : MonoBehaviour
{
    public GameObject changeUser;

    public Button changeButton;

    // Use this for initialization
    void Start ()
    {
        changeButton = changeUser.GetComponent<Button>();

        changeButton.onClick.AddListener(BackToLogin);

    }

    // this will allow you to log out and change users
    private void BackToLogin()
    {
        SceneManager.LoadScene(0);
    }

}
