using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour
{

  public GameObject go;
  public float smoothing;
  public bool IsPop
  {
    get{return isPop;}
    set
      {
	isPop = value;

	StopCoroutine("Pop");
	StartCoroutine("Pop");
      }
  }

  private Vector3 target1;
  private Vector3 target2;
  private bool isPop = false;

  void Start ()
  {
    target1 = transform.position;
    target2 = new Vector3(go.transform.position.x, go.transform.position.y,0);
  }

  public IEnumerator Pop ()
  {
    Vector3 target;
    if(IsPop)
      target = target1;
    else
      target = target2;

    yield return null;

    while (Vector3.Distance(transform.position, target) > 0.05f) {
      transform.position = Vector3.Lerp (transform.position, target, smoothing * Time.deltaTime);

      yield return null;
    }	
  }

}
