using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tethering : MonoBehaviour 
{

	//private bool onglass = false;
	private List<Tetherable> under = new List<Tetherable>();
	private Tetherable tethered = null;
	private List<Slot> slots = new List<Slot>();
	private float tmp = 0f;
	
	
	void Update()
	{
		if(!InInput.controller.isOnGlass())
			tmp = Time.time;

		if(Time.time - tmp > 0.1f && GameManager.Manager.thresholdLC1(0.7f) > 0 && under.Count > 0 && tethered == null)
		{
			tethered = under[under.Count - 1].Tether(gameObject.transform);
		}
		if(GameManager.Manager.thresholdLC1(0.4f) < 0 && tethered != null)
		{
			tethered.Untether();
			tethered = null;
		}

		if(tethered != null)
		{
			var availableSlots = slots.FindAll(sl => !sl.IsOccupied || sl.Sloted.Contains(tethered)); // if sl.Unique, IsOccupied == true iff the slot is actually occupied, else IsOccupied is always false
			if(availableSlots.Count > 0)
				tethered.ActiveSlot = availableSlots[0];
			else
				tethered.ActiveSlot = null;
		}

	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Tetherable teth = other.gameObject.GetComponent<Tetherable>();
		if(teth != null)
			under.Add(teth);
		Slot sl = other.gameObject.GetComponent<Slot>();
		if(sl != null)
			slots.Add(sl);
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		Tetherable teth = other.gameObject.GetComponent<Tetherable>();
		if(teth != null)
			under.Remove(teth);
		Slot sl = other.gameObject.GetComponent<Slot>();
		if(sl != null)
			slots.Remove(sl);
	}
}
