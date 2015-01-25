using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

	private static Game _instance;

	public static Game Instance ()
	{
		if (_instance == null) {
			Debug.Log ("Game instance should not null");
		}

		return _instance;
	}

	// should set in Unity Editor
	public UIAtlas UIAtlasMain;
	public UIAtlas UIAtlasPawn;
	public Camera UICamera;
	public Rect GameArea;

	// private
	private Book book = new Book ();
	private StateSaveManager _saved;

	// publc
	public int money = 0;

	// property
	public Book Book { get { return book; } }

	public StateSaveManager StateSaveManager { get { return _saved; } set { _saved = value; } }

	// Use this for initialization
	void Awake ()
	{
		// set instance
		_instance = this;
	}

	void Start ()
	{

		Vector4 v4 = GetComponentInParent<UIPanel> ().baseClipRegion;

		Debug.Log (GetComponentInParent<UIPanel> ().clipOffset);
		Debug.Log (GetComponentInParent<UIPanel> ().width);
		Debug.Log (GetComponentInParent<UIPanel> ().baseClipRegion);



//		GameArea = new Rect (v4.x, v4.y, v4.w, v4.z);

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Heater2.Instance ().Init ();
		Tank2.Instance ().Init ();
		Filter2.Instance ().Init ();

		book.Init ();
		_saved.Load ();

//      FacilityManager.Instance().TankLevel = 15;
//      FacilityManager.Instance().HeaterLevel = 15;
//      FacilityManager.Instance().FilterLevel = 15;

		GameObject UI_Root = GameObject.Find ("UI Root");
        
		UI_Root.transform
            .FindChild ("Book")
            .GetComponent<BookUI> ()
            .Init (book);

		UI_Root
            .transform
            .FindChild ("Facilities")
            .GetComponent<FacilityUI> ()
            .Init ();

		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);

		StartCoroutine (HomeKeyInput ());
	}

	IEnumerator HomeKeyInput ()
	{
		while (true) {
			if (Application.platform == RuntimePlatform.Android) {
				if (Input.GetKey (KeyCode.Home)) {
					_saved.Save ();
					yield return new WaitForSeconds (3f); // wait for next savable frame
				}

				yield return 0; // wait for next frame
			} else {
				yield break; // ends coroutine
			}
		}
	}

	void OnApplicationExit ()
	{
		_saved.Save ();
	}
}
