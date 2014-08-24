using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CalibrateMaxScript : MonoBehaviour 
{

	public Clickable sc = null;
	
	void Awake()
	{
		if(sc == null)
		{
			sc = gameObject.GetComponent<Clickable>();
			if(sc == null)
			{
				sc = gameObject.AddComponent<Clickable>();
			}
		}
		sc.timer = 1f;
		sc.action += Calibrate;
	}
	
	void Calibrate()
	{
		StartCoroutine(Cor());
	}
	
	IEnumerator Cor()
	{
		long bt = DateTime.Now.Ticks;
		
		while(DateTime.Now.Ticks - bt < 30000000)
			yield return null;
		try
		{
			GameManager.Manager.Data.extrLC1.Y = GameManager.Manager.lc1set.GetSet().Average();
			GameManager.Manager.Data.extrLC2.Y = GameManager.Manager.lc2set.GetSet().Average();
			GameManager.Manager.Data.extrPress.Y = GameManager.Manager.pressset.GetSet().Average();	
		}
		catch
		{
			print("Again, some sets are null. Ignore this message if in mouse input");
		}
		
		Application.LoadLevel("calibrateA");
	}

}
