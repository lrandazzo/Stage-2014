using UnityEngine;
using System.Collections;
using Extensions;

public class ResetScript : MonoBehaviour 
{
	public Clickable cl = null;

	void Awake()
	{
		if(cl == null)
			cl = gameObject.GetComponent<Clickable>();
		if(cl != null)
			cl.action += res;
	}

	void res()
	{
		this.DataLog("Restarting " + Application.loadedLevelName);
		Application.LoadLevel(Application.loadedLevelName);
	}

}
