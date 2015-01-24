using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour
{

	private void Start () {
		StartCoroutine (RunIdle ());
	}
	
	// Update is called once per frame
	private void Update ()
	{

	}

	private IEnumerator RunIdle () {
		yield return new WaitForSeconds (240);

		while (true) {
			yield return new WaitForSeconds (120);

			ShowIdleMessage ();
		}
	}

	private void ShowIdleMessage () {

	}
}
