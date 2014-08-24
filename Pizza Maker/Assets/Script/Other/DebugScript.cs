using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour 
{

	public GUIText text = null;
	public static string errormsg = "";

	void Awake()
	{
		if(text == null)
			text = gameObject.GetComponent<GUIText>();
		if(text == null)
			text = gameObject.AddComponent<GUIText>();
	}

	void Update()
	{
		if(Input.GetButtonDown("f1"))
			text.enabled = !text.enabled;

		text.text = "LC1: min " + 
			GameManager.Manager.Data.extrLC1.X.ToString() +
				" - max " +
				GameManager.Manager.Data.extrLC1.Y.ToString() + 
				" - current " + 
				InInput.controller.getLC1().ToString() + 
				"\n" + errormsg;
	}

}
