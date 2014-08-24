using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotator : MonoBehaviour 
{
	public List<Transform> ObjectList = new List<Transform>();
	public Transform Direction = null;
	private Vector3 baserot = new Vector3(0,0,0);
//	public Clickable NextArrow = null;
//	public Clickable PreviousArrow = null;
	private int currentObject;

	void Awake()
	{
//		NextArrow.action += Next;
//		PreviousArrow.action += Previous;3
		if(Direction != null)
		{
			Vector3 es = Direction.position - transform.position;
			baserot = es;
		}
		Vector3 ess = ObjectList[currentObject].position - transform.position;
		StartCoroutine("Turn",ess);
	}

	public void Next()
	{
		currentObject = (currentObject+1)%(ObjectList.Count);
		StopCoroutine("Turn");
		Vector3 ess = ObjectList[currentObject].position - transform.position;
		//StartCoroutine("Turn",Mathf.Sign(ess.x) * new Vector3(0,0,Vector3.Angle(new Vector3(0,1,0),ess)));
		StartCoroutine("Turn",ess);
	}

	public void Previous()
	{
		currentObject = (currentObject+ObjectList.Count-1)%(ObjectList.Count);
		StopCoroutine("Turn");
		Vector3 ess = ObjectList[currentObject].position - transform.position;
		StartCoroutine("Turn",ess);

	}

	IEnumerator Turn(Vector3 target)
	{
		Vector3 tmp = target;

		while(Vector3.Angle(tmp,baserot) > 0.05f)
		{
			Vector3 tmmmp = tmp;
			tmp = Vector3.RotateTowards(tmp,baserot,5f*Time.deltaTime,0);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,0,Mathf.Sign(Vector3.Cross(tmmmp,tmp).z) * Vector3.Angle(tmp,tmmmp)));
			foreach(var p in ObjectList)
				p.transform.rotation = Quaternion.identity;
			yield return null;
		}

		yield return null;

	}

}
