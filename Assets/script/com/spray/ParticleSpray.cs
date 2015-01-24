using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSpray : MonoBehaviour
{

	private Dictionary<string, GameObject> pool = new Dictionary<string, GameObject> ();
	private static ParticleSpray _instance = null;
	private static bool _block = false;

	public void Start ()
	{
		if (null == _instance) {
			_instance = this;
		}
	}

	private void LateUpdate ()
	{
		_block = false;
	}

	public static void Spray (string efxName, Vector2 position)
	{
		if (_block) {
			return;
		}
		return;
		if (! _instance.pool.ContainsKey (efxName)) {
			GameObject efx = Resources.Load<GameObject> ("sfx/prefabs/" + efxName);

			if (efx == null) {
				Debug.Log ("Can't find particle: " + efxName);
				return;
			}

			_instance.pool.Add (efxName, efx);

			GameObject temp = _instance.pool [efxName];

			Debug.Log (temp);
		} else {
			Debug.Log ("_d_d_");
		}

		GameObject toSpray = (GameObject)Instantiate (_instance.pool [efxName]);
		toSpray.transform.position = new Vector3 (position.x, position.y, 0);

		toSpray.AddComponent<Evaporation> ();
		_block = true;
	}
}
