using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class StateSaveManager : MonoBehaviour
{

	private GameState gameState; // data class

	private void Awake ()
	{
		Game.Instance ().StateSaveManager = this;
	}

	// Use this for initialization
	public void Save ()
	{
		gameState = new GameState ();

		gameState.facilityUpgradeData = new FacilityUpgradeData (FacilityManager.Instance ().TankLevel,
		                                                         FacilityManager.Instance ().FilterLevel,
		                                                         FacilityManager.Instance ().HeaterLevel);

		foreach (var pawn in GameObject.FindObjectsOfType<Pawn> ()) {
			PawnData pawnData = new PawnData ();
			pawnData.rankFactor = pawn.rankFactor;
			pawnData.rankName = pawn.rankName;
			pawnData.growthIndex = pawn.growthIndex;

			gameState.pawnsInTank.Add (pawnData);
		}

		gameState.exitTime = Time.time;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/db/upgrade_and_tank", FileMode.CreateNew);
		bf.Serialize (file, gameState);
		file.Close ();
	}
	
	// Update is called once per frame
	public void Load ()
	{
		if ( ! File.Exists (Application.persistentDataPath + "/db/upgrade_and_tank")) {
			return;
		}

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/db/upgrade_and_tank", FileMode.Open);
		gameState = (GameState)bf.Deserialize (file);

		float elapsedTime = Time.time - gameState.exitTime;

		FacilityManager.Instance ().TankLevel = gameState.facilityUpgradeData.TankLevel;
		FacilityManager.Instance ().FilterLevel = gameState.facilityUpgradeData.FilterLevel;
		FacilityManager.Instance ().HeaterLevel = gameState.facilityUpgradeData.HeaterLevel;

		foreach (var pawnData in gameState.pawnsInTank) {
			Pawn pawn = Pawn.SprayPawn (Game.Instance ().transform, pawnData.growthIndex, true);
			pawn.rankFactor = pawnData.rankFactor;
			pawn.rankName = pawnData.rankName;
		}
	}
}

[System.Serializable]
public class GameState
{
	public FacilityUpgradeData facilityUpgradeData;
	public List<PawnData> pawnsInTank;
	public float exitTime;
}

[System.Serializable]
public class PawnData
{
	public int rankFactor;
	public string rankName;
	public int growthIndex;
}

[System.Serializable]
public class FacilityUpgradeData
{
	public int TankLevel = 0;
	public int FilterLevel = 0;
	public int HeaterLevel = 0;

	public FacilityUpgradeData (int tankLevel, int filterLevel, int heaterLevel)
	{
		TankLevel = tankLevel;
		FilterLevel = filterLevel;
		HeaterLevel = heaterLevel;
	}
}