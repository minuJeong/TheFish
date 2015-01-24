using UnityEngine;
using System.Collections;

public class Evaporation : MonoBehaviour
{

	private int _life = 100;

	public void AddLife (int life)
	{
		_life += life;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (_life > 0) {
			_life--;
		} else {
			Destroy (gameObject, 0.1f);
		}
	}
}

