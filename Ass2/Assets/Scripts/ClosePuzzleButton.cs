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
		if (GameVariables.p2option == 1)
			GameVariables.p2DoorOpen = true;

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

	public void p2Option(int opt)
	{
		GameVariables.p2attempts++;
		switch(opt)
		{
			case 1:
				GameVariables.p2option = 1;
				if (GameVariables.homeworkComplete)
					GameVariables.p2DoorOpen = true;
				closeUICanvas();
				break;
			case 2:
				GameVariables.p2DoorOpen = true;
				closeUICanvas();
				GameVariables.p2option = 2;
				break;
			case 3:
				GameVariables.p2option = 3;
				closeUICanvas();
				break;
			case 4:
				GameVariables.p2option = 4;
				closeUICanvas();
				break;
		}
	}

	public void p3Option(int opt)
	{
		GameVariables.p3attempts++;
		switch(opt)
		{
			case 1:
				p3One();
				break;
			case 2:
				p3Two();
				break;
			case 3:
				p3Three();
				break;
			case 4:
				p3Four();
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
		GameObject.Find("HelpPanelText").GetComponent<UnityEngine.UI.Text>().text = "This puzzle includes a WHILE DO loop,"
				+ " when the condition (between WHILE and DO) is true the code will be executed"
		 		+ "; it will then loop, executing the code again if the condition is true.\n\n"
				+ "There are multiple ways to open the doors.";
	}

	void p3One()
	{
		print("o1");
		if (GameVariables.keys.Contains("Green") && GameVariables.keys.Contains("Blue"))
			completeP3();
		else
			closeUICanvas();
	}
	void p3Two()
	{
		if (GameVariables.keys.Contains("Pink") && GameVariables.homeworkComplete)
			completeP3();
		else
			closeUICanvas();
	}
	void p3Three()
	{
		if (GameVariables.keys.Contains("Blue") || GameVariables.homeworkComplete)
			completeP3();
		else
			closeUICanvas();
	}
	void p3Four()
	{
			completeP3();
	}

	void completeP3()
	{
		print("p3 done");
		closeUICanvas();
		GameVariables.p3complete = true;
		GameVariables.currentPuzzle++;
	}

}
