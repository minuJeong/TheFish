using UnityEngine;
using System.Collections;

public class FacebookLogin : SimpleButton
{
	// Facebook Login sequence
	public override void Clicked ()
	{
		if (FB.IsLoggedIn) {
			FB.Feed (toId: "",
			         link: "",
			         linkName: "",
			         linkCaption: "",
			         linkDescription: "",
			         picture: "",
			         mediaSource: "",
			         actionName: "",
			         actionLink: "",
			         reference: "",
			         properties: null,
			         callback: delegate (FBResult result)
			{
				Debug.Log (result.Text);
			});
		} else {
			FB.Init (InitComplete); 
		}
	}

	private void InitComplete ()
	{
		FB.Login ("email, publish_actions, user_likes", delegate(FBResult result)
		{
			Debug.Log (result.Text);

			LoggedIn ();
		});
	}

	private void LoggedIn ()
	{
		FB.API ("/me?fields=id,name", Facebook.HttpMethod.GET, delegate(FBResult result)
		{
			Debug.Log (result.Text);
		});
	}
}
