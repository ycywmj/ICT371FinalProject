using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzleButton : MonoBehaviour {

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

	public void CompleteHomework()
	{
		closeUICanvas();

		UnityEngine.UI.Button btn = GameObject.Find("p1HomeworkBTN").GetComponent<UnityEngine.UI.Button>();
		if (btn != null)
			btn.interactable = false;

		GameVariables.homeworkComplete = true;
	}

	public void p1Option(int opt)
	{


		GameVariables.p1attempts++;
		switch(opt)
		{
			case 1:
				p1One();
				break;
			case 2:
				p1Two();
				break;
			case 3:
				p1Three();
				break;
			case 4:
				p1Four();
				break;
		}
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

	void p1One()
	{
		if (GameVariables.keys.Contains("Green"))
			completeP1();
		else
			closeUICanvas();
	}
	void p1Two()
	{
		if (GameVariables.keys.Contains("Pink"))
			completeP1();
		else
			closeUICanvas();
	}
	void p1Three()
	{
			closeUICanvas();
	}
	void p1Four()
	{
		if (GameVariables.homeworkComplete)
			completeP1();
		else
			closeUICanvas();
	}
	void completeP1()
	{
		closeUICanvas();
		GameVariables.p1complete = true;
		GameVariables.currentPuzzle++;
		//TODO: open Door
		GameObject door = GameObject.Find("Door1");
		if (door != null)
		{
			door.GetComponent<Transform>().position = new Vector3(1.3135f, 0.829f, 4.3695f);
 			door.GetComponent<Transform>().rotation = Quaternion.Euler(-90,0,90);
		}
	}

	void Help()
	{
		//EditoyUnity.DisplayDialog("Help", "If statment is good", "Continue");
	}
}
