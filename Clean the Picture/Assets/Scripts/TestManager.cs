using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour 
{
	public GameObject dot;
	public AddToWorkspace atw;
	private float tiem = 0;
	private bool done = false;

	void Awake()
	{
		atw.Active = true;
	}

	void Update()
	{
		if(tiem < 6f)
			tiem += Time.deltaTime;
		else if(done == false)
		{
			GameManager.Manager.Data.WSpace.DrawSamples();
			for(int i = 0; i<1000; i++)
			{
				atw.Active = false;
				Vector2 x = GameManager.Manager.Data.WSpace.Random();
				Instantiate(dot,new Vector3(x.x,x.y,0),Quaternion.identity);
			}
			done = true;
		}
	}


}
