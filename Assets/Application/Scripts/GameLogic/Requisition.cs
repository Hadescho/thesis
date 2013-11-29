using UnityEngine;
using System.Collections;

public class Requisition {

	
	public static bool Spend(int price) //Used for purchases
	{
		if ((SiriusPrefs.I["Requsition"] - price) >= 0)
		{
			Debug.Log ("Requisition is being spent");
			SiriusPrefs.I["Requsition"] -= price;
			SiriusPrefs.Save ();
			return true;
		}
		else
		{
			Debug.Log ("Not enough requisition to make the transaction :" + SiriusPrefs.I["Requsition"].ToString());
			return false;
		}
	}
	
	public static void Recieve(int val)
	{
		SiriusPrefs.I["Requsition"] += val;
		SiriusPrefs.Save();
	}
	
	public static int GetRequisitionVal()
	{
		return SiriusPrefs.I["Requsition"];
	}
	
	void OnApplicationQuit()
	{
		Recieve(1000);
		SiriusPrefs.Save ();
	}
}
