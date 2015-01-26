using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveAdd (gameObject, iTween.Hash ("amount", new Vector3 (0, 0.05f, 0),
		                                         "islocal", true,
		                                         "looptype", iTween.LoopType.pingPong));
	}
}
