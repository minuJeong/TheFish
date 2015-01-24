using UnityEngine;
using System.Collections;

public class BookButton : SimpleButton
{
    public GameObject Target;

    public override void Clicked()
    {
        if (Target.activeSelf)
        {
            Target.SetActive(false);
        }
        else
        {
            Target.transform.localPosition = new Vector3(0, 0, 0);
            Target.SetActive(true);
        }
    }
}
