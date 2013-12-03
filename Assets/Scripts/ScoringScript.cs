using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ScoringScript : MonoBehaviour {

	public int playerOneScore, playerTwoScore;

	public Transform playerOne, playerTwo, puck;

	public GUIStyle blueStyle = null;
	public GUIStyle orangeStyle = null;

	private float timeScored = 0;
	private bool scoreMessage = false;
	private bool oneScored = false;

	public float victoryDuration = 1.5f;

	void Awake() {
		GamePad.SetVibration (0, 0, 0);
	}

	void Update() {
		if (scoreMessage)
			if (Time.time - timeScored > 3)
			{
				timeScored = 0; scoreMessage = false; oneScored = false; 
			}
	}

	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 32, Screen.height / 16, Screen.width / 16, Screen.height / 8), playerOneScore.ToString (), orangeStyle);
		GUI.Label (new Rect ((Screen.width / 32)*29, Screen.height / 16, Screen.width / 16, Screen.height / 8), playerTwoScore.ToString (), blueStyle);
		if (scoreMessage)
		{
			GUI.Label (new Rect (320, 240, 320, 240), "GOAL!", (oneScored) ? orangeStyle : blueStyle); return;
		}
		return;
	}

	public void increment(int i)
	{
		if (i == 1) 
		{
			playerOneScore += 1;
			oneScored = true;

		}
		else if (i == 2) 
		{

			playerTwoScore += 1;
			oneScored = false;
		}
		scoreMessage = true;
		timeScored = Time.time;
		Reset ();
		playerOne.gameObject.GetComponent<RumbleScript> ().RumbleForSeconds (victoryDuration, 10f, 10f);
		playerTwo.gameObject.GetComponent<RumbleScript> ().RumbleForSeconds (victoryDuration, 10f, 10f);
		/*Camera.main.SendMessage ("fadeOut");
		StartCoroutine(Reset ());
		Camera.main.SendMessage ("fadeIn");*/
	}

	public void Reset()
	{
		puck.position = new Vector3 (0f, 0f, 1f);
		playerOne.position = new Vector3 (-0.3f, 0f, 0f);
		playerTwo.position = new Vector3 (0.3f, 0f, 0f);

		puck.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f); 
		playerOne.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		playerTwo.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
	}


}
