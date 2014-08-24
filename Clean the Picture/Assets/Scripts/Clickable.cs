using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour 
{
	[Range(0,10)] public float timer;
	public bool loop;
	private float timed = 0;
	private Clicker pointer = null;
	private bool flag_clicked = false;
	private bool flag_clicksafe = false;
	// private bool flag_clicksafe{get{return true;}set{}}

	public delegate void Click(); 
	public event Click action;

	void Awake()
	{
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Clicker ptmp = other.GetComponent<Clicker>();
		if(ptmp != null)
			pointer = ptmp;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Clicker ptmp = other.GetComponent<Clicker>();
		if(ptmp == pointer)
			pointer = null;
	}

	void Update()
	{
		if(pointer != null && pointer.Activated && flag_clicksafe)
		{
			timed += Time.deltaTime;
			if(timed > timer && !flag_clicked)
			{
				action();
				if(loop)
					timed -= timer;
				else
					flag_clicked = true;
			}
		}
		else if(pointer != null && !pointer.Activated)
		{
			timed = 0;
			flag_clicked = false;
			flag_clicksafe = true;
		}
		else // pointer == null || ( pointer != null && pointer.Activated && !flag_clicksafe)
		{
			timed = 0;
			flag_clicked = false;
			flag_clicksafe = false;
		}
	}
	
}
