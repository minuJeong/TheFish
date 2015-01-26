using UnityEngine;
using System.Collections;

public class SplashInput : MonoBehaviour {

	private bool _flag = true;
	
	// Update is called once per frame
	void Update () {
		if (! _flag) {
			return;
		}
		if (Input.GetMouseButton (0)) {
			Application.LoadLevel ("1_Intro");
		}
	}
}
