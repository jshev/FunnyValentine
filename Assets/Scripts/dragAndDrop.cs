using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dragAndDrop : MonoBehaviour {
	
	// set private variables in script
	Vector3 screenPoint;
	Vector3 offset;

	Vector2 defaultPos;

	string myGirl;

	int tempGirls;
	int tempMistakes;

	// Use this for initialization
	void Start () {
		// set default position as the position of the card on Start
		defaultPos = transform.localPosition;

		switch (gameObject.name)
		{
		// tutorial cards
		case "gothCard":
			myGirl = "gothGirl";
			break;
		case "rainbowCard":
			myGirl = "rainbowGirl";
			break;
		
		// lvl1 cards
		case "musicCard":
			myGirl = "musicGirl";
			break;
		case "flowerCard":
			myGirl = "flowerGirl";
			break;
		case "artCard":
			myGirl = "artGirl";
			break;
		case "bakerCard":
			myGirl = "bakerGirl";
			break;
		
		// lvl2 cards
		case "beeCard":
			myGirl = "beeGirl";
			break;
		case "carCard":
			myGirl = "carGirl";
			break;
		case "stemCard":
			myGirl = "stemGirl";
			break;
		case "gamerCard":
			myGirl = "gamerGirl";
			break;
		case "nerdyCard":
			myGirl = "nerdyGirl";
			break;
		case "sportyCard":
			myGirl = "sportyGirl";
			break;

		// lvl3 cards
		case "britishCard":
			myGirl = "britishGirl";
			break;
		case "canadaCard":
			myGirl = "canadaGirl";
			break;
		case "germanCard":
			myGirl = "germanGirl";
			break;
		case "frenchCard":
			myGirl = "frenchGirl";
			break;
		case "japanCard":
			myGirl = "japanGirl";
			break;
		case "indoCard":
			myGirl = "indoGirl";
			break;

			// lvl4 cards
		case "heartCard":
			myGirl = "earthGirl";
			break;
		case "toasterCard":
			myGirl = "rotatesGirl";
			break;
		case "damselCard":
			myGirl = "medalsGirl";
			break;
		case "cosmicCard":
			myGirl = "comicsGirl";
			break;
		case "peaCard":
			myGirl = "apeGirl";
			break;
		case "resistCard":
			myGirl = "sisterGirl";
			break;

		default:
			// debug tool to ensure all cards are assigned girls
			Debug.Log(gameObject.name + " has no girl!");
			break;
		}

	}

	// Update is called once per frame
	void Update () {
		// if user presses R on any of the matching levels, reload the scene
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("Labbo4");
		}
	}

	// OnMouseDown and OnMouseDrag allow the cards to be clicked and dragged in the game window
	// got code from http://coffeebreakcodes.com/drag-object-with-mouse-unity3d/
	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
	}

	void OnMouseUp() {
		// return the card to its default position on mouse up
		transform.position = defaultPos;

		// raycasting determines whether the player dragged card to girl and, if they did, whether it was the right girl
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if((hit.collider != null) && (hit.transform.tag == "girl"))
		{
			// if player dragged card over to a girl...
			if (hit.transform.name == myGirl) {
				// if the girl matches the card...
				// decrease count of girls remaining in level and update PlayerPrefs
				tempGirls = PlayerPrefs.GetInt ("Girls") - 1;
				PlayerPrefs.SetInt ("Girls", tempGirls);
				//Debug.Log ("MY GIRL!!!");
				// deactivate matching card and girl
				hit.transform.gameObject.SetActive (false);
				gameObject.SetActive (false);
			} else {
				// if the girl does not match the card...
				// increase mistakes count and update PlayerPrefs
				tempMistakes = PlayerPrefs.GetInt ("Mistakes") + 1;
				PlayerPrefs.SetInt ("Mistakes", tempMistakes);
				//Debug.Log ("opps...");
			}
		}
	}

}
