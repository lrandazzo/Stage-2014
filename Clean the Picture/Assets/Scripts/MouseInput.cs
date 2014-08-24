using UnityEngine;
using System.Collections;

public class MouseInput : InInput
{

  void Start()
  {
  }

  public override float getPressure()
  {
		return Input.GetAxis("Fire3")*10f;
  }

  public override float getLC1()
  {
    return Input.GetAxis("Fire1")*10f;
  }

  public override float getLC2()
  {
    return Input.GetAxis("Fire2")*10f;
  }

  public override Pair<float,float> getTouchPosition()
  {
		return new Pair<float,float>((Input.mousePosition.x - GameManager.Manager.Data.bl.x) * GameManager.Manager.Data.ratioTransfo.X + GameManager.Manager.Data.blref.x
		                             ,(Input.mousePosition.y - GameManager.Manager.Data.bl.y)* GameManager.Manager.Data.ratioTransfo.Y + GameManager.Manager.Data.blref.y);
  }

  public override bool isOnGlass()
  {
    return Input.GetAxis("Fire3")>0.05f;
  }

  public override Quaternion getRotation()
  {
		return Quaternion.identity;
  }

	public override void UpdatePacket ()
	{
	}

}
