using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveBy (gameObject, iTween.Hash ("amount", new Vector3 (0, 0.01f, 0),
		                                          "time", 1.5F,
		                                          "easetype", iTween.EaseType.easeInOutSine,
		                                          "looptype", iTween.LoopType.pingPong));
	}
}
