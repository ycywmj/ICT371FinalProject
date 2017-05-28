using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzleButton : MonoBehaviour {

	public void TaskOnClick()
    {
		CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
		if (cg != null)
			cg.alpha = 0;
    }
}
