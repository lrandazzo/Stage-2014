using UnityEngine;
using System.Collections;

public class Sprayable : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		Spray spr = other.gameObject.GetComponent<Spray>();
		if(spr != null)
		{
			spr.activate = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Spray spr = other.gameObject.GetComponent<Spray>();
		if(spr != null)
		{
			spr.activate = false;
		}
	}


}
