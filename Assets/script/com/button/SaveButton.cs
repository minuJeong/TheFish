using UnityEngine;
using System.Collections;

public class SaveButton : SimpleButton
{

	public override void Clicked ()
	{
		Game.Instance ().StateSaveManager.Save ();
	}
}
