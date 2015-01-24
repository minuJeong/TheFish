using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSpray : MonoBehaviour
{

	private Dictionary<string, GameObject> pool = new Dictionary<string, GameObject> ();
	public Transform efxTransform; // should set in Unity Editor

	public static ParticleSpray Instance;
	private static bool _block = false;

	public void Awake ()
	{
		if (null == ParticleSpray.Instance) {
			Debug.Log (this);
			ParticleSpray.Instance = this;
		}
	}

	public void Spray (string efxName, Vector2 position, bool isEFXPanel = false)
	{
		if (_block) {
			return;
		}

		if (! pool.ContainsKey (efxName)) {
			GameObject efx = Resources.Load<GameObject> ("sfx/prefabs/" + efxName);

			if (efx == null) {
				Debug.Log ("Can't find particle: " + efxName);
				return;
			}

			pool.Add (efxName, efx);

			GameObject temp = pool [efxName];
		}

		GameObject toSpray = (GameObject)Instantiate (pool [efxName]);
		toSpray.transform.position = new Vector3 (position.x, position.y, 0);

		toSpray.AddComponent<Evaporation> ();

		if (isEFXPanel) {
			toSpray.transform.SetParent (efxTransform);
		}

		StartCoroutine (SetBlockForSeconds (0.5f));
	}

	private IEnumerator SetBlockForSeconds (float time)
	{
		float startTime = Time.time;

		while (Time.time - startTime < time) {
			_block = true;
			yield return 0;
		}

		_block = false;
	}
}
