using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSlot : MonoBehaviour 
{
	public int LevelNumber = 0;
	private Slot selfSlot = null;

	void Start()
	{
		selfSlot = gameObject.GetComponent<Slot>();
		if(selfSlot == null)
			selfSlot = gameObject.AddComponent<Slot>();
	}

	public LevelToken GetToken()
	{
		var l = selfSlot.Sloted.FindAll(t => t.gameObject.GetComponent<LevelToken>() != null);
		if(l.Count > 0)
			return l[0].gameObject.GetComponent<LevelToken>(); // != null
		else
			return null;
	}


}
