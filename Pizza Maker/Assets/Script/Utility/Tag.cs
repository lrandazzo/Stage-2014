using UnityEngine;
using System.Collections;
using System.Linq;

public class Tag : MonoBehaviour 
{
	public string Value = "";

	public static GameObject FindObjectWithTag(string stag)
	{
		var e = GameObject.FindObjectsOfType<Tag>().FirstOrDefault(g => g.Value == stag);
		return ((e == null) ? null : e.gameObject);
	}

	public static GameObject[] FindObjectsWithTag(string stag)
	{
		return (GameObject[])GameObject.FindObjectsOfType<Tag>().Where(g => g.Value == stag).Select(g => g.gameObject);
	}
}

