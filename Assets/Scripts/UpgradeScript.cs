using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

// Responsible for the behavior of the Upgrade Shop, where the player can enter and spend currency
// gained from playing the game.
public class UpgradeScript : MonoBehaviour
{
  
    // These are the public gameobjects we created to have more control over the behaviors of particular 
    // things, such as specific buttons, texts, and their interaction based on the player's input.
    // So, one example is the SetActive and Invoke() methods that allow for easy ways to change the 
    // color of the button or display a button for only a avertain period of time. These things are very
    // easily implemented when using GameObjects.
    public GameObject fireRate;
    public GameObject moveSpeed;
    public GameObject hardenedBullets;
    public GameObject maxLives;
    public GameObject sold;
    public GameObject sold2;
    public GameObject sold3;
    public GameObject sold4;
    public GameObject og;
    public GameObject ogC;
    public GameObject ogH;
    public GameObject ogS;
    public GameObject UserC;

    User currentUser;

    // These are the buttons that I have declared to be able to control their button functionalities.
    public Button fireRateButton;
    public Button moveSpeedButton;
    public Button hardenedBulletsButton;
    public Button maxLivesButton;
    

    // The text that will display the username, cost of upgrades, as
    // well as the player's currency at their disposal.
    public Text printThis;
    public Text UsernameText;
    public Text fireCost;
    public Text moveCost;
    public Text hardCost;
    public Text maxLivesCost;

    // The costs of the upgrades, hard coded in. And a variable for to store player's currency.
    int playerCurrency;
    int move = 5000;
    int hardB = 10000;
    int fire = 200;
    int lives = 20000;

    // These are my flags that I will be using to keep track of the upgrades I will be buying.
    int moveSel = 0;
    int hardSel = 0;
    int fireSel = 0;
    int livesSel = 0;

    public Color newColor;
   

    // Use this for initialization.
    void Start()
    {

        // We get this reference out of the way and assign it, so we dont need to use this entire thing laters.
        currentUser = FindObjectOfType<CurrentUser>().GetComponent<CurrentUser>().currentUser;

        // This loads up the users.
        UserC.GetComponent<CurrentUser>().Load();

        UsernameText.text = "Username: " + (currentUser.getName());

        // This gets the currency of the player from the instance of the User class
        // that stores the player's info.
        playerCurrency = currentUser.getCurrency();


        // The following if statements are initially hecking for if the user has bought the upgrade
        // or if the user does not have enough currency. This will visibly change the color of the 
        // button to indicate the inavailability of the option to buy it.
        if(playerCurrency < fire || currentUser.getRateOfFire() == 1)
        {
           
            og.SetActive(false);
            sold.SetActive(true);
            // color change
            ColorBlock cb = fireRateButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            
            fireRateButton.colors = cb;
        }

        if (playerCurrency < move || currentUser.getSpeed() == 1)
        {
            // Color change.
            ColorBlock cb = moveSpeedButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            moveSpeedButton.colors = cb;

            ogS.SetActive(false);
            sold2.SetActive(true);

        }

        if (playerCurrency < hardB || currentUser.getHard() == 1)
        {
            // Color change.
            ColorBlock cb = hardenedBulletsButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            hardenedBulletsButton.colors = cb;

            ogH.SetActive(false);
            sold3.SetActive(true);
        }

        if (playerCurrency < lives)
        {
            // Color change.
            ColorBlock cb = maxLivesButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            maxLivesButton.colors = cb;

            ogC.SetActive(false);
            sold4.SetActive(true);
        }


        // Displays the player's currency.
        printThis.text = "Player Currency: " + playerCurrency.ToString();

        // Buttons are assigned functionality.
        fireRateButton = fireRate.GetComponent<Button>();
        moveSpeedButton = moveSpeed.GetComponent<Button>();
        hardenedBulletsButton = hardenedBullets.GetComponent<Button>();
        maxLivesButton = maxLives.GetComponent<Button>();

        // The costs are being displayed.
        fireCost.text = "Cost: " + fire.ToString();
        moveCost.text = "Cost: " + move.ToString();
        hardCost.text = "Cost: " + hardB.ToString();
        maxLivesCost.text = "Cost: " + lives.ToString();
        
        

        // Here we are making the calls to the other methods that are responsible for checking if the player can afford them and 
        // updates the currency accordingly.
        fireRateButton.onClick.AddListener(SubtractFire);
        moveSpeedButton.onClick.AddListener(SubtractMove);
        hardenedBulletsButton.onClick.AddListener(SubtractHard);
        maxLivesButton.onClick.AddListener(SubtractMaxLives);


    }

    // This method is responsible for checking if the current player has enough currency to buy
    // this upgrade.If the player does, the currency will be updated with the correct amount and
    // that result will be stored into our file that stores all of the player's information. In
    // addition, the method will keep track of a flag that is sent into our User class that stores
    // the player's info.
    private void SubtractMove()
    {

        // The initial check.
        if ((playerCurrency < move) || currentUser.getSpeed() == 1)
        {
            // Color change.
            ColorBlock cb = moveSpeedButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            moveSpeedButton.colors = cb;
        }

        else
        {
            // Color change.
            ColorBlock cb = moveSpeedButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            moveSpeedButton.colors = cb;

            // Flag.
            moveSel = 1;

            // Storing the info.
            currentUser.setMoveMultiplier(2.0f);

            playerCurrency -= move;

            currentUser.setCurrency(playerCurrency);

            printThis.text = "Player Currency: " + currentUser.getCurrency().ToString();

            // Storing the flag .
            currentUser.setSpeed(moveSel);

            // Saving the currentUser into the file and adding it to the list of users.
            UserC.GetComponent<CurrentUser>().Save(currentUser);

            
        }
    }


    // This method is responsible for checking if the current player has enough currency to buy
    // this upgrade.If the player does, the currency will be updated with the correct amount and
    // that result will be stored into our file that stores all of the player's information.In
    // addition, the method will keep track of a flag that is sent into our User class that stores
    // the player's info.
    private void  SubtractHard()
    {
        if (playerCurrency < hardB || currentUser.getHard() == 1)
        {
            // Color change.
            ColorBlock cb = hardenedBulletsButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            hardenedBulletsButton.colors = cb;
        }
        else
        {
            // Color change.
            ColorBlock cb = hardenedBulletsButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            hardenedBulletsButton.colors = cb;

            // Flag.
            hardSel = 1;

            playerCurrency -= hardB;
            
            // Setting the updated currency.
            currentUser.setCurrency(playerCurrency);

            printThis.text = "Player Currency: " + currentUser.getCurrency().ToString();

            // Setting the flag.
            currentUser.setHard(hardSel);

            // Saving it.
            UserC.GetComponent<CurrentUser>().Save(currentUser);
        }
    }


    // This method is responsible for checking if the current player has enough currency to buy
    // this upgrade.If the player does, the currency will be updated with the correct amount and
    // that result will be stored into our file that stores all of the player's information.In
    // addition, the method will keep track of a flag that is sent into our User class that stores
    // the player's info.
    private void SubtractFire()
    {
        if (playerCurrency < fire || currentUser.getRateOfFire() == 1 )
        {

            // Color change.
            ColorBlock cb = fireRateButton.colors;
            cb.normalColor = newColor;
            fireRateButton.colors = cb;
        }
        else
        {
            ColorBlock cb = fireRateButton.colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            fireRateButton.colors = cb;

            currentUser.setFireMultiplier(.5f);

            fireSel = 1;
            
            playerCurrency -= fire;

            currentUser.setCurrency(playerCurrency);

            currentUser.setRateOfFire(fireSel);

            printThis.text = "Player Currency: " + currentUser.getCurrency().ToString();

            UserC.GetComponent<CurrentUser>().Save(currentUser);

        }

    }

    // This method is responsible for checking if the current player has enough currency to buy
    // this upgrade.If the player does, the currency will be updated with the correct amount and
    // that result will be stored into our file that stores all of the player's information.In
    // addition, the method will keep track of a flag that is sent into our User class that stores
    // the player's info.
    private void SubtractMaxLives()
    {
        if (playerCurrency < lives)
        {
           
            // Wont allow you to buy this item, yet.
        }
       
        else
        {

       
            playerCurrency -= lives;

            // Sets updated currency.
            currentUser.setCurrency(playerCurrency);

            printThis.text = "Player Currency: " + currentUser.getCurrency().ToString();

            int max = currentUser.getMaxHealth();

            // Adds one to the last value stored.
            currentUser.setMaxHealth(1+max);

            // Save the info.
            UserC.GetComponent<CurrentUser>().Save(currentUser);

        }
    }

}
