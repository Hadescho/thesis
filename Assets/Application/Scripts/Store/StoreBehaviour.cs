using UnityEngine;
using System.Collections;

public class StoreBehaviour : MonoBehaviour {
	
	public GameObject requisitionVal;
	
	void Start () 
	{
		requisitionVal = GameObject.Find("/Requisition/RequisitionFont");
		requisitionVal.GetComponent<exSpriteFont>().text = Requisition.GetRequisitionVal().ToString();
	
	}
	
	void Update () 
	{
		requisitionVal.GetComponent<exSpriteFont>().text = Requisition.GetRequisitionVal().ToString();
		if(Input.GetMouseButtonUp(0))
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
				/*if(hitObj.name == "Techtree")
				{
					Shop.SetActive(false);	
				}*/
				
				
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Main.Load("Menu");
		}
	}
}
