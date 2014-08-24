using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour 
{
	private List<LevelSlot> ls = new List<LevelSlot>();
	public Clickable SaveButton = null;
	public Clickable BackButton = null;

	public static string[] RegularLevels = new string[3] {"cut","roll","final"};
	public static string LastLevel = "end";

	void Awake()
	{
		ls = GameObject.FindObjectsOfType<LevelSlot>().ToList();
		SaveButton.action += Save;
		BackButton.action += Cancel;
	}

	public void Save()
	{
		ls.Sort(delegate(LevelSlot s1, LevelSlot s2){return s1.LevelNumber - s2.LevelNumber;});
		var tls = ls.FindAll(s => s.GetToken() != null);
		GameManager.Manager.Levels = tls.ConvertAll(s => s.GetToken().Level);
		Application.LoadLevel("Menu");
	}

	public void Cancel()
	{
		Application.LoadLevel("Menu");
	}

}
