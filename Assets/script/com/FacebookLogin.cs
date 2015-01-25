using LitJson;
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
		FB.Login ("email, publish_actions, user_likes, user_photos", delegate(FBResult result)
		{
			Debug.Log (result.Text);

			LoginComplete (result);
		});
	}

	private void LoginComplete (FBResult result = null)
	{
		FB.API ("me/permissions/user_photos", Facebook.HttpMethod.GET, delegate(FBResult result_permission)
		{
			JsonData data = JsonMapper.ToObject (result_permission.Text);

			if ((string)data ["data"] [0] ["permission"] == "user_photos" &&
				(string)data ["data"] [0] ["status"] == "granted") {

				Debug.Log ("Log in successful");
			} else {
				FB.Login ("user_photos", LoginComplete);
			}
		});

		FeedButton.SetActive (true);
		gameObject.SetActive (false);
	}
}
