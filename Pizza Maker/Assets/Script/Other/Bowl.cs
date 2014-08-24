using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bowl : Tetherable 
{
	public List<Tetherable> ThingInBowl;
	[Range(1,10)]public int cpt = 3;

	public delegate void Empty(); 
	public event Empty onEmpty;

	public override Tetherable Tether(Transform t)
	{
		if(cpt > 0)
		{
			Vector3 ean = new Vector3(0,0,Random.Range (-30,30));
			Quaternion qu = new Quaternion();
			qu.eulerAngles = ean;
			Tetherable tmp = (Tetherable)Instantiate(ThingInBowl[Random.Range(0,ThingInBowl.Count)],transform.position,qu);
			tmp.Tether(t);
			cpt -= 1;
			if(cpt == 0 && onEmpty != null)
				onEmpty();
			return tmp;
		}
		else
			return null;
	}


}
