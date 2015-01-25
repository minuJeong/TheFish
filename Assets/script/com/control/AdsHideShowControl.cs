using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

[SerializePrivateVariables]
public class AdsHideShowControl : MonoBehaviour
{

	private const int COUNTDOWN = 550;
	private int countdown = COUNTDOWN;
	[HideInInspector]
	private const string
		GameAdID = "131622779";
	[HideInInspector]
	private Transform
		showhide = null;

	private void Start ()
	{
		showhide = transform.GetChild (0);

		Advertisement.Initialize (GameAdID);
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (countdown > 0) {
			countdown--;
		} else {
			countdown = COUNTDOWN;
			showhide.gameObject.SetActive (true);
			HideAfterSeconds (5.0f);

			foreach (var sprite in showhide.GetComponentsInChildren<UISprite> ()) {
				sprite.alpha = 1.0f;
			}
		}

		foreach (var sprite in showhide.GetComponentsInChildren<UISprite> ()) {
			sprite.alpha *= 0.995f;
		}
	}

	private IEnumerator HideAfterSeconds (float time)
	{

		yield return new WaitForSeconds (time);

		showhide.gameObject.SetActive (false);
	}
}
