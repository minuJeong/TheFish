using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		iTween.RotateBy (gameObject, iTween.Hash ("amount", new Vector3 (0, 0, -0.015F),
		                                          "time", 1.5F,
		                                          "easetype", iTween.EaseType.easeInOutSine,
		                                          "looptype", iTween.LoopType.pingPong));
	}
}
