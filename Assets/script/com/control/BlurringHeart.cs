using UnityEngine;
using System.Collections;

public class BlurringHeart : MonoBehaviour
{

	public float amount = 0.05F;

	// Use this for initialization
	void Start ()
	{
		GetComponent<UISprite> ().MakePixelPerfect ();

		TweenAlpha.Begin (gameObject, 1.2f, 0.0f);

		iTween.PunchScale (gameObject, iTween.Hash ("amount", new Vector3 (1 - amount, 1 + amount, 0f),
		                                            "islocal", true));
	}
}
