using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cutter : MonoBehaviour 
{
	private bool activate;
	public bool Activate
	{
		get
		{
			return activate;
		}
		set
		{
			if(activate != value)
			{
				if(activate)
				{
					foreach(var p in clcutting)
					{
						if(!clcancut.Contains(p))
						{
							clcancut.Add(p);
							p.sc.AbortCut();
						}
					}
					clcutting.Clear();
				}
				else
				{
					foreach(var p in clcancut)
					{
						if(!clcutting.Contains(p))
						{
							clcutting.Add(p);
							p.sc.BeginCut();
						}
					}
					clcancut.Clear();
				}

			}
			activate = value;
		}
	}
	private List<Cutable> clcutting = new List<Cutable>();
	private List<Cutable> clcancut = new List<Cutable>();

	void OnTriggerEnter2D(Collider2D other)
	{
		Cutable tmp = other.gameObject.GetComponent<Cutable>();
		if(tmp != null)
		{
			if(Activate && !clcutting.Contains(tmp))
			{
				clcutting.Add(tmp);
				tmp.sc.BeginCut();
			}
			else if(!Activate && !clcancut.Contains(tmp))
				clcancut.Add(tmp);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Cutable tmp = other.gameObject.GetComponent<Cutable>();
		if(tmp != null)
		{
			if(Activate && clcutting.Contains(tmp))
			{
				clcutting.Remove(tmp);
				tmp.Cut();
			}
			else if(!Activate && clcancut.Contains(tmp))
				clcancut.Remove(tmp);
		}
	}

	void Update()
	{
		Activate = InInput.controller.isOnGlass();
	}



}
