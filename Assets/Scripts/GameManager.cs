using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public GameObject playButton;
	public GameObject playerShip;
	// Reference to our enemy spawner.
	public GameObject enemySpawner;
	// Reference to game over image.
	public GameObject GameOverGO; 
	// Reference to the score text UI game object.
	public GameObject scoreUITextGO; 
	// Reference to ther time counter game object.
	public GameObject TimeCounterGO; 
	// Reference to the GameTitleGo.
	public GameObject GameTitleGo; 
	// Reference to the shoot button game object.
	public GameObject ShootButton;
	// Reference to the left arrow button game object.
	public GameObject LeftButton; 
	// Reference to the right arrow button game object.
	public GameObject RightButton;
	// Reference to our meteor spawner.
	public GameObject meteorSpawner;
	// Reference to the quit button game object.
	public GameObject quitButton; 
	// Reference to the store button game object.
	public GameObject storeButton; 
	// Reference to the next level button.
	public GameObject nextLevelButton; 
	// Reference to the BossSpawner.
	public GameObject BossSpawner;
	// Reference to the retry button.
	public GameObject retryButton; 
    public GameObject introSong;
    public GameObject gameSong;
    public GameObject UserC;
    public GameObject PauseButton;
    public GameObject pauseMenuUI;
    public User currentUser;
	public int level;
    public GameObject buttonMain;
    public GameObject creditButton;
    public bool GameIsPaused;

	public enum GameManagerState
	{
		Opening,
		Gameplay,
		GameOver,
		EndLevel,
	}

	GameManagerState GMState;

	// Use this for initialization.
	void Start () 
	{
        currentUser = FindObjectOfType<CurrentUser>().GetComponent<CurrentUser>().currentUser;
		GMState = GameManagerState.Opening;
	}
	
	// Update game manager state.
	void UpdateGameManagerState()
	{
		switch (GMState) 
		{
		case GameManagerState.Opening:

			    // Display the game title.
			    GameTitleGo.SetActive (true);

                creditButton.SetActive(true);

                buttonMain.SetActive(false);
                // Hide game over.
                GameOverGO.SetActive(false);

			    // Display the game title.
			    GameTitleGo.SetActive(true);

			    // Set play button visible (active).
			    playButton.SetActive(true);

				break;
		case GameManagerState.Gameplay:

			// Reset the score.
			scoreUITextGO.GetComponent<GameScore> ().Score = 0;

            buttonMain.SetActive(false);

			// Hide next level button.
			nextLevelButton.SetActive(false);

            creditButton.SetActive(false);

			// Hide quit button.
			quitButton.SetActive(false);

			// Hide store button.
			storeButton.SetActive(false);

			// Hide retry button.
			retryButton.SetActive(false);

			// Hide play button on game play state.
			playButton.SetActive (false);

			// Hide the game title.
			GameTitleGo.SetActive (false);

			// Hide game over.
			GameOverGO.SetActive(false);

			// Display the shoot button.
			ShootButton.SetActive (true);

			// Display the left arrow button.
			LeftButton.SetActive (true);

			// Display the right arrow button.
			RightButton.SetActive (true);

            // Display the pause button.
            PauseButton.SetActive(true);

			// Set the player visible (active) and init the player lives.
			playerShip.GetComponent<PlayerControl> ().Init ();

			// Start enemy spawner.
			enemySpawner.GetComponent<EnemySpawner> ().ScheduleEnemySpawner ();

			// Start the time counter.
			TimeCounterGO.GetComponent<TimeCounter> ().StartTimeCounter ();

			// Start meteor spawner.
			meteorSpawner.GetComponent<MeteorSpawner> ().ScheduleMeteorSpawner ();

            // Stop intro muysic, start game music.
            introSong.GetComponent<AudioSource>().Stop();
            gameSong.GetComponent<PlayMusic>().PlayM();

			break;
		case GameManagerState.GameOver:

                creditButton.SetActive(false);

                UserC.GetComponent<CurrentUser>().Load();

                buttonMain.SetActive(true);

                int playerCurrency = currentUser.getCurrency();

                int updatedCurrency = scoreUITextGO.GetComponent<GameScore>().Score + playerCurrency;

                currentUser.setCurrency(updatedCurrency);
                UserC.GetComponent<CurrentUser>().Save(currentUser);

                // If boss is active, and you lose, then set bossNumber to zero.
                TimeCounterGO.GetComponent<TimeCounter>().bossNumber = 0;

			    // Stop the time counter.
			    TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

			    // Stop enemy spawner.
			    enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();

			    // Stop meteor spawner.
			    meteorSpawner.GetComponent<MeteorSpawner>().UnsheduleEnemySpawner();

			    // Destroy boss if it is there.
			    BossSpawner.GetComponent<BossSpawner>().DestroyBoss();

			    // Hide the shoot button.
			    ShootButton.SetActive(false);

			    // Hide the left arrow button.
			    LeftButton.SetActive(false);

			    /// Hide the right arrow button.
			    RightButton.SetActive(false);

                // HidE the pause button.
                PauseButton.SetActive(false);

			    // Display retry button.
			    retryButton.SetActive(true);

			    // Display game over.
			    GameOverGO.SetActive(true);

                // Stop music.
                gameSong.GetComponent<PlayMusic>().StopM();
			    
			break;
		case GameManagerState.EndLevel:

                creditButton.SetActive(false);

                buttonMain.SetActive(false);

                print("At EndLevel ");
                UserC.GetComponent<CurrentUser>().Load();
                foreach (User user in UserC.GetComponent<CurrentUser>().savedUsers)
                {
                    print("User: " + user.getName());
                    print("Score: " + user.getHighScore());
                    print("Currency: " + user.getCurrency());
                }

                int pC= currentUser.getCurrency();

                int uC = scoreUITextGO.GetComponent<GameScore>().Score + pC;

                currentUser.setCurrency(uC);
                UserC.GetComponent<CurrentUser>().Save(currentUser);

                // Reseting the bossNumber, so it will respawn when we retry.
                TimeCounterGO.GetComponent<TimeCounter>().bossNumber = 0;

                // This will only update highscore table if the score just received is higher than the
                // the previously stored highscore.
                if (currentUser.getHighScore() < scoreUITextGO.GetComponent <GameScore> ().Score)
                {
                    currentUser.setHighScore(scoreUITextGO.GetComponent<GameScore>().Score);
                    UserC.GetComponent<CurrentUser> ().Save(currentUser);
                }

                // Stop music.
                gameSong.GetComponent<PlayMusic>().StopM();

                creditButton.SetActive(false);

                buttonMain.SetActive(false);
                // Stop the time counter.
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

			    // Stop enemy spawner.
			    enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner(); 

			    // Stop meteor spawner.
			    meteorSpawner.GetComponent<MeteorSpawner>().UnsheduleEnemySpawner(); 

			    // Display quit button.
			    quitButton.SetActive(true);

			    // Display store button.
			    storeButton.SetActive(true);

			    // Display next level button.
			    nextLevelButton.SetActive(true);

			    // Display retry button.
			    retryButton.SetActive(true);

			    // Hide shoot button.
			    ShootButton.SetActive(false);

			    // Hide left arrow button.
			    LeftButton.SetActive(false);

			    // Hide right arrow button.
			    RightButton.SetActive(false);

                // Hide pause button.
                PauseButton.SetActive(false);

			    // Hide player ship.
			    playerShip.SetActive(false);

			break;
		}
	}

	// Set game manager state.
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}

	// Out play button will call this function
	// when the user clicks the button.
	public void StartGamePlay()
	{
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
	}

	// Change game manager state to opening state.
	public void ChangeToOpeningState()
	{
		SetGameManagerState (GameManagerState.Opening);
	}

	// Load the next level.
	public void NextLevel()
	{
		if (level == 1) 
		{
			level++;
			Application.LoadLevel ("Level2");
		} 
		else if (level == 2) 
		{
			level++;
			Application.LoadLevel ("Level3");
		} 
		else if (level == 3) 
		{
			level++;
			Application.LoadLevel ("Level4");
		}
		else if (level == 4)
		{
			level++;
			Application.LoadLevel ("Ending");
		}
	}


    public void Pause()
    {
        gameSong.GetComponent<PlayMusic>().PauseM();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        gameSong.GetComponent<PlayMusic>().UnPauseM();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    public void Print()
    {
        print("I really hope we didnt break something");
    }

}
