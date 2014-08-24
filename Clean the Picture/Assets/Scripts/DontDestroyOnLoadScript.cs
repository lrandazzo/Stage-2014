using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadScript : MonoBehaviour 
{

  void Start()
  {
		DontDestroyOnLoad(gameObject);
  }

}
