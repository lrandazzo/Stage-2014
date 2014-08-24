using UnityEngine;
using System.Collections;

public class Erasable : MonoBehaviour 
{
	public SpriteRenderer rendere = null;

	void Awake()
	{
		if(rendere == null)
			rendere = gameObject.GetComponent<SpriteRenderer>();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Eraser e = other.gameObject.GetComponent<Eraser>();
		if(e != null && renderer != null && InInput.controller.isOnGlass())
		{
			Color ct = rendere.color;
			ct.a = Mathf.Max(0,rendere.color.a - e.quantity/(1+Vector3.Distance(gameObject.transform.position,e.gameObject.transform.position)));
			rendere.color = ct;
		}
	}

}
