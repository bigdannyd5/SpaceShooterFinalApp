using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class HighScores : MonoBehaviour
{

    public GameObject UserC;
    public GameObject scorePrefab;

    public Transform scoreParent;
    public int topRanks;

    private static List<User> highScores;

    int playerCurrency;

    // Use this for initialization
    void Start ()
    {
        // local list stroing the savd users
        highScores = new List<User>();

        UserC.GetComponent<CurrentUser>().Load();

        // here we are calling a Sort() method we designed using the IEComparable in out User Class
        UserC.GetComponent<CurrentUser>().savedUsers.Sort();

        // local variable for storing how many users we have
        int count = UserC.GetComponent<CurrentUser>().savedUsers.Count;


        // gets the highScore from the users
        UserC.GetComponent<CurrentUser>().currentUser.getHighScore();
              
        // if there are less highscores than 10, then we will only print how many
        // high scores there are.
        if (count < topRanks)
            topRanks = count;

        // iterate through the 10 highscores and create a prefab that already has slots for rank, 
        // username, nad highscore. this will go into the score parent and duplicate the prefab with unique 
        // usernames, ranks, and highscores, based on the highscores list that was created.
        for (int i = 0; i < topRanks; i++)
        {
            GameObject tempObject = Instantiate(scorePrefab);

            print("User name: " + UserC.GetComponent<CurrentUser>().savedUsers[i].getName());

            if (UserC.GetComponent<CurrentUser>().savedUsers[i].getName() == null)
                continue;
            User tempScore = UserC.GetComponent<CurrentUser>().savedUsers[i];

            tempObject.GetComponent<HighScoreScrript>().SetScore(tempScore.getName(), tempScore.getHighScore().ToString(), "#" + (i + 1).ToString());
            tempObject.transform.SetParent(scoreParent);

            // this will assure that our text is scaled to 1 by 1 by 1, for better visibilty
            tempObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }

     

    }



       

}

   
     


  
