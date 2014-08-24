using UnityEngine;
using System.Collections;

public class MuteScript : MonoBehaviour 
{
	public AudioSource bgm = null;

	void Update()
	{
		if(Input.GetKeyDown("m") && bgm != null)
		{
			bgm.volume = 1f - bgm.volume;
		}
	}

}
