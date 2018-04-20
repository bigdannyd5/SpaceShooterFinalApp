using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class LoginScript : MonoBehaviour
{
    
    public static List<User> savedUsers;

    // The game objects used.
    public GameObject createUser;
    public GameObject LoginUser;
    public GameObject UserC;
    public GameObject playerNotFound;
    public GameObject CouldNot;
    public GameObject createdUser;

    // The buttons with their functionalities.
    public Button LoginUserButton;
    public Button createUserButton;

    public string profileString = string.Empty;

    private List<User> highScores = new List<User>();


    public Text enterUsername;

    FileStream file;

    private static string filename;

    // Flag.
    int pf;

    // Use this for initialization.
    void Start()
    {
        filename = Application.persistentDataPath + "/savedGames3.gd";
        // If persistent data file for saved games does not exist, make one.
        if (!(File.Exists(filename)))
            file = File.Create(filename);

        // A list containing all of the users.
        savedUsers = new List<User>();

        LoginUserButton.onClick.AddListener(LoadMainMenu);
    }

    // Is reponsible for logging in and checking for if the user does not exist.
    public void Login()
    {

        Load();
        
        User player = LoginScript.savedUsers.Find(user => user.getName() == enterUsername.text);

        if (player == null)
        {
            playerNotFound.SetActive(true);
            Invoke("DoOtherStuff", 3.5f);
          
            pf = 1;
        }

        // If player was found, set user.
        if (player != null)
        {
            pf = 0;
            FindObjectOfType<CurrentUser>().GetComponent<CurrentUser>().currentUser = player;

        }


    }

    // Responsible for loading info.
    public static void Load()
    {
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            // If there is no file, create an empty list of users.
            if (file.Length == 0)
                LoginScript.savedUsers = new List<User>();
            else
                LoginScript.savedUsers = (List<User>)bf.Deserialize(file);
            file.Close();
        }
    }
    
    // This function does not verify if a user is duplicated.
    // That must be done by the caller if duplicates are to be avoided.
    public static void Save(User user)
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames3.gd"))
        {
            Load();
            LoginScript.savedUsers.Add(user);
            BinaryFormatter bf = new BinaryFormatter();
            //Application.persistentDataPath is a string, so if you wanted you can put
			//that into debug.log if you want to know where save games are located.
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames3.gd", FileMode.Open);
            bf.Serialize(file, LoginScript.savedUsers);
            file.Close();
        }
    }

    public void RegisterUser()
    {
        Load();

        // Attempt to find a user with the same name from currently regisetered users.
        User player = LoginScript.savedUsers.Find(user => user.getName() == enterUsername.text);

        // If a valid username is entered, and a user with the same name is not found, create new user.
        if (enterUsername.text != String.Empty && player == null)
        {
            User user = new User(enterUsername.text);
            Save(user);
            createdUser.SetActive(true);
            Invoke("CreatedStuff", 3.5f);
            
        }
        // If a user with same username is entered, indicate.
        else
        {
            CouldNot.SetActive(true);
            Invoke("DoStuff", 3.5f);
          
        }

    }

    // Used for the invoke method call.
    public void CreatedStuff()
    {
        createdUser.SetActive(false);
    }
    // Used for the invoke method call.
    public void DoOtherStuff()
    {
        playerNotFound.SetActive(false);
    }
    // Used for the invoke method call.
    public void DoStuff()
    {
        CouldNot.SetActive(false);
    }

    // Simply loads up the scene that holds the MainMenu.
    private void LoadMainMenu()
    {
        if((enterUsername.text != String.Empty) && pf!=1)
            SceneManager.LoadScene(1);  
    }

}
