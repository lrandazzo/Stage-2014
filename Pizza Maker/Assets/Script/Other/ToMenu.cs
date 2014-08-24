using UnityEngine;
using System.Collections;
using Extensions;

public class ToMenu : MonoBehaviour 
{
	public Clickable button = null;

	void Start()
	{
		if(button != null)
			button.action += act;
	}

	public void act()
	{
		GameManager.Manager.ResetLevels();
		this.DataLog("Menu");
		Application.LoadLevel("Menu");
	}

}
