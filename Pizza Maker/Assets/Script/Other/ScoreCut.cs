using UnityEngine;
using System.Collections;

public class ScoreCut : MonoBehaviour
{
	[Range(0,10000)]public int MaxScore = 100;
	[Range(0.00001f,5f)]public float ScoreDecay = 0.1f;
	[Range(0f,1f)]public float MinScore = 0.3f;
	private int tmpscore = 0;
	private bool cutting = false;

	public void BeginCut()
	{
		tmpscore = MaxScore;
		cutting = true;
	}

	void Update()
	{
		if(cutting)
		{
			tmpscore = (int)Mathf.Lerp(tmpscore,MaxScore*MinScore,Time.deltaTime * ScoreDecay);
		}
	}

	public void EndCut()
	{
		cutting = false;
		// print(tmpscore);
		GameManager.Manager.Data.CutScore += tmpscore;
	}

	public void AbortCut()
	{
		cutting = false;
		tmpscore = 0;
	}
}
