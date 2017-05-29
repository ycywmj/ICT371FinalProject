using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutButton : MonoBehaviour {
	/** Sets a canvas group as invisible, unclickable around
	 * stops player movement.
	 */
	public void HideCanvas()
    {
		closeUICanvas();
    }

	/** Sets a canvas group as visible, clickable around
	 * allows player movement.
	 */
	public void ShowCanvas()
	{
		CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
		if (cg != null)
		{
			cg.alpha = 1;
			cg.blocksRaycasts = true;
		}

		GameVariables.playerMoveable = false;
	}

	void closeUICanvas()
	{
		CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
		if (cg != null)
		{
			cg.alpha = 0;
			cg.blocksRaycasts = false;
		}

		GameVariables.playerMoveable = true;
	}
}
