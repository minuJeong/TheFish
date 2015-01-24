using LitJson;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Faculty
{
	public static JsonData data = null;

	public Faculty ()
	{
		if (null == data) {
			Faculty.data = JsonMapper.ToObject (Resources.Load<TextAsset> ("info/Faculty").text);
		}
	}

	protected void Init (string name)
	{
		FacultyName = name;
		price = (float)((int)Faculty.data [FacultyName] ["base_price"]);
		price_increment = (float)((int)Faculty.data [FacultyName] ["price_increment"]);
		price_multipliment = (float)((double)Faculty.data [FacultyName] ["price_multipliment"]);
	}

	public string FacultyName = "";
	public float price = 0;
	public float price_increment = 0;
	public float price_multipliment = 0;
	public int level = 0;
	public UILabel label; // should set in Unity Editor

	public virtual void UpgradeSuccess ()
	{
	}

	public virtual void Upgrade ()
	{
		if (Game.Instance ().money >= price) {
			Game.Instance ().money -= (int)Mathf.Floor (price);
			
			price *= price_multipliment;
			price += price_increment;
			price = (int)Mathf.Floor (price);
			 
			level++;

			UpdatePriceText ();
			UpgradeSuccess ();
		} else {
			Debug.Log ("Not enough money");
		}
	}

	public void UpdatePriceText ()
	{
		if (null == label) {
			return;
		}

		label.text = FacultyName + ": " + price + " won";
	}
}

[System.Serializable]
public class Tank : Faculty
{
	public Tank ()
	{
		Init ("tank");
	}

	public override void UpgradeSuccess ()
	{
		PawnManager.Instance ().MaxPawnCount *= (float)((double)data [FacultyName] ["effect"]);
	}
}

[System.Serializable]
public class Filter : Faculty
{
	public Filter ()
	{
		Init ("filter");
	}

	public override void UpgradeSuccess ()
	{

	}
}

[System.Serializable]
public class Warmer : Faculty
{
	public Warmer ()
	{
		Init ("warmer");
	}

	public override void UpgradeSuccess ()
	{
		Pawn.growthFactor *= (float)((double)data [FacultyName] ["effect"]);
	}
}

