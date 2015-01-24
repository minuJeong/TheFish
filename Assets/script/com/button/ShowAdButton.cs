using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class ShowAdButton : SimpleButton
{

	private const string GameAdID = "131622779";

	public override void Clicked ()
	{
		Debug.Log ("_hello ad");
		GetComponent<UISprite> ().alpha = 1.0f;

		Advertisement.Initialize (GameAdID);
		if (Advertisement.isReady ()) {
			Advertisement.Show ();
		}
	}
}
