using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class Rollable : MonoBehaviour 
{
	[Range(0,10)] public float rollCoeffH = 0.1f;
	[Range(0,10)] public float rollCoeffV = 0.3f;
	[Range(1,10)] public float rollMax = 3f;
	public Sprite Finish = null;

	private bool activated = true;
	private float rolledH = 1;
	private float rolledV = 1;
	private int spritecount;
	//private int currentSprite = 0;
	private SpriteRenderer sr = null;
	private Rolling roller = null;
	private Vector3 lastroll = new Vector3();

//	public delegate void OnCompletion();
//	public event OnCompletion compl;

	void Awake()
	{
		if(sr == null)
			sr = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if(roller != null && activated)
		{
			float amountH = rollCoeffH * InInput.controller.getPressure() * Mathf.Abs(Vector3.Dot(roller.transform.position - lastroll,new Vector3(0,1,0)))/30f;	
			float amountV = rollCoeffV * InInput.controller.getPressure() * Mathf.Abs(Vector3.Dot(roller.transform.position - lastroll,new Vector3(1,0,0)))/30f;
			GameManager.Manager.Data.RollScore += (int)((amountH+1)*(amountH)); 
			GameManager.Manager.Data.RollScore += (int)((amountV+1)*(amountV)); 
			rolledH = Mathf.Min(rollMax,rolledH+amountH);
			rolledV = Mathf.Min(rollMax,rolledV+amountV);
			transform.localScale = new Vector3(rolledV,rolledH,1f);
			if(rolledH == rollMax && rolledV == rollMax)
			{
				activated = false;
				sr.sprite = Finish;
				transform.localScale = new Vector3(1f,1f,1f);
				this.DataLog("Finished rolling");
			}

			lastroll = roller.transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		roller = other.gameObject.GetComponent<Rolling>();
		if(roller != null)
		{
			lastroll = roller.transform.position;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(roller == null)
		{
			roller = other.gameObject.GetComponent<Rolling>();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<Rolling>() == roller)
		{
			roller = null;
		}
	}




}
