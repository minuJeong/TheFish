using UnityEngine;
using System.Collections;

public class TakeScreenshotButton : SimpleButton
{

	private const int DELAY = 25;
	private int delay = DELAY;

	public override void Clicked ()
	{
		if (delay > 0) {
		} else {
			StartCoroutine (ScreenshotManager.TakeScreenshot ());
		}
	}

	private void Update ()
	{
		if (delay > 0) {
			--delay;
		}
	}
}
