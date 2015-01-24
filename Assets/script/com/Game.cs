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

	// publc
	public int money = 0;

	// Use this for initialization
	void Awake ()
	{
		// set instance
		_instance = this;
	}

	void Start ()
	{
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
		Pawn.SprayPawn (transform, 1, true);
	}
}
