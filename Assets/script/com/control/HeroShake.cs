using UnityEngine;
using System.Collections;

public class HeroShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ScaleBy (gameObject, iTween.Hash ("amount", new Vector3 (0.97F, 0.97F, 0F),
		                                          "time", 2.2F,
		                                          "easetype", iTween.EaseType.easeInOutSine,
		                                          "looptype", iTween.LoopType.pingPong));
	}
}
