using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputIntro : MonoBehaviour
{

	private int phaseIndex = 0;
	private List <GameObject> phases = new List <GameObject> ();
	private const int DELAY = 20;
	private int delay = DELAY;

	private GameObject fish;
	private GameObject pawn;

	void Start ()
	{
		phases.Add (GameObject.Find ("#1_1"));
		phases.Add (GameObject.Find ("#1_2"));
		phases.Add (GameObject.Find ("#1_3"));
		phases.Add (GameObject.Find ("#2_1"));
		phases.Add (GameObject.Find ("#2_2"));
		phases.Add (GameObject.Find ("#2_3"));

		foreach (var phase in phases) {
			phase.SetActive (false);
		}

		phases [0].SetActive (true);

		fish = GameObject.Find ("fish");
		fish.SetActive (false);

		pawn = GameObject.Find ("pawn");
		pawn.SetActive (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (delay > 0) {
			delay--;
		} else {
			if (Input.GetMouseButton (0)) {
				delay = DELAY;
				ProceedPhase ();
			}
		}
	}

	private void ProceedPhase ()
	{
		if (phases.Count > phaseIndex) {
			phases [phaseIndex].SetActive (false);
		}

		phaseIndex++;

		if (phases.Count > phaseIndex) {
			if (phaseIndex > 2) {
				pawn.SetActive (false);
				fish.SetActive (true);
			}
			phases [phaseIndex].SetActive (true);
		} else {
			Application.LoadLevel ("2_Game");
		}
	}
}
