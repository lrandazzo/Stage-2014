using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;

public class CutManagerScript : MonoBehaviour 
{
	public List<Cutable> Ingredients = new List<Cutable>();
	public Transform lookat = null;
	[Range(1,128)]public int randomCount = 1;
	private int counter = 0;

	void Awake()
	{
		Vector2 ve = GameManager.Manager.Data.WSpace.Random(randomCount);
		Vector3 ve3 = new Vector3(ve.x,ve.y,0);
		Cutable c = (Cutable)Instantiate(Ingredients[0],ve3,Quaternion.identity);
		c.action += OnFinished;
		Vector3 tev = lookat.transform.position - c.transform.position;
		c.transform.Rotate(new Vector3(0,0,Vector3.Angle(new Vector3(0,1f,0),tev) * Mathf.Sign(Vector3.Cross(new Vector3(0,1f,0),tev).z)));
	}

	void OnFinished()
	{
		counter += 1;
		if(counter < Ingredients.Count)
		{
			Vector2 ve = GameManager.Manager.Data.WSpace.Random(randomCount);
			Vector3 ve3 = new Vector3(ve.x,ve.y,0);
			Cutable c = (Cutable)Instantiate(Ingredients[counter],ve3,Quaternion.identity);
            c.action += OnFinished;
			Vector3 tev = lookat.transform.position - c.transform.position;
			c.transform.Rotate(new Vector3(0,0,Vector3.Angle(new Vector3(0,1f,0),tev) * Mathf.Sign(Vector3.Cross(new Vector3(0,1f,0),tev).z)));
		}
	}

}
