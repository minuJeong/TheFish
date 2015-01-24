using LitJson;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PawnRank
{
	D = 1,
	C,
	B,
	A,
	S,
}

[System.Serializable]
public class PawnInfo
{
	public int index;
	public string name;
	public PawnRank rank;
}

public class Pawn : MonoBehaviour
{
	// static utilities
	public static Pawn SprayPawn ()
	{
		if (sprayPawnDelay > 0) {
			return null;
		}

		sprayPawnDelay = SPRAY_PAWN_DELAY;

		if (PawnManager.Instance ().isPawnMax ()) {
			// Pawn is full
			return null;
		}

		SoundManager.Instance ().Play ("crossbreed");

		Pawn pawn = new GameObject ("Pawn").AddComponent<Pawn> ();
		PawnManager.Instance ().pawns.Add (pawn);

		pawn.gameObject.layer = LayerMask.NameToLayer ("UI");

		return pawn;
	}

	public static Pawn SprayPawn (Transform parent_transform)
	{
		Pawn pawn = SprayPawn ();
		if (null == pawn) {
			return null;
		}

		pawn.transform.parent = parent_transform;
		return pawn;
	}

	public static Pawn SprayPawn (Transform parent_transform, int growthIndex)
	{
		Pawn pawn = SprayPawn (parent_transform);
		if (null == pawn) {
			return null;
		}

		pawn.growthIndex = growthIndex;
		return pawn;
	}

	public static Pawn SprayPawn (Transform parent_transform, int growthIndex, bool ignoreDelay)
	{
		Pawn pawn = null;
		if (ignoreDelay) {
			sprayPawnDelay = 0;
			pawn = SprayPawn (parent_transform, growthIndex);
		} else {
			pawn = SprayPawn (parent_transform, growthIndex);
		}

		if (null == pawn) {
			return null;
		}

		return pawn;
	}

	public static Pawn SprayPawn (Transform parent_transform, int growthIndex, bool ignoreDelay, Vector3 localPosition)
	{
		Pawn pawn = SprayPawn (parent_transform, growthIndex, ignoreDelay);
		if (null == pawn) {
			return null;
		}
		pawn.transform.localPosition = localPosition;
		pawn.isSetPosition = true;
		
		return pawn;
	}


	// static data
	public const int SPRAY_PAWN_DELAY = 10;
	public static int sprayPawnDelay = SPRAY_PAWN_DELAY;

	// public data
	public Vector2 speed = Vector2.zero;
	public int rankFactor = 0;
	public string rankName = "C";
	public int growthIndex = 0;
	public string loadedName = "";
	public double timeLeft = 0.0;
	public bool isSetPosition = false;
	public bool isRankSet = false;
	public PawnInfo info = null;

	// private data
	private Rect boundRect;
	private const float MIN_SPEED_X = 1.55f;
	private const float MAX_SPEED_X = 2.55f;
	private const float MIN_SPEED_Y = 0.54f;
	private const float MAX_SPEED_Y = 1.15f;
	private const float COLLIDE_RADIUS = 100.0f;

	private void Start ()
	{

		//
		timeLeft = Heater2.Instance ().Level [FacilityManager.Instance ().HeaterLevel].hatchTime;

		GeneratePawn ();

		if (growthIndex == 0) {
			StartCoroutine (grow ());
		}

		if (loadedName != "") {
			Debug.Log (loadedName);
			info.name = loadedName;
		}

		// add sprite component
		UISpriteAnimation spriteAnimation = gameObject.AddComponent<UISpriteAnimation> ();
		UISprite sprite = gameObject.GetComponent<UISprite> ();
		sprite.atlas = Game.Instance ().UIAtlasPawn;

		if (growthIndex == 0) {
			spriteAnimation.namePrefix = "egg_";
		} else {
			spriteAnimation.namePrefix = info.name;
		}

		spriteAnimation.framesPerSecond = 6;

		if (speed.x < 0) {
			sprite.flip = UIBasicSprite.Flip.Horizontally;
		}

		// matches size
		sprite.MakePixelPerfect ();

		// boundaries
		boundRect = Game.Instance ().GameArea;
		boundRect.x += sprite.width / 2;
		boundRect.y += sprite.height / 2;
		boundRect.width -= sprite.width;
		boundRect.height -= sprite.height;

		if (! isSetPosition) {

			float _x = Random.value * boundRect.width + boundRect.x;
			float _y = Random.value * boundRect.height + boundRect.y;

			transform.localPosition = new Vector3 (_x, _y, 0f);
		}

		// add collider
		BoxCollider2D box2d = gameObject.AddComponent<BoxCollider2D> ();
		box2d.size = new Vector2 (203, 87);

		// set speed
		speed.x = Random.Range (MIN_SPEED_X, MAX_SPEED_X);
		speed.y = Random.Range (MIN_SPEED_Y, MAX_SPEED_Y);

		if (Random.value < .5f) {
			speed.x *= -1;
		}
		if (Random.value < .5f) {
			speed.y *= -1;
		}

		punch ();
	}

	private void Update ()
	{
		// count delay
		sprayPawnDelay--;

		// collide
		Vector3 tmplPoint = transform.localPosition;

		tmplPoint.x += speed.x;
		tmplPoint.y += speed.y;

		if (tmplPoint.x < boundRect.xMin) {
			tmplPoint.x = boundRect.xMin;
			speed.x *= -1f;

			punch ();
		}

		if (tmplPoint.x > boundRect.xMax) {
			tmplPoint.x = boundRect.xMax;
			speed.x *= -1f;

			punch ();
		}

		if (tmplPoint.y < boundRect.yMin) {
			tmplPoint.y = boundRect.yMin;
			speed.y *= -1f;

			punch ();
		}

		if (tmplPoint.y > boundRect.yMax) {
			tmplPoint.y = boundRect.yMax;
			speed.y *= -1f;

			punch ();
		}

		List<Vector3> meetups = new List<Vector3> ();
		int count = PawnManager.Instance ().pawns.Count;
		for (int i = count - 1; i >= 0; i--) {
			Pawn pawn = PawnManager.Instance ().pawns [i];
			if (pawn == this) {
				continue;
			}

			Vector3 delta = transform.localPosition - pawn.transform.localPosition;
			if (delta.magnitude < COLLIDE_RADIUS) {
				// coliide!

				float angle = Mathf.Atan2 (delta.y, delta.x);

				speed.x = Mathf.Cos (angle) * speed.magnitude;
				speed.y = Mathf.Sin (angle) * speed.magnitude;

				punch ();

				Vector3 center = (transform.localPosition + pawn.transform.localPosition) * .5F;

				if (growthIndex > 0 && pawn.growthIndex > 0) {
					StartCoroutine (MateManager.Mate ());
					ParticleSpray.Instance.Spray ("HeartEfx", new Vector2 (center.x, center.y), true);
					meetups.Add (center);
				}
			}

			foreach (var meetup in meetups) {
				Pawn.SprayPawn (Game.Instance ().transform, 0, false, meetup);
			}
		}

		if (Mathf.Abs (speed.x) < 1f) {
			speed.x *= 1.08f;
		}

		// apply temp variable
		transform.localPosition = tmplPoint;
	}

	private void punch ()
	{
		if (speed.x < 0) {
			GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Horizontally;
		} else {
			GetComponent<UISprite> ().flip = UIBasicSprite.Flip.Nothing;
		}

		iTween.PunchScale (gameObject, iTween.Hash ("amount", new Vector3 (- 0.3f, 0.3f, 0f)));
	}

	private IEnumerator grow ()
	{
		if (timeLeft < 0) {
			yield break;
		}

		yield return new WaitForSeconds ((float)timeLeft);

		switch (rankName) {
		case "S":
			{
				SoundManager.Instance ().Play ("hatch_s");
			}
			break;
		case "A":
			{
				SoundManager.Instance ().Play ("hatch_a");
			}
			break;
		default:
			{
				SoundManager.Instance ().Play ("hatch_normal");
			}
			break;
		}

		growthIndex++;

		timeLeft = Heater2.Instance ().Level [FacilityManager.Instance ().HeaterLevel].hatchTime;
		GetComponent<UISpriteAnimation> ().namePrefix = info.name;

		punch ();
	}

	private void GeneratePawn ()
	{
		// Generate rank
		PawnRank rank = PawnRank.D;
		List<PawnInfo> list;
		int index = 0;

		if (isRankSet) {
			switch (rankName) {
			case "D":
				rank = PawnRank.D;
				break;
			case "C":
				rank = PawnRank.C;
				break;
			case "B":
				rank = PawnRank.B;
				break;
			case "A":
				rank = PawnRank.A;
				break;
			case "S":
				rank = PawnRank.S;
				break;
			}

		} else {
			var curInfo = Filter2.Instance ().Level [FacilityManager.Instance ().FilterLevel];
			float dice = Random.Range (0.0f, 100.0f);

			float sum = 0.0f;
			for (var i = 0; i < curInfo.percentage.Length; ++i) {
				PawnRank currentRank = i + PawnRank.D;
				if (PawnManager.Instance ().IsRankAvailable (currentRank) == false) {
					rank = (PawnRank)System.Math.Max ((int)currentRank - 1, (int)PawnRank.D);
					break;
				}

				float percentage = (float)curInfo.percentage [i];
				if (sum <= dice && dice <= sum + percentage) {
					rank = currentRank;
					break;
				}

				sum += percentage;
			}

			rankName = rank.ToString ();
		}

		// Generate instance
		list = Game.Instance ().Book.PawnInfoPerRank [rank];
		index = Random.Range (0, list.Count - 1);
		info = list [index];
	}

}

