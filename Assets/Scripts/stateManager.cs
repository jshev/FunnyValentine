using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stateManager : MonoBehaviour {

	public enum GameState { TitleState, InstructionsState, TutorialState, Lvl1State, Lvl2State, Lvl3State, Lvl4State };
	public GameState currentGameState;

	// set public variables in Inspector
	public GameObject Title;
	public GameObject Instruct;
	public GameObject Tutorial;
	public GameObject Lvl1;
	public GameObject Lvl2;
	public GameObject Lvl3;
	public GameObject Lvl4;

	public InputField pName;

	public string PlayerName;

	// set private variables in script
	Text flavorTxt;
	Text gameOverTxt;
	Text lvlTxt;
	SpriteRenderer mistake1;
	SpriteRenderer mistake2;
	SpriteRenderer mistake3;
	bool gameOver;
	string myCongrats;
	GameState nextState;
	GameObject nextPrefab;
	int once;


	// Use this for initialization
	void Start () {
		// set these varaibles by finding gameobjects by name
		flavorTxt = GameObject.Find("FlavorText").GetComponent<Text>();
		gameOverTxt = GameObject.Find("GOText").GetComponent<Text>();
		lvlTxt = GameObject.Find("LevelText").GetComponent<Text>();
		mistake1 = GameObject.Find ("Broken1").GetComponent<SpriteRenderer>();
		mistake2 = GameObject.Find ("Broken2").GetComponent<SpriteRenderer>();
		mistake3 = GameObject.Find ("Broken3").GetComponent<SpriteRenderer>();

		// load Title state on play
		currentGameState = GameState.TitleState;
		ShowScreen (Title);

		// set gameOver as false on start
		gameOver = false;

		// set mistakes PlayerPrefs to 0 on start
		PlayerPrefs.SetInt ("Mistakes", 0);
	}

	// Update is called once per frame
	void Update () {

		switch (PlayerPrefs.GetInt ("Mistakes")) {
		case 3:
			// third broken heart becomes opaque
			mistake3.color = new Color (1f, 1f, 1f, 1f);
			// triggers Game Over
			if (!gameOver) {
				// disables flavor text in case player tries to complete level before game restarts
				flavorTxt.enabled = false;
				// update game over text with disappointed message
				gameOverTxt.text = PlayerName + "... I thought you liked me...";
				// scene reloads on a delay
				Invoke ("reloadGame", 2.0f);
				gameOver = !gameOver;
			}
			break;
		case 2:
			// second broken heart becomes opaque
			mistake2.color = new Color (1f, 1f, 1f, 1f);
			break;
		case 1:
			// first broken heart becomes opaque
			mistake1.color = new Color (1f, 1f, 1f, 1f);
			break;
		default:
			// default is 0
			// all mistakes are partially transparent
			mistake1.color = new Color (1f, 1f, 1f, 0.25f);
			mistake2.color = new Color (1f, 1f, 1f, 0.25f);
			mistake3.color = new Color (1f, 1f, 1f, 0.25f);
			break;
		}

		switch (currentGameState)
		{
		case GameState.TitleState:
			// set once to 0
			once = 0;
			// no girls in this level
			PlayerPrefs.SetInt ("Girls", 0);
			// not a level so do not update level indiction text
			lvlTxt.text = " ";
			// mistake indictator is completely transparent during this state
			mistake1.color = new Color (1f, 1f, 1f, 0f);
			mistake2.color = new Color (1f, 1f, 1f, 0f);
			mistake3.color = new Color (1f, 1f, 1f, 0f);
			break;
		case GameState.InstructionsState:
			// set once to 0
			once = 0;
			// no girls in this level
			PlayerPrefs.SetInt ("Girls", 0);
			// not a level so do not update level indiction text
			lvlTxt.text = " ";
			// mistake indictator is completely transparent during this state
			mistake1.color = new Color (1f, 1f, 1f, 0f);
			mistake2.color = new Color (1f, 1f, 1f, 0f);
			mistake3.color = new Color (1f, 1f, 1f, 0f);
			break;
		case GameState.TutorialState:
			if (once == 0) {
				// update level indiction text
				lvlTxt.text = "Level 0";
				// set next state and matching game object
				nextState = GameState.Lvl1State;
				nextPrefab = Lvl1;
				// set congratulator text
				myCongrats = "Awesome, " + PlayerName + "!";
				// 2 girls in this level
				PlayerPrefs.SetInt ("Girls", 2);
				once++;
			} else {
				if (PlayerPrefs.GetInt ("Girls") == 0) {
					// no girls remaining
					if (once == 1) {
						// see noMoreGirls method
						noMoreGirls ();
						once++;
					}
				}
			}
			break;
		case GameState.Lvl1State:
			if (once == 2) {
				// update level indiction text
				lvlTxt.text = "Level 1";
				// set next state and matching game object
				nextState = GameState.Lvl2State;
				nextPrefab = Lvl2;
				// set congratulator text
				myCongrats = "You rock, " + PlayerName + "!";
				// 4 girls in this level
				PlayerPrefs.SetInt ("Girls", 4);
				once++;
			} else {
				if (PlayerPrefs.GetInt ("Girls") == 0) {
					// no girls remaining
					if (once == 3) {
						// see noMoreGirls method
						noMoreGirls ();
						once++;
					}
				}
			}
			break;
		case GameState.Lvl2State:
			if (once == 4) {
				// update level indiction text
				lvlTxt.text = "Level 2";
				// set next state and matching game object
				nextState = GameState.Lvl3State;
				nextPrefab = Lvl3;
				// set congratulator text
				myCongrats = PlayerName + ", don't forget the foreign exchange students!";
				// 6 girls in this level
				PlayerPrefs.SetInt ("Girls", 6);
				once++;
			} else {
				if (PlayerPrefs.GetInt ("Girls") == 0) {
					// no girls remaining
					if (once == 5) {
						// see noMoreGirls method
						noMoreGirls ();
						once++;
					}
				}
			}
			break;
		case GameState.Lvl3State:
			if (once == 6) {
				// update level indiction text
				lvlTxt.text = "Level 3";
				// set next state and matching game object
				nextState = GameState.Lvl4State;
				nextPrefab = Lvl4;
				// set congratulator text
				myCongrats = "Do you like anagrams, " + PlayerName + "?";
				// 6 girls in this level
				PlayerPrefs.SetInt ("Girls", 6);
				once++;
			} else {
				if (PlayerPrefs.GetInt ("Girls") == 0) {
					// no girls remaining
					if (once == 7) {
						// see noMoreGirls method
						noMoreGirls ();
						once++;
					}
				}
			}
			break;
		case GameState.Lvl4State:
			if (once == 8) {
				// update level indiction text
				lvlTxt.text = "Level 4";
				// set next state and matching game object
				nextState = GameState.TitleState;
				nextPrefab = Title;
				// set congratulator text
				myCongrats = "Happy Valentines Day, " + PlayerName + "!";
				// 6 girls in this level
				PlayerPrefs.SetInt ("Girls", 6);
				once++;
			} else {
				if (PlayerPrefs.GetInt ("Girls") == 0) {
					// no girls remaining
					if (once == 9) {
						// no more levels, so reload scene instead of moving to next level
						flavorTxt.text = myCongrats;
						Invoke ("reloadGame", 2.0f);
						once++;
					}
				}
			}
			break;
		}
	}

	void ShowScreen(GameObject gameObjectToShow) {
		// deactivate all state gameobjects
		Title.SetActive(false);
		Instruct.SetActive(false);
		Tutorial.SetActive(false);
		Lvl1.SetActive (false);
		Lvl2.SetActive (false);
		Lvl3.SetActive (false);
		Lvl4.SetActive (false);
		// set flavor text to a blank
		flavorTxt.text = " ";

		// activate gameobect of current state
		gameObjectToShow.SetActive(true);
	}

	void loadState() {
		// load state and that state gameobject with whatever was last set
		currentGameState = nextState;
		ShowScreen (nextPrefab);
	}
	 
	void noMoreGirls() {
		// update flavor text with that levels congratulatory message
		flavorTxt.text = myCongrats;
		// load next state on a delay
		Invoke ("loadState", 2.0f);
	}

	void reloadGame() {
		// reload scene... invoked elsewhere
		SceneManager.LoadScene ("Labbo4");
	}

	public void ShowInstructions() {
		// instructions button method
		currentGameState = GameState.InstructionsState;
		ShowScreen (Instruct);
	}

	public void PlayGame() {
		// play button method
		currentGameState = GameState.TutorialState;
		ShowScreen (Tutorial);
	}

	public void SetPlayerName(){
		// input textfield method
		PlayerName = pName.text;
	}
}
