using UnityEngine;
using System.Collections;
using System.Linq;

public class GoToThisAndDieScript : MonoBehaviour 
{
	public string TagOfTarget = "";
	private Vector3 target = new Vector3();
	private Vector3 initPos = new Vector3();

	void Awake()
	{
		GameObject ee = Tag.FindObjectWithTag(TagOfTarget);
		if(ee != null)
		{
			target = ee.transform.position;
			initPos = transform.position;
			StartCoroutine("MoveCoroutine");
		}
	}

	IEnumerator MoveCoroutine()
	{
		float cof = 0;
		while(cof < 1f)
		{
			cof += Time.deltaTime;
			transform.position = Vector3.Lerp(initPos,target,cof);
			yield return null;
		}
		Destroy(this.gameObject);
	}
}
