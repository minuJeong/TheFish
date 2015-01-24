using UnityEngine;
using System.Collections;

[System.Serializable]
public class Faculty
{
	public string FacultyName = "";

	public virtual void Upgrade ()
	{
	}
}

public class Tank : Faculty
{
	public override void Upgrade ()
	{
		base.Upgrade ();

		if (Game.Instance ().money > 1000) {
			PawnManager.Instance ().MaxPawnCount ++;
			Game.Instance ().money -= 1000;
		} else {
			Debug.Log ("Not enough money");
		}
	}
}

public class Filter : Faculty
{
	public override void Upgrade ()
	{
		base.Upgrade ();


	}
}

public class Warmer : Faculty
{
	public override void Upgrade ()
	{
		base.Upgrade ();


	}
}