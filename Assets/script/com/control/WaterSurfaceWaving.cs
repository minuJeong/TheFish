using UnityEngine;
using System.Collections;

public class WaterSurfaceWaving : MonoBehaviour {
	private void Start (){
		iTween.ScaleAdd (gameObject, iTween.Hash ("amount", new Vector3 (-0.05f, 0.15f, 0f),
		                                          "easetype", iTween.EaseType.linear,
		                                          "looptype", iTween.LoopType.pingPong));
	}
}
