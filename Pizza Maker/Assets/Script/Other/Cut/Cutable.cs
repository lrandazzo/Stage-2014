using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class Cutable : MonoBehaviour 
{
	public GameObject Part;
	public List<Sprite> LS;
	public Vector3 offsetpart;
	public ScoreCut sc = null;
	private int state = 0;
	private SpriteRenderer srperso;

	public delegate void Cuted(); 
	public event Cuted action;

	void Start()
	{
		ScoreCut sctmp = GameObject.FindObjectOfType<ScoreCut>();
		sc = gameObject.GetComponent<ScoreCut>() ?? gameObject.AddComponent<ScoreCut>();
		if(sctmp != null)
		{
			sc.MaxScore = sctmp.MaxScore;
			sc.ScoreDecay = sctmp.ScoreDecay;
			sc.MinScore = sctmp.MinScore;
		}
		srperso = gameObject.GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();
		if(LS.Count > 0)
			srperso.sprite = LS[0];
	}

	public void Cut()
	{
		if(state < LS.Count)
		{
			Vector2 r = Random.insideUnitCircle;
			Instantiate(Part,transform.position + offsetpart + (new Vector3(r.x,r.y,0))*0.2f,Quaternion.Euler(0,0,Random.Range (-30,30)));
//			transform.position += offsetself;
			state += 1;
			if(state == LS.Count)
			{
				Vector2 rr = Random.insideUnitCircle;
				Vector3 rean = new Vector3(0,0,Random.Range (-30,30));
				Quaternion rqu = new Quaternion();
				rqu.eulerAngles = rean;
				Instantiate(Part,transform.position + offsetpart + (new Vector3(rr.x,rr.y,0))*0.2f,rqu);
				srperso.sprite = null;
				Destroy(this.gameObject);
				action();
			}
			else
				srperso.sprite = LS[state];
		}			
		this.DataLog("Cutting " + gameObject.name);
		sc.EndCut();
	}
	
}
