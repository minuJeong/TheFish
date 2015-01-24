using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class ShowAdButton : SimpleButton
{

	private void Update ()
	{
		if (! Advertisement.isInitialized) {
			GetComponent<UISprite> ().alpha = 0.0f;
		}
	}

	public override void Clicked ()
	{
		GetComponent<UISprite> ().alpha = 1.0f;

		TweenAlpha.Begin (gameObject, 0.5f, 0.0f);

		if (Advertisement.isReady ()) {
			Advertisement.Show ();

			Game.Instance ().money += 100000;
		}
	}
}
