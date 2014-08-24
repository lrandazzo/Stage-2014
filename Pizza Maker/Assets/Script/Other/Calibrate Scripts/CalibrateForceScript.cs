using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class CalibrateForceScript : MonoBehaviour 
{
	public SpriteRenderer sr = null;
	public Clickable cl = null;
	public Sprite put = null;
	public Sprite take = null;
	public Sprite grip = null;
	public Sprite release = null;
	public Sprite press = null;
	public string LevelAfter = "";

	void Awake()
	{
		if(sr == null)
			sr = gameObject.GetComponent<SpriteRenderer>();
		if(sr == null)
			sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = put;

		if(cl == null)
			cl = gameObject.GetComponent<Clickable>();
		if(cl == null)
			cl = gameObject.AddComponent<Clickable>();

		GameManager.Manager.calibforcedone = true;

		cl.timer = 1f;
		cl.action += put_action;
	}

	IEnumerator WaitAndDo(float seconds2wait, Action todo)
	{
		double deb = DateTime.Now.Ticks;
		while((double)(seconds2wait * 10000000) > DateTime.Now.Ticks - deb)
			yield return null;
		todo();
		yield return null;
	}

	IEnumerator WaitTillIsNotOnScreen(float seconds2wait, Action a)
	{
		double deb = DateTime.Now.Ticks;
		while((double)(seconds2wait * 10000000) > DateTime.Now.Ticks - deb)
		{
			if(InInput.controller.isOnGlass())
				deb = DateTime.Now.Ticks;
			yield return null;
		}
		a();
		yield return null;
	}

	void put_action()
	{
		StartCoroutine(WaitAndDo(1f,put_action_aux));
	}

	void put_action_aux()
	{
		// Calibrate Base Force
		try
		{
			GameManager.Manager.Data.extrLC1.X = GameManager.Manager.lc1set.GetSet().Average();
			GameManager.Manager.Data.extrLC2.X = GameManager.Manager.lc2set.GetSet().Average();
			GameManager.Manager.Data.extrPress.X = GameManager.Manager.pressset.GetSet().Average();
			GameManager.Manager.xangavg = GameManager.Manager.xangset.GetSet().Average();
			GameManager.Manager.yangavg = GameManager.Manager.yangset.GetSet().Average();
			GameManager.Manager.zangavg = GameManager.Manager.zangset.GetSet().Average();
		}
		catch
		{
			print("Some sets are null. Ignore this message if in mouse input");
			DebugScript.errormsg = "min failed";
		}

		sr.sprite = take;
		cl.action -= put_action;
		StartCoroutine(WaitTillIsNotOnScreen(0.2f,take_action));
	}

	void take_action()
	{
//		cl.timer = 2f;
//		cl.action += grip_action;
		sr.sprite = grip;
		StartCoroutine(WaitAndDo(4f,grip_action_aux));
	}

//	void grip_action()
//	{
//		StartCoroutine(WaitAndDo(1f,grip_action_aux));
//	}

	void grip_action_aux()
	{
		// Calibrate Grip
		try
		{
			GameManager.Manager.Data.extrLC1.Y = GameManager.Manager.lc1set.GetSet().Average();
		}
		catch
		{
			print("Some sets are null. Ignore this message if in mouse input");
			DebugScript.errormsg = "min failed";
		}

		sr.sprite = release;
		// cl.action -= grip_action;
		StartCoroutine(WaitTillIsNotOnScreen(2f,release_action));
	}

	void release_action()
	{
		cl.timer = 1f;
		cl.action += press_action;
		sr.sprite = press;
	}

	void press_action()
	{
		StartCoroutine(WaitAndDo(1f,press_action_aux));
	}

	void press_action_aux()
	{
		try
		{
			GameManager.Manager.Data.extrPress.Y = GameManager.Manager.pressset.GetSet().Average();
		}
		catch
		{
			print("Some sets are null. Ignore this message if in mouse input");
			DebugScript.errormsg = "min failed";
		}
		sr.sprite = take;
		cl.action -= press_action;
		StartCoroutine(WaitAndDo(0.5f,delegate {Application.LoadLevel(LevelAfter);}));
	}

}
