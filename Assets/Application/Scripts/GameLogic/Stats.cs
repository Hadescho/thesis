using UnityEngine;
using System.Collections;

public class Stats
{
	//Currency
	public static void AddTotalCurrency(float val)
	{
		SiriusPrefs.F["totalCurrency"] += val;
	}
	
	public static void AddSpentCurrency(float val)
	{
		SiriusPrefs.F["totalCurrencySpent"] += val;
	}
	
	public static float GetTotalCurrency()
	{
		return SiriusPrefs.F["totalCurrency"];
	}
	
	public static float GetSpentCurrency()
	{
		return SiriusPrefs.F["totalCurrencySpent"];
	}
	//Time
	public static void AddTotalTime(float val)	
	{
		SiriusPrefs.F["totalTime"] += val;
	}
	
	public static float GetTotalTime()
	{
		return SiriusPrefs.F["totalTime"];
	}
	
	//Towers
	public static void AddTower1Built()
	{
		SiriusPrefs.F["tower1Built"] += 1;
	}
	
	public static float GetTower1Built()
	{
		return SiriusPrefs.F["tower1Built"];
	}
	
	public static void AddTower2Built()
	{
		SiriusPrefs.F["tower2Built"] += 1;
	}
	
	public static float GetTower2Built()
	{
		return SiriusPrefs.F["tower2Built"];
	}
	
	public static void AddTower3Built()
	{
		SiriusPrefs.F["tower3Built"] += 1;
	}
	
	public static float GetTower3Built()
	{
		return SiriusPrefs.F["tower3Built"];
	}
	
	public static void AddTower4Built()
	{
		SiriusPrefs.F["tower4Built"] += 1;
	}
	
	public static float GetTower4Built()
	{
		return SiriusPrefs.F["tower4Built"];
	}
	
	public static void AddTower5Built()
	{
		SiriusPrefs.F["tower5Built"] += 1;
	}
	
	public static float GetTower5Built()
	{
		return SiriusPrefs.F["tower1Built"];
	}
	
	public static void AddTower6Built()
	{
		SiriusPrefs.F["tower6Built"] += 1;
	}
	
	public static float GetTower6Built()
	{
		return SiriusPrefs.F["tower6Built"];
	}
	
	public static float GetTowerBuilt()
	{
		return (SiriusPrefs.F["tower6Built"] + SiriusPrefs.F["tower5Built"] + SiriusPrefs.F["tower4Built"] + SiriusPrefs.F["tower3Built"]+ SiriusPrefs.F["tower2Built"]+ SiriusPrefs.F["tower1Built"]);
	}
	
	//Victories and Defeats
	public static void AddVictory()
	{
		SiriusPrefs.I["totalVictories"] += 1;
	}
	
	public static void AddDefeat()
	{
		SiriusPrefs.I["totalDefeats"] += 1;
	}
	
	public static int GetVictories()
	{
		return SiriusPrefs.I["totalVictories"];
	}
	
	public static int GetDefeats()
	{
		return SiriusPrefs.I["totalDefeats"];
	}
	
	//Requisition
	
	public static void AddTotalRequisition(int val)
	{
		SiriusPrefs.I["totalRequisition"] += val;
	}
	
	public static void AddSpentRequisition(int val)
	{
		SiriusPrefs.I["spentRequisition"] += val;
	}
	
}
