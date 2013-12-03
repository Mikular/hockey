using UnityEngine;
using System.Collections;

public class NoMouseScript : MonoBehaviour {
	void Start ()
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
}