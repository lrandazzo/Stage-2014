using UnityEngine;
using System.Collections;
using Extensions;

public class MenuManagerScript : MonoBehaviour 
{
	public Clickable startButton = null;
	public Clickable levelButton = null;

	void Start()
	{
		if(startButton != null)
			startButton.action += StartAction;
		if(levelButton != null)
			levelButton.action += LevelSelectAction;
		YoloScript.Load("../Clean the Picture/calib");

	}

	public void StartAction()
	{
		this.DataLog("Start");
		string lvl = GameManager.Manager.NextLevel();
		this.DataLog("Starting " + lvl);
		Application.LoadLevel(lvl);
	}

	public void LevelSelectAction()
	{
		this.DataLog("Level Select");
		Application.LoadLevel ("levelselect");
	}

	void Update()
	{
		if(Input.GetButtonDown("tab"))
		{
			this.DataLog("Calibration");
			Application.LoadLevel("choosecalib");
		}
	}

}
