using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
	void Update()
	{
		if (Input.anyKeyDown)
			Application.LoadLevel("HockeyGame");
	}
}