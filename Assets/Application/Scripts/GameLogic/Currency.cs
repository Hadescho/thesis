using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {
	
	public class Config
	{
		public float startCurrency = 1337;
		public float maxCurrency = 133700;
	}
	
	public static Config config = new Config();
	
	public static class Prices
	{
		public static float Tower1Price = 150;
		public static float Tower2Price = 100;
		public static float Tower3Price = 200;
		public static float Tower4Price = 300;
		public static float Tower5Price = 200;
		public static float Tower6Price = 400;
		public static float CannonTowerPrice = 300;
		public static float AuraTowerPrice = 500;
		public static float ShockTowerPrice = 250;
	}
	
	public static float currency;
	void Start () 
	{
		currency = config.startCurrency;
		UpdateCurrency();
	}
	
	
	public static bool Purchase(float cost)
	{
		if ((currency - cost) >= 0 )
		{
			currency -= cost;
			UpdateCurrency();
			Stats.AddSpentCurrency(cost);
			return true;
		}
		else
		{
			Debug.Log ("Not enough money to make this transaction!");
			UpdateCurrency();
			return false;
		}
		
	}
	
	public static bool Purchase(GameObject obj)
	{
		switch(obj.name)
		{
		case "Tower 1":
			return Purchase(Prices.Tower1Price);
		case "Tower 2":
			return Purchase(Prices.Tower2Price);
		case "Tower 3":
			return Purchase(Prices.Tower3Price);
		case "AuraTower":
			return Purchase(Prices.AuraTowerPrice);
		case "CannonTower":
			return Purchase(Prices.CannonTowerPrice);
		default:
			return false;
		}		
		
	}
	
	public static void GiveCurrency(float val)
	{
		if (currency + val <= config.maxCurrency)
		{
			currency += val;
			UpdateCurrency();
		}
	}
	
	private static void UpdateCurrency()
	{
		Game.Prototypes.HUD.Money.GetComponent<exSpriteFont>().text = currency.ToString();
	}
}
