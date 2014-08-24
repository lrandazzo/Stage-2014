using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spray : MonoBehaviour 
{
	public List<GameObject> Splashes;
	private Tetherable thistether;
	public float Frequency
	{
		get
		{
			return frequency;
		}
		set
		{
			if(frequency == 0)
				tmptime = Time.time;
			frequency = value;
		}
	}
	private float tmptime;
	private float frequency;
	public float spread;
	public bool activate = false;
	public int maxInstances = 300;
	private int instances = 0;

	public delegate void Empty(); 
	public event Empty onEmpty;

	void Start()
	{
		Frequency = 0;
		thistether = gameObject.GetComponent<Tetherable>();
		if(thistether == null)
			thistether = gameObject.AddComponent<Tetherable>();
	}

	void Update()
	{
		float t = Time.time;
		if(Frequency > 0 && activate && InInput.controller.isOnGlass() && thistether.Tethered != null && thistether.Tethered.gameObject.GetComponent<AbstractPointer>() != null && maxInstances > instances)
		{
			while(tmptime + frequency < t)
			{
				tmptime += frequency;
				Vector3 u = Random.insideUnitSphere;
				Vector3 ean = new Vector3(0,0,Random.Range (-30,30));
				Quaternion qu = new Quaternion();
				qu.eulerAngles = ean;
				Instantiate(Splashes[Random.Range(0,Splashes.Count)],transform.position + new Vector3(spread * u.x, spread * u.y,0),qu);
				instances += 1;
			}
			if(instances >= maxInstances && onEmpty != null)
				onEmpty();
		}
		else
			tmptime = Time.time;

		float tmprat = 0;
		if(GameManager.Manager.Data.extrLC1.Y != GameManager.Manager.Data.extrLC1.X)
			tmprat = (InInput.controller.getLC1()-GameManager.Manager.Data.extrLC1.X)/(GameManager.Manager.Data.extrLC1.Y-GameManager.Manager.Data.extrLC1.X);
		else
			tmprat = 0.5f;
		tmprat = Mathf.Max(0,tmprat);
		tmprat = Mathf.Min(1f,tmprat);
		tmprat *= 9f;
		tmprat += 1f;
		if(InInput.controller.isOnGlass())
			Frequency = 0.2f/tmprat;
		else
			Frequency = 0;
	}

}
