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

		grid = transform.FindChild ("Clip").FindChild ("Foreground").FindChild ("grid").gameObject.GetComponent<UIGrid> ();
		var item = grid.gameObject.transform.FindChild ("item").gameObject;

		foreach (var pair in book.PawnInfoList) {
			var info = pair.Value;

			// Add item object
			var clone = (GameObject)UnityEngine.Object.Instantiate (item);
			clone.transform.parent = grid.gameObject.transform;

			clone.transform.localPosition = item.transform.localPosition;
			clone.transform.localScale = item.transform.localScale;

			var newPosition = clone.transform.localPosition;
			newPosition.y += -275.24f;
			clone.transform.localPosition = newPosition;

			// Fill data
			var name = clone.transform.FindChild ("name_text").gameObject.GetComponent<UILabel> ();
			var description = clone.transform.FindChild ("description_text").gameObject.GetComponent<UILabel> ();

			name.text = info.name;
			description.text = info.rank.ToString ();// TODO: rank to description

			Debug.Log (book.UnlockedList.Contains (pair));
		}

		GameObject.Destroy (item.gameObject);
	}
}
