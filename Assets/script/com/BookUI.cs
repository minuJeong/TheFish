using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BookUI : MonoBehaviour
{
	private UIGrid grid;

	public void Init (Book book)
	{
		if (book.PawnInfoList.Count == 0) {
			return;
		}

//		grid = transform.FindChild ("Clip").FindChild ("Foreground").FindChild ("grid").gameObject.GetComponent<UIGrid> ();
//		var item = grid.gameObject.transform.FindChild ("item").gameObject;

		var item = transform.FindChild ("Clip/Foreground/dragged/item").gameObject;

		var count = book.PawnInfoList.Count;
		for (int i = 0; i < count; i++) {
			var pair = book.PawnInfoList [i];

			// Add item object
			var clone = (GameObject)UnityEngine.Object.Instantiate (item);
			clone.transform.parent = item.transform.parent;

			clone.transform.localPosition = item.transform.localPosition;
			clone.transform.localScale = item.transform.localScale;
			clone.transform.localPosition += new Vector3 (0, -305.6f * i, 0);

			// Fill data
			var name = clone.transform.FindChild ("name_text").gameObject.GetComponent<UILabel> ();
			var rank = clone.transform.FindChild ("rank_text").gameObject.GetComponent<UILabel> ();
			var description = clone.transform.FindChild ("description_text").gameObject.GetComponent<UILabel> ();

			name.text = pair.name;
			rank.text = pair.rank.ToString ();
			description.text = pair.rank.ToString ();

			clone.GetComponent<UIPanel> ().depth += i;

			if (book.UnlockedList.ContainsValue (pair)) {
				iTween.MoveBy (clone.transform.FindChild ("curtain").gameObject, iTween.Hash ("amount", new Vector3 (0, 200, 0)));
			}
		}

		GameObject.Destroy (item.gameObject);
	}

	private void Update ()
	{
		var dragged = transform.FindChild ("Clip/Foreground/dragged").gameObject;
		for (int i = 0; i < dragged.transform.childCount; i++) {
			var g = dragged.transform.GetChild (i);
			var diff = g.transform.localPosition.y + dragged.transform.localPosition.y;
			if (Mathf.Abs (diff) > 500) {
				g.gameObject.SetActive (false);
			} else {
				g.gameObject.SetActive (true);
			}
		}
	}
}
