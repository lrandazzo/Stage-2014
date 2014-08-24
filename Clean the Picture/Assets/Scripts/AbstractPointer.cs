using UnityEngine;
using System.Collections;

public class AbstractPointer : MonoBehaviour 
{
	void Update()
	{
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(InInput.controller.getTouchPosition().X,InInput.controller.getTouchPosition().Y,10));
	}
}
