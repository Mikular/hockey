using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class RumbleScript : MonoBehaviour {

	public PlayerIndex controller;

	bool rumbleActive = false;
	float timeActivated = 0;
	float setDuration = 0;

	void Awake ()
	{
		Kill ();
	}

	public void Kill ()
	{
		GamePad.SetVibration (controller, 0, 0);
	}

	void Update ()
	{
		if (rumbleActive && Time.time - timeActivated > setDuration)
		{
			Kill ();
			rumbleActive = false;
			setDuration = 0;
		}
	}

	// Use this for initialization
	public void RumbleForSeconds(float duration, float left, float right)
	{
		GamePad.SetVibration(controller, left, right);
		rumbleActive = true;
		timeActivated = Time.time;
		setDuration = duration;
	}
}
