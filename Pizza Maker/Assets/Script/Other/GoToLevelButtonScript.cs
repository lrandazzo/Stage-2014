using UnityEngine;
using System.Collections;

public class GoToLevelButtonScript : MonoBehaviour 
{
	public string NextLevel = "";
	public Clickable cl = null;

	void Awake()
	{
		if(cl == null)
			cl = gameObject.GetComponent<Clickable>();
		if(cl == null)
			cl = gameObject.AddComponent<Clickable>();
		cl.action += Act;
	}

	void Act()
	{
		Application.LoadLevel(NextLevel);
	}
}
