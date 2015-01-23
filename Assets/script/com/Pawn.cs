using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
	// static utilities
	public static Pawn SprayPawn ()
	{
		Pawn pawn = new GameObject ("Pawn").AddComponent<Pawn> ();
		PawnManager.Instance ().pawns.Add (pawn);

		pawn.gameObject.layer = LayerMask.NameToLayer ("UI");

		return pawn;
	}

	public static Pawn SprayPawn (Transform parent_transform)
	{
		Pawn pawn = SprayPawn ();
		pawn.transform.parent = parent_transform;
		return pawn;
	}

	// public data
	public string pawnName = "";
	public Vector2 speed = Vector2.zero;
	private Rect boundRect;
	private const float MIN_SPEED_X = 1.55f;
	private const float MAX_SPEED_X = 2.55f;
	private const float MIN_SPEED_Y = 0.54f;
	private const float MAX_SPEED_Y = 1.15f;

	private void Start ()
	{
		// add sprite component
		UISprite sprite = gameObject.AddComponent<UISprite> ();
		sprite.atlas = Game.Instance ().UIAtlasPawn;
		if (Random.value < 0.5F) {
			sprite.spriteName = "pawn_1";
		} else {
			sprite.spriteName = "pawn_2";
		}

		sprite.MakePixelPerfect ();

		boundRect = Game.Instance ().GameArea;
		boundRect.x += sprite.width / 2;
		boundRect.y += sprite.height / 2;
		boundRect.width -= sprite.width;
		boundRect.height -= sprite.height;

		float _x = Random.value * boundRect.width + boundRect.x;
		float _y = Random.value * boundRect.height + boundRect.y;
		
		transform.localPosition = new Vector3 (_x, _y, 0f);


		// set speed
		speed.x = Random.Range (MIN_SPEED_X, MAX_SPEED_X);
		speed.y = Random.Range (MIN_SPEED_Y, MAX_SPEED_Y);
	}

	private void Update ()
	{
		Vector3 tmplPoint = transform.localPosition;

		tmplPoint.x += speed.x;
		tmplPoint.y += speed.y;

		if (tmplPoint.x < boundRect.xMin) {
			tmplPoint.x = boundRect.xMin;
			speed.x *= -1f;

			if (GetComponent<UISprite> ().flip == UIBasicSprite.Flip.Nothing) {
				GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Horizontally;
			} else {
				GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Nothing;
			}
		}

		if (tmplPoint.x > boundRect.xMax) {
			tmplPoint.x = boundRect.xMax;
			speed.x *= -1f;

			if (GetComponent<UISprite> ().flip == UIBasicSprite.Flip.Nothing) {
				GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Horizontally;
			} else {
				GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Nothing;
			}
		}

		if (tmplPoint.y < boundRect.yMin) {
			tmplPoint.y = boundRect.yMin;
			speed.y *= -1f;
		}

		if (tmplPoint.y > boundRect.yMax) {
			tmplPoint.y = boundRect.yMax;
			speed.y *= -1f;
		}

		transform.localPosition = tmplPoint;
	}

}