using UnityEngine;
using System.Collections;

public class FadeBlink : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (StartFade ());
	}

	private IEnumerator StartFade ()
	{
		while (true) { 
			TweenAlpha.Begin (gameObject, 0.5f, 0.0f);
			yield return new WaitForSeconds (0.5f);
			TweenAlpha.Begin (gameObject, 0.5f, 1.0f);
			yield return new WaitForSeconds (0.5f);
		}
	}
}
