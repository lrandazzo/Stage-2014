using UnityEngine;
using System.Collections;

public class InfoClickScript : MonoBehaviour 
{
	public Clickable cl = null;

	void Awake()
	{
		if(cl != null)
			cl.action += delegate {
			cl.gameObject.SetActive(false);
						};
		print(Application.persistentDataPath);
	}

}
