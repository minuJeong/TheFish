using UnityEngine;
using System.Collections;

public class FacilityButton : SimpleButton
{
	// should set in Unity Editor
	public GameObject Target;

	public override void Clicked ()
	{
		if (Target.activeSelf) {
			Target.SetActive (false);
		} else {
			Target.SetActive (true);
		}
	}
}
