using UnityEngine;
using System.Collections;

public class SaveTrajectory : MonoBehaviour 
{

	void Update()
	{
		GameManager.Manager.Data.Trajectory.Add(new Pair<float, Pair<float, float>>(Time.time,new Pair<float, float>(transform.position.x,transform.position.y)));
	}

}
