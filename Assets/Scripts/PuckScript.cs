using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PuckScript : MonoBehaviour {

	public ScoringScript scoring;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D otherCollider) {
		GoalScript goal = otherCollider.gameObject.GetComponent<GoalScript> ();

		if (goal != null) {
				scoring.increment (goal.player);
				SoundEffectsHelper.Instance.MakeGoalSound();
				GamePad.SetVibration (0, 10,10);
		}
	}
}
