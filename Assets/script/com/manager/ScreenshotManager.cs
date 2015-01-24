using UnityEngine;
using System.Collections;

public class ScreenshotManager : MonoBehaviour
{
	public RenderTexture renderTexture; // should set in Unity Editor
	public Camera screenshotCamera; // should set in Unity Editor

	private static ScreenshotManager _instance;

	private void Awake ()
	{
		ScreenshotManager._instance = this;
	}

	public static ScreenshotManager Instance ()
	{
		return _instance;
	}

	public static IEnumerator TakeScreenshot ()
	{
		Instance ().screenshotCamera.gameObject.SetActive (true);

		yield return 0;

		Instance ().screenshotCamera.gameObject.SetActive (false);


		RenderTexture.active = Instance ().renderTexture;

		var texture = new Texture2D (Instance ().renderTexture.width, Instance ().renderTexture.height, TextureFormat.RGB24, false);
		texture.ReadPixels (new Rect (0, 0, Instance ().renderTexture.width, Instance ().renderTexture.height), 0, 0);
		texture.Apply ();

		byte[] bytes = texture.EncodeToPNG ();
		var wwwForm = new WWWForm ();
		wwwForm.AddBinaryData ("image", bytes, "ForHumanism.png");

		FB.API ("me/photos", Facebook.HttpMethod.POST, delegate(FBResult result)
		{
			Debug.Log (result.Text);
		}, wwwForm);
	}
}







