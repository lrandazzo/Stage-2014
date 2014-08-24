using UnityEngine;
using System.Collections;

public class Saver : MonoBehaviour 
{

	void Update()
	{
		try
		{
			GameManager.Manager.Data.LC1.Add(new Pair<float, float>(Time.time,InInput.controller.getLC1()));
			GameManager.Manager.Data.LC2.Add(new Pair<float, float>(Time.time,InInput.controller.getLC2()));
			GameManager.Manager.Data.Pressure.Add(new Pair<float, float>(Time.time,InInput.controller.getPressure()));
			GameManager.Manager.Data.Rotation.Add(new Pair<float, Quaternion>(Time.time,InInput.controller.getRotation()));
		}
		catch
		{

		}
	}

}
