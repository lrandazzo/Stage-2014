using UnityEngine;
using System.Collections;
using Extensions;

public class Tetherable : MonoBehaviour
{

	private Transform tethered;
	public Transform Tethered{get{return tethered;}}
	public bool slotable = false;
	private Slot activeSlot = null;
	public Slot ActiveSlot
	{
		get
		{
			return activeSlot;
		}
		set
		{
			if(activeSlot != value)
			{
				if(activeSlot != null)
					activeSlot.OnUnSlot(this);
				activeSlot = value;
				if(activeSlot != null)
					activeSlot.OnSlot(this);
			}
		}
	}


	public virtual Tetherable Tether(Transform t)
	{
		tethered = t;
		this.DataLog("Pick " + this.gameObject.name + " at (" + this.transform.position.x.ToString() + "," + this.transform.position.y.ToString() + ")");
		return this;
	}

	public virtual void Untether()
	{
		this.DataLog("Drop " + this.gameObject.name + " at (" + this.transform.position.x.ToString() + "," + this.transform.position.y.ToString() + ")");
		tethered = null;
	}

	void Update()
	{
		if(tethered != null && (!slotable || activeSlot == null))
			transform.position = tethered.position;
		else if(slotable && activeSlot != null)
			transform.position = activeSlot.transform.position;
	}

}
