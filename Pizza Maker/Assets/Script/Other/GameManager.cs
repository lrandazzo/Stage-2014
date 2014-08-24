using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour 
{
	public GameData Data{get;set;}

	public static GameManager Manager = null;
	
	public float xangavg = 0;
	public float yangavg = 0;
	public float zangavg = 0;

	public TempSet<float> xangset = new TempSet<float>(1f);
	public TempSet<float> yangset = new TempSet<float>(1f);
	public TempSet<float> zangset = new TempSet<float>(1f);
	public TempSet<float> lc1set = new TempSet<float>(1f);
	public TempSet<float> lc2set = new TempSet<float>(1f);
	public TempSet<float> pressset = new TempSet<float>(1f);

	public RectScreen touchedRect = new RectScreen();
	public RectScreen trueRect = new RectScreen();

	private List<string> levels = new List<string>();
	private List<string> remainingLevels = new List<string>();
	public bool calibforcedone = false;

	public List<string> Levels
	{
		get
		{
			return levels;
		}
		set
		{
			levels = new List<string>(value);
			remainingLevels = new List<string>(value);
		}
	}

	void Awake()
	{
		if(Manager == null)
		{
			Manager = this;
			Data = new GameData();
			Data.WSpace = new Workspace();
			Levels = LevelManager.RegularLevels.ToList();
		}
		else if(Manager != this)
			Destroy(this);
		
	}

	public string NextLevel()
	{
		if(!calibforcedone)
		{
			return "calibforce";
		}
		else if(remainingLevels.Count > 0)
		{
			string res = remainingLevels.First();
			remainingLevels.Remove(res);
			return res;
		}
		else
			return LevelManager.LastLevel;
	}

	public void ResetLevels()
	{
		remainingLevels = new List<string>(levels);
	}

	public void ResetCalibrationPosition()
	{
		Data.bl = new Vector2(0,0);
		Data.blref = new Vector2(0,0);
		Data.ratioTransfo = new Pair<float, float>(1f,1f);
		touchedRect = new RectScreen();
		trueRect = new RectScreen();

	}

	public void SetCalibrationPosition()
	{
		Data.bl = new Vector2((touchedRect.A.x + touchedRect.D.x) / 2f,(touchedRect.D.y + touchedRect.C.y) / 2f);
		Vector2 tr = new Vector2((touchedRect.B.x + touchedRect.C.x) / 2f,(touchedRect.A.y + touchedRect.B.y) / 2f);
		Data.blref = new Vector2((trueRect.A.x + trueRect.D.x) / 2f,(trueRect.D.y + trueRect.C.y) / 2f);
		Vector2 trref = new Vector2((trueRect.B.x + trueRect.C.x) / 2f,(trueRect.A.y + trueRect.B.y) / 2f);

		try
		{
			Data.ratioTransfo.X = (Data.blref.x - trref.x) / (Data.bl.x - tr.x);
			Data.ratioTransfo.Y = (Data.blref.y - trref.y) / (Data.bl.y - tr.y);

		}
		catch
		{
			Data.ratioTransfo = new Pair<float, float>(1f,1f);
		}

	}

	public float thresholdLC1(float t)
	{
		float tmp = InInput.controller.getLC1() - Mathf.Lerp(Data.extrLC1.X,Data.extrLC1.Y,t);
		if(tmp != 0)
			tmp /= Mathf.Abs(tmp);
		return tmp;
	}

	public float thresholdLC2(float t)
	{
		float tmp = InInput.controller.getLC2() - Mathf.Lerp(Data.extrLC2.X,Data.extrLC2.Y,t);
		if(tmp != 0)
			tmp /= Mathf.Abs(tmp);
		return tmp;
	}

	public float thresholdPress(float t)
	{
		float tmp = InInput.controller.getPressure() - Mathf.Lerp(Data.extrPress.X,Data.extrPress.Y,t);		
		if(tmp != 0)
			tmp /= Mathf.Abs(tmp);
		return tmp;
	}

}

public class RectScreen
{
	public Vector2 A{get;set;}
	public Vector2 B{get;set;}
	public Vector2 C{get;set;}
	public Vector2 D{get;set;}

	public RectScreen(){}

}