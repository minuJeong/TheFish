using UnityEngine;
using System.Collections;

public class CloseButton : SimpleButton {

	public GameObject DisableTarget = null;

	public override void Clicked ()
	{
		if (null != DisableTarget) {
			DisableTarget.SetActive (false);
		}
	}
}
