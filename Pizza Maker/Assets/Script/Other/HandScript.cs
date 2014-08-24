using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour 
{
	private SpriteRenderer sr = null;
	public Clicker cas = null;
	public Sprite opened = null;
	public Sprite closed = null;


	void Awake()
	{
		sr = gameObject.GetComponent<SpriteRenderer>();
		if(sr == null)
			sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = opened;
	}

	void Update()
	{
		if(GameManager.Manager.thresholdLC1(0.7f) > 0)
			sr.sprite = closed;
		else if(GameManager.Manager.thresholdLC1(0.4f) < 0)
			sr.sprite = opened;
		if(cas.isOnButton && InInput.controller.isOnGlass())
			sr.color = Color.yellow;
		else if(InInput.controller.isOnGlass())
			sr.color = Color.red;
		else
			sr.color = Color.black;
	}
}
