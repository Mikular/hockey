using UnityEngine;
using System.Collections;

public class BlinkScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Time.time % 0.5 == 0) 
		{
			renderer.enabled = !renderer.enabled;
		}
	}
}
