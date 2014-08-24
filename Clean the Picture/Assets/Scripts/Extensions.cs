using UnityEngine;
using System.Collections;

namespace Extensions
{
	public static class ExtClass
	{
		public static void DataLog(this object o, string s)
		{
			if(GameManager.Manager != null)
				GameManager.Manager.Data.Log("["+o.GetType().ToString()+"] "+s);
		}
	}

}
