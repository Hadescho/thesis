using UnityEngine;
using System.Collections;

public class ShopBehaviour : MonoBehaviour 
{
	public static GameObject money;
	
	void Awake()
	{
		money = GameObject.Find("/Requisition/RequisitionFont");
		money.GetComponent<exSpriteFont>().text=Requisition.GetRequisitionVal().ToString();
	}
	
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
}
