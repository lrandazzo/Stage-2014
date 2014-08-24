using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class GameData
{
	// Text Data
	public List<Pair<float,Pair<float,float>>> Trajectory{get;set;}
	public List<Pair<float,float>> LC1{get;set;}
	public List<Pair<float,float>> LC2{get;set;}
	public List<Pair<float,float>> Pressure{get;set;}
	public List<Pair<float,Quaternion>> Rotation{get;set;}

	// Calibration Data
	public Pair<float,float> ratioTransfo = new Pair<float, float>(1f,1f);
	public Vector2 bl = new Vector2(0,0);
	public Vector2 blref = new Vector2(0,0);
	
	public Workspace WSpace{get;set;}
	
	public Pair<float,float> extrLC1 = new Pair<float, float>(0,10f);
	public Pair<float,float> extrLC2 = new Pair<float, float>(0,10f);
	public Pair<float,float> extrPress = new Pair<float, float>(0,10f);

	// Score
	public float RollScore{get;set;}
	public float CutScore{get;set;}

	// Debug Log
	private string debug = "";
	private long initTime = 0;
	
	public GameData()
	{
		Trajectory = new List<Pair<float, Pair<float, float>>>();
		LC1 = new List<Pair<float, float>>();
		LC2 = new List<Pair<float, float>>();
		Pressure = new List<Pair<float, float>>();
		Rotation = new List<Pair<float, Quaternion>>();
		initTime = DateTime.Now.Ticks;

		if(File.Exists(Application.persistentDataPath + "/log"))
			File.Delete(Application.persistentDataPath + "/log");
		if(File.Exists(Application.persistentDataPath + "/data"))
			File.Delete(Application.persistentDataPath + "/data");
		if(File.Exists(Application.persistentDataPath + "/workspace"))
			File.Delete(Application.persistentDataPath + "/workspace");

		File.AppendAllText(Application.persistentDataPath + "/data","Time X Y LC1 LC2 Xangle Yangle Zangle SitarForce\r\n");

		Log("........Starting Game");
	}

	public void Log(string s)
	{
		debug += "["+((float)(DateTime.Now.Ticks - initTime)/10000000f).ToString() + "]" + s + "\r\n";
	}

	public void Dump()
	{
		// Data
		// Assumes that the lists are synchronized. Unknown behaviour otherwise
		string d = "";
		try
		{
			for(int i = 0; i< Trajectory.Count; i++)
			{
				Vector3 ang = Rotation[i].Y.eulerAngles;
				d += (Trajectory[i].X.ToString() + " " + 
				      Trajectory[i].Y.X.ToString() + " " + 
				      Trajectory[i].Y.Y.ToString() + " " + 
				      LC1[i].Y.ToString() + " " + 
				      LC2[i].Y.ToString() + " " + 
				      ang.x + " " +
				      ang.y + " " + 
				      ang.z + " " + 
				      Pressure[i].Y.ToString()+"\r\n");
			}
		}
		catch
		{
			
		}
		File.AppendAllText(Application.persistentDataPath + "/data",d);
		
		// Debug
		File.AppendAllText(Application.persistentDataPath + "/log",debug);

		// Reset
		Trajectory = new List<Pair<float, Pair<float, float>>>();
		LC1 = new List<Pair<float, float>>();
		LC2 = new List<Pair<float, float>>();
		Pressure = new List<Pair<float, float>>();
		Rotation = new List<Pair<float, Quaternion>>();
		debug = "";
	}

	public void RandomSamples(int number)
	{
		string d = "X Y\r\n";
		for(int i = 0;i<number;i++)
		{
			Vector2 v = WSpace.Random();
			d += v.x.ToString() + " " + v.y.ToString() + "\r\n";
		}
		File.AppendAllText(Application.persistentDataPath + "/workspace",d);

	}

	public void Save(string s)
	{
		BinaryFormatter bf = new BinaryFormatter();

		Dump();

		// Save Binary
		FileStream file = File.Create(Application.persistentDataPath + "/" + s);
		SubGameData sgd = new SubGameData();

		sgd.ratioTransfo = ratioTransfo;
		sgd.bl = new Pair<float,float>(bl.x,bl.y);
		sgd.blref = new Pair<float,float>(blref.x,blref.y);
		sgd.WSpace = new Workspace(WSpace);
		sgd.extrLC1 = extrLC1;
		sgd.extrLC2 = extrLC2;
		sgd.extrPress = extrPress;

		bf.Serialize(file,sgd);
		file.Close();
		
	}

	public void Load(string s)
	{
		if(File.Exists(Application.persistentDataPath + "/" + s))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + s,FileMode.Open);
			SubGameData gd = (SubGameData)bf.Deserialize(file);
			file.Close();

			ratioTransfo = gd.ratioTransfo;
			bl = new Vector2(gd.bl.X,gd.bl.Y);
			blref = new Vector2(gd.blref.X,gd.blref.Y); 
			WSpace = new Workspace(gd.WSpace);
			extrLC1 = gd.extrLC1;
			extrLC2 = gd.extrLC2;
			extrPress = gd.extrPress;
			this.DataLog("Loading " + s);
		}

	}
	
}

[Serializable]
public class SubGameData
{
	public Pair<float,float> ratioTransfo = new Pair<float, float>(1f,1f);
	public Pair<float,float> bl = new Pair<float,float>(0,0);
	public Pair<float,float> blref = new Pair<float,float>(0,0);
	
	public Workspace WSpace{get;set;}
	
	public Pair<float,float> extrLC1 = new Pair<float, float>(0,10f);
	public Pair<float,float> extrLC2 = new Pair<float, float>(0,10f);
	public Pair<float,float> extrPress = new Pair<float, float>(0,10f);
	
	public SubGameData()
	{}
}