using UnityEngine;
using System.Collections;

public class FacebookLogin : SimpleButton
{
	public GameObject FeedButton; // should set in Unity Editor

	// Facebook Login sequence
	public override void Clicked ()
	{
		if (FB.IsLoggedIn) {
			LoginComplete ();
		} else {
			FB.Init (InitComplete); 
		}
	}

	private void InitComplete ()
	{
		FB.Login ("email, publish_actions, user_likes", delegate(FBResult result)
		{
			Debug.Log (result.Text);

			LoginComplete ();
		});
	}

	private void LoginComplete ()
	{
		FeedButton.SetActive (true);
		gameObject.SetActive (false);
	}
}
