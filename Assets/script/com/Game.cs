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

	// publc
	public int money = 0;

	// property
	public Book Book { get { return book; } }

	// Use this for initialization
	void Awake ()
	{
		// set instance
		_instance = this;
	}

	void Start ()
	{
		book.Init ();
		Heater2.Instance ().Init ();
		Tank2.Instance ().Init ();
		Filter2.Instance ().Init ();

//         FacilityManager.Instance().TankLevel = 15;
//         FacilityManager.Instance().HeaterLevel = 15;
//         FacilityManager.Instance().FilterLevel = 15;

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
	}
}
