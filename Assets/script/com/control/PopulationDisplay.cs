using UnityEngine;
using System.Collections;

public class PopulationDisplay : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
		GetComponent<UILabel> ().text = PawnManager.Instance ().pawns.Count + " / " + PawnManager.Instance ().MaxPawnCount;
	}
}
