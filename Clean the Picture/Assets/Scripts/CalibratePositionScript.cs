using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CalibratePositionScript : MonoBehaviour 
{
	private Corner c;
	
	public Clickable sc = null;

	public SpriteRenderer sr = null;

	public Sprite press = null;
	public Sprite lift = null;

	public string LevelNext = "";

	void Awake()
	{
		c = Corner.UpperLeft;

		if(sr == null)
			sr = gameObject.GetComponent<SpriteRenderer>();
		if(sr == null)
			sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = press;

		sc.timer = 1f;
		sc.action += Calibrate;
		sc.transform.position = new Vector3(-8,4,0);

		try
		{
			GameManager.Manager.ResetCalibrationPosition();
		}
		catch
		{}
	}

	void Calibrate()
	{
		StartCoroutine(Cor());
	}

	IEnumerator Cor()
	{
		switch(c)
		{
		case Corner.UpperLeft:
			GameManager.Manager.touchedRect.A = new Vector2(InInput.controller.getTouchPosition().X, InInput.controller.getTouchPosition().Y);
			Vector3 tmp2 = Camera.main.WorldToScreenPoint(new Vector3(sc.transform.position.x, sc.transform.position.y,0));
			GameManager.Manager.trueRect.A = new Vector2(tmp2.x, tmp2.y);
			c = Corner.UpperRight;
			sc.transform.position = new Vector3(8,4,0);
			sc.gameObject.SetActive(false);
			sr.sprite = lift;
			StartCoroutine(WaitTillIsNotOnScreen(0.2f,WhenNotOnScreen));
			break;
		case Corner.UpperRight:
			GameManager.Manager.touchedRect.B = new Vector2(InInput.controller.getTouchPosition().X, InInput.controller.getTouchPosition().Y);
			Vector3 tmp22 = Camera.main.WorldToScreenPoint(new Vector3(sc.transform.position.x, sc.transform.position.y,0));
			GameManager.Manager.trueRect.B = new Vector2(tmp22.x, tmp22.y);
			c = Corner.BottomRight;
			sc.transform.position = new Vector3(8,-4,0);
			sc.gameObject.SetActive(false);
			sr.sprite = lift;
			StartCoroutine(WaitTillIsNotOnScreen(0.2f,WhenNotOnScreen));
			break;
		case Corner.BottomRight:
			GameManager.Manager.touchedRect.C = new Vector2(InInput.controller.getTouchPosition().X, InInput.controller.getTouchPosition().Y);
			Vector3 tmp222 = Camera.main.WorldToScreenPoint(new Vector3(sc.transform.position.x, sc.transform.position.y,0));
			GameManager.Manager.trueRect.C = new Vector2(tmp222.x, tmp222.y);
			c = Corner.BottomLeft;
			sc.transform.position = new Vector3(-8,-4,0);
			sc.gameObject.SetActive(false);
			sr.sprite = lift;
			StartCoroutine(WaitTillIsNotOnScreen(0.2f,WhenNotOnScreen));
			break;
		case Corner.BottomLeft:
			GameManager.Manager.touchedRect.D = new Vector2(InInput.controller.getTouchPosition().X, InInput.controller.getTouchPosition().Y);
			Vector3 tmp2222 = Camera.main.WorldToScreenPoint(new Vector3(sc.transform.position.x, sc.transform.position.y,0));
			GameManager.Manager.trueRect.D = new Vector2(tmp2222.x, tmp2222.y);
			GameManager.Manager.SetCalibrationPosition();
			sc.gameObject.SetActive(false);
			sr.sprite = lift;
			StartCoroutine(WaitTillIsNotOnScreen(0.2f,delegate {Application.LoadLevel(LevelNext);}));
			break;
		}

		yield return null;
	}

	IEnumerator WaitTillIsNotOnScreen(float seconds2wait, Action a)
	{
		double deb = DateTime.Now.Ticks;
		while((double)(seconds2wait * 10000000) > DateTime.Now.Ticks - deb)
		{
			if(InInput.controller.isOnGlass())
				deb = DateTime.Now.Ticks;
			yield return null;
		}
		a();
		yield return null;
	}

	void WhenNotOnScreen()
	{
		sc.gameObject.SetActive(true);
		sr.sprite = press;
	}
}

public enum Corner {UpperLeft, UpperRight, BottomRight, BottomLeft};

