using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour 
{

	public Clickable nextButton = null;
	
	void Start()
	{
		if(nextButton == null)
			nextButton = gameObject.GetComponent<Clickable>();
		if(nextButton != null)
			nextButton.action += StartAction;
	}
	
	public void StartAction()
	{
		Application.LoadLevel(GameManager.Manager.NextLevel());
	}

}
