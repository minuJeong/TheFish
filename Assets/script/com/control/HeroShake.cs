using UnityEngine;
using System.Collections;

public class HeroShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.RotateBy (gameObject, iTween.Hash ("amount", new Vector3 (0, 0, -0.01F),
		                                          "time", 1.2F,
		                                          "easetype", iTween.EaseType.easeInOutSine,
		                                          "looptype", iTween.LoopType.pingPong));
	}
}
