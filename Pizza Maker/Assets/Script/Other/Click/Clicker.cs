using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour 
{
	public bool Activated
	{
		get
		{return activated;}
	}
	private bool activated = false;
	[Range(0,10)] public float lag = 0;
	private float tmp = 0;
	public bool isOnButton = false;
	
	void Update()
	{
		if(InInput.controller.isOnGlass() && GameManager.Manager.thresholdPress(0.2f) == 1)
		//if(InInput.controller.isOnGlass())
			tmp += Time.deltaTime;
		else
		{
			isOnButton = false;
			tmp = 0;
		}
		if(tmp>lag)
			activated = true;
		else
			activated = false;
	}

}
