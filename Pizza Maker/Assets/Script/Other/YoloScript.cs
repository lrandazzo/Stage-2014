using UnityEngine;
using System.Collections;

public static class YoloScript
{

	private static bool loaded = false;
	public static void Load(string s)
	{
		if(!loaded)
		{
			GameManager.Manager.Data.Load(s);
			loaded = true;
		}
	}


}
