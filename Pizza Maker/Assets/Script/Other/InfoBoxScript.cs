using UnityEngine;
using System.Collections;

public class InfoBoxScript : MonoBehaviour 
{

	public Clickable TriggerInfo = null;
	public Clickable Info = null;

	void Awake()
	{
		Info.action += delegate {
			Info.gameObject.SetActive(false);
				};
		TriggerInfo.action+= delegate {
			Info.gameObject.SetActive(true);
				};
	}

}
