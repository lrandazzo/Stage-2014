using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SitarInput : InInput
{
	private Quaternion rot = Quaternion.identity;

	private long dt = 0;
	private long curtime = 0;

	public void resetAngles()
	{
		rot = Quaternion.identity;
	}

	void Start()
	{
		curtime = DateTime.Now.Ticks;
	}


	public override void UpdatePacket() // In ReceiveUDP Thread
	{
		dt = DateTime.Now.Ticks - curtime;
		curtime = DateTime.Now.Ticks;

		float xangle = ((float)ReceiveUDP.SitarData.object1Gyr[0] - GameManager.Manager.xangavg) * dt / 10000000;
		float yangle = ((float)ReceiveUDP.SitarData.object1Gyr[1] - GameManager.Manager.yangavg) * dt / 10000000;
		float zangle = ((float)ReceiveUDP.SitarData.object1Gyr[2] - GameManager.Manager.zangavg) * dt / 10000000;

		rot *= Quaternion.Euler(xangle,zangle,yangle);

		GameManager.Manager.xangset.Add((float)ReceiveUDP.SitarData.object1Gyr[0]);
		GameManager.Manager.yangset.Add((float)ReceiveUDP.SitarData.object1Gyr[1]);
		GameManager.Manager.zangset.Add((float)ReceiveUDP.SitarData.object1Gyr[2]);
		GameManager.Manager.lc1set.Add(getLC1());
		GameManager.Manager.lc2set.Add(getLC2());
		GameManager.Manager.pressset.Add(getPressure());

	}

	public override float getPressure()
	{
		return (float)ReceiveUDP.SitarData.touchForce;
	}

	public override float getLC1()
	{
		return (float)ReceiveUDP.SitarData.object1ADC[1];
	}

	public override float getLC2()
	{
		return (float)ReceiveUDP.SitarData.object1ADC[0];
	}

	public override Pair<float,float> getTouchPosition()
	{
		return new Pair<float,float>(((float)ReceiveUDP.SitarData.touchPositionPix[0] - GameManager.Manager.Data.bl.x) * GameManager.Manager.Data.ratioTransfo.X + GameManager.Manager.Data.blref.x,
		                             ((float)ReceiveUDP.SitarData.touchPositionPix[1] - GameManager.Manager.Data.bl.y) * GameManager.Manager.Data.ratioTransfo.Y + GameManager.Manager.Data.blref.y);
	}

	public override bool isOnGlass()
	{
		//return ReceiveUDP.SitarData.object1Present == 1;
		return ReceiveUDP.SitarData.touched == 1;
	}

	public override Quaternion getRotation()
	{
		return rot;
	}

}
