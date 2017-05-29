using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables {

	public static int currentPuzzle = 1;

	public static bool homeworkComplete = false;

	public static bool playerMoveable = true;

	public static List<string> keys = new List<string>();

	//Puzzle One
	public static bool p1complete = false;
	public static int p1attempts = 0;

	//Puzzle Two
	public static bool p2complete = false;
	public static int p2attempts = 0;
	public static int p2option = 0;
	public static bool p2DoorOpen = false;

	//Puzzle Three
	public static bool p3complete = false;
	public static int p3attempts = 0;
}
