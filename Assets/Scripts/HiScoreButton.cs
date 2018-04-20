using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This just loads up the highScore scene once the user has presed that button.
public class HiScoreButton : MonoBehaviour
{
    public GameObject HiScore;

    public Button HiScoresButton;

    // Use this for initialization.
    void Start()
    {
        HiScoresButton = HiScore.GetComponent<Button>();

        HiScoresButton.onClick.AddListener(LoadHiScores);

    }


    private void LoadHiScores()
    {
        SceneManager.LoadScene(4);
    }

}
