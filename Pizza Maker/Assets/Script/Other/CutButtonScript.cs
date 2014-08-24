using UnityEngine;
using System.Collections;

public class CutButtonScript : MonoBehaviour 
{

	public Vector3 targetPosition = new Vector3();
	private Clickable button = null;
	public Cutable item = null;
	public static Cutable currentItem = null;
	public static Vector3 exitPosition = new Vector3(20,0,0);

	void Awake()
	{
		button = gameObject.GetComponent<Clickable>();
		if(button == null)
			button = gameObject.AddComponent<Clickable>();
		button.action += createItem;

	}

	void createItem()
	{
		Cutable tmp = (Cutable)Instantiate(item,targetPosition,Quaternion.identity);
		//StopCoroutine("moveIt");
		//StartCoroutine("moveIt",tmp);
		if(currentItem != null)
			StartCoroutine("moveAndKillIt",currentItem);
		currentItem = tmp;
	}

	IEnumerator moveIt(Cutable it)
	{
		while(Vector3.Distance(it.transform.position,targetPosition) > 0.05f)
		{
			it.transform.position = Vector3.Lerp(it.transform.position,targetPosition,3f*Time.deltaTime);
			yield return null;
		}			                                  
	}

	IEnumerator moveAndKillIt(Cutable it)
	{
		while(Vector3.Distance(it.transform.position,exitPosition) > 0.05f)
		{
			it.transform.position = Vector3.Lerp(it.transform.position,exitPosition,3f*Time.deltaTime);
			yield return null;
		}	
		Destroy(it.gameObject);
	}


}
