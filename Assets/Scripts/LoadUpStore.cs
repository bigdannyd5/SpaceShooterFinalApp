using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadUpStore : MonoBehaviour
{
    public GameObject upgradeStore;

    public Button upgradeStoreButton;

    // Use this for initialization
    void Start()
    {
        upgradeStoreButton = upgradeStore.GetComponent<Button>();

        upgradeStoreButton.onClick.AddListener(ToUpgrade);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ToUpgrade()
    {
        SceneManager.LoadScene(2);
    }

}
