using UnityEngine;
using System.IO;
using System.Collections;

public class DeleteSaveButton : SimpleButton {

	public override void Clicked ()
	{
		File.Delete (Application.persistentDataPath + "/db/upgrade_and_tank");
	}
}
