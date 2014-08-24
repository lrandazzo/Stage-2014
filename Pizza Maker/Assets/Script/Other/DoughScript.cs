using UnityEngine;
using System.Collections;

public class DoughScript : MonoBehaviour 
{
	public Rollable phase1 = null;
	public Rollable phase2 = null;
	public Vector3 rotate = new Vector3();

	void Awake()
	{
		Rollable[] rolllist = gameObject.GetComponents<Rollable>();

		if(rolllist.Length > 0)
			phase1 = rolllist[0];
		else
			phase1 = gameObject.AddComponent<Rollable>();

		if(rolllist.Length > 1)
			phase2 = rolllist[1];
		else
			phase2 = gameObject.AddComponent<Rollable>();

//		phase1.Activated = true;
//		phase2.Activated = false;
//
//		phase1.compl += EndPhase1;
//		phase2.compl += EndPhase2;


	}

	void EndPhase1()
	{
		StartCoroutine("Turn");

	}

	void EndPhase2()
	{
//		phase2.Activated = false;
	}

	IEnumerator Turn()
	{
//		phase1.Activated = false;
		Quaternion target = Quaternion.Euler(rotate);
		yield return new WaitForSeconds(0.5f);

		while(Mathf.Abs(Quaternion.Angle(transform.localRotation,target)) > 0.05f) 
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation,target,5f *Time.deltaTime);
			yield return null;
		}



//		phase2.Activated = true;
	}
}

