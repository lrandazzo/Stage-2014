using UnityEngine;
using System.Collections;

public abstract class InInput : MonoBehaviour
{
  public static InInput controller;

  void OnEnable()
  {
    if(controller == null)
      {
	controller = this;
	DontDestroyOnLoad(gameObject);
      }
    else if(controller != this)
      this.enabled = false;
  }

  public abstract float getPressure();
  public abstract float getLC1();
  public abstract float getLC2();
  public abstract Pair<float,float> getTouchPosition();
  public abstract bool isOnGlass();
	public abstract Quaternion getRotation();
  
	public abstract void UpdatePacket();

}
