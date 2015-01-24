using UnityEngine;
using System.Collections;

public class PopulationDisplay : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
        int maxPawnCount = Tank2.Instance().Level[FacilityManager.Instance().TankLevel].maxFishCount;
		GetComponent<UILabel> ().text = PawnManager.Instance ().pawns.Count + " / " + maxPawnCount;
	}
}
