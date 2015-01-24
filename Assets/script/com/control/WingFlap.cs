using UnityEngine;
using System.Collections;

public class WingFlap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.RotateAdd (gameObject, iTween.Hash ("amount", new Vector3 (0, 0, 15),
		                                           "easetype", iTween.EaseType.linear,
		                                           "time", 1.5f,
		                                           "looptype", iTween.LoopType.pingPong));
	}
}
