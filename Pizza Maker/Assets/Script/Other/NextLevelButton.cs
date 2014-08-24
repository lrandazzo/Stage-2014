using UnityEngine;
using System.Collections;
using Extensions;

public class NextLevelButton : MonoBehaviour 
{
	public Clickable nextButton = null;
	
	void Start()
	{
		if(nextButton != null)
			nextButton.action += StartAction;
	}
	
	public void StartAction()
	{
		string lvl = GameManager.Manager.NextLevel();
		this.DataLog("Starting " + lvl);
		Application.LoadLevel(lvl);
	}


}
