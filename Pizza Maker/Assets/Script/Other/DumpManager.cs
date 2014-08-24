using UnityEngine;
using System.Collections;

public class DumpManager : MonoBehaviour 
{
	private float temp = 0;
	void Update()
	{
		if(Time.time - temp > 2)
		{
			GameManager.Manager.Data.Dump();
			temp = Time.time;
		}
	}

}
