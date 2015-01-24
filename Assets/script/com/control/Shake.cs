using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ScaleBy (gameObject, iTween.Hash ("amount", new Vector3 (0.95f, 1.05f, 1f),
		                                         "time", 1f,
		                                         "looptype", iTween.LoopType.pingPong));
	}
}
