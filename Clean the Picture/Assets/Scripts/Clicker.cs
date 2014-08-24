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
	
	void Update()
	{
		if(InInput.controller.isOnGlass())
			tmp += Time.deltaTime;
		else
			tmp = 0;
		if(tmp>lag)
			activated = true;
		else
			activated = false;
	}

}
