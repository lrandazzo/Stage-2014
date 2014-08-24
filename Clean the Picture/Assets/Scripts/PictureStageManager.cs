using UnityEngine;
using System.Collections;

public class PictureStageManager : MonoBehaviour 
{
	public AddToWorkspace atw = null;
	public SpriteRenderer sr = null;

	private bool toquit = false;

	void Awake()
	{
		if(atw == null)
			atw = GameObject.FindObjectOfType<AddToWorkspace>();
		if(sr != null)
			sr.enabled = false;
	}

	void Update()
	{
		if(atw != null && !atw.Active)
			atw.Active = true;
		
		if(toquit)
		{
			Quit();
			toquit = false;
		}

		if(Input.GetKeyDown("space"))
		{
			if(sr != null)
				sr.enabled = true;
			toquit = true;
		}

	}

	void Quit()
	{
		if(atw != null)
			atw.Active = false;
		GameManager.Manager.Data.WSpace.DrawSamples();
		GameManager.Manager.Data.Save("calib");
		GameManager.Manager.Data.RandomSamples(2000);
		Application.Quit();
	}

}
