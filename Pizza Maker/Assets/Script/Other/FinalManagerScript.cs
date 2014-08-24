using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class FinalManagerScript : MonoBehaviour 
{
	public Rotator rot = null;
	public List<Spray> sprayList = new List<Spray>();
	public List<Bowl> boxList = new List<Bowl>();

	void Awake()
	{
		if(rot != null)
		{
			foreach(var a in sprayList)
				a.onEmpty += rot.Next;
			foreach(var a in boxList)
				a.onEmpty += rot.Next;
		}
	}
	

}
