using UnityEngine;
using System.Collections;
using System;

public class DeactivatePointerScript : MonoBehaviour 
{
	private AbstractPointer ap = null;


	void Awake()
	{
		ap = GameObject.FindObjectOfType<AbstractPointer>();
		ap.gameObject.GetComponent<CircleCollider2D>().enabled = false;
		StartCoroutine(aux());
	}

	IEnumerator aux()
	{
		double tmp = DateTime.Now.Ticks;
		while(DateTime.Now.Ticks < tmp + 10000000)
			yield return null;
		ap.gameObject.GetComponent<CircleCollider2D>().enabled = true;
	}
}
