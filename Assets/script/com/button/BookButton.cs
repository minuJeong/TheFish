using UnityEngine;
using System.Collections;

public class BookButton : SimpleButton
{
	// should set in Unity Editor
	public GameObject Target;

	public override void Clicked ()
	{
		Debug.Log ("Book Button");
	}
}
