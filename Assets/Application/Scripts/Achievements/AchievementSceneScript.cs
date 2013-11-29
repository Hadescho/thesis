using UnityEngine;
using System.Collections;

public class AchievementSceneScript : MonoBehaviour {

	void Start () 
	{
	
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			
			
			if(!Physics.Raycast(ray, out hit))
			{
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				if(hitObj.name == "ButtonExit")
				{
					Main.Load("Menu");
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Main.Load("Menu");
		}
	}
}
