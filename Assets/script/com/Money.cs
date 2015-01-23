using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{

	// Update is called once per frame
	void Update ()
	{
		GetComponent<UILabel> ().text = Game.Instance ().money + " Won";
	}
}
