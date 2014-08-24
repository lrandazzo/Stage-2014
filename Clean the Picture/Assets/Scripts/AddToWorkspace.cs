using UnityEngine;
using System.Collections;

public class AddToWorkspace : MonoBehaviour 
{
	public bool Active = false;

	void Update()
	{
		if(Active && InInput.controller.isOnGlass() && GameManager.Manager.thresholdPress(0.2f) == 1)
			GameManager.Manager.Data.WSpace.Add(transform.position.x, transform.position.y);
	}
}
