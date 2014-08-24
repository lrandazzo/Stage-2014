using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slot : MonoBehaviour 
{
	private delegate void DelegateName();
	private event DelegateName deleg; 
	public bool Unique = true;
	private bool isOccupied = false;
	public bool IsOccupied {get{return isOccupied;}}
	public List<Tetherable> Sloted = new List<Tetherable>();


	public void OnSlot(Tetherable other)
	{
		if(deleg != null)
			deleg();
		if(Unique)
			isOccupied = true;
		Sloted.Add(other);
	}

	public void OnUnSlot(Tetherable other)
	{
		if(Unique)
		{
			isOccupied = false;
		}
		Sloted.Remove(other);
	}

}
