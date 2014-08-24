using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour 
{

	void Update()
	{
		if(Input.GetKey("escape"))
			Quit();
	}

	void Quit()
	{
		GameManager.Manager.Data.Save("pizza");
		Application.Quit();
		print("Quit");
	}

}
