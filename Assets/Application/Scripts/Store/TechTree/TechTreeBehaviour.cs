using UnityEngine;
using System.Collections;

public class TechTreeBehaviour : MonoBehaviour {
	
	
	class Prices
	{
		
	}
	
	void Awake()
	{
		if (SiriusPrefs.I["Requsition"] == 0)
		{
			//Debug.Log("I'm giving you 133700 Requisition for test purporses");	
		}
		SiriusPrefs.B["Cannon"+"unlocked"] = true;
		SiriusPrefs.B["Cannon 2"+"unlocked"] = true;
		SiriusPrefs.B["Machinegun"+"unlocked"] = true;
		SiriusPrefs.B["Utility Tower"+"unlocked"] = true;
	}
	
	
	void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				if (hitObj.GetComponent<TechTreeNodeBehaviour>() != null)
				{
					Debug.Log("Unlockale: " + hitObj.GetComponent<TechTreeNodeBehaviour>().unlockable);
					Debug.Log("Unlocked: " + hitObj.GetComponent<TechTreeNodeBehaviour>().unlocked);
					if(hitObj.GetComponent<TechTreeNodeBehaviour>().unlocked)
					{
						// Do somthing when unlocked.
						Debug.Log ("This shit is unlocked!");
					}
					if(hitObj.GetComponent<TechTreeNodeBehaviour>().unlockable && !hitObj.GetComponent<TechTreeNodeBehaviour>().unlocked)
					{
						Debug.Log("shit");
						//Unlock
						hitObj.GetComponent<TechTreeNodeBehaviour>().Purchase();
					}
				}
			}
		}
	}
}
