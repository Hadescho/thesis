using UnityEngine;
using System.Collections;

public class SkillStoreBehaviour : MonoBehaviour {
	
	public class Config
	{
		public int orbitalStrikePrice = 1000;
		public int airStrikePrice = 1000;
		public int ArtilleryStrikePrice = 1000;
		public int EMPPrice = 1000;
		public int EnergyDrainPrice = 1000;
		public int AirDropPrice = 1000;
		public int TimeshiftPrice = 1000;
		public int WarbannerPrice = 1000;
	}
	
	public static Config config = new Config();
	
	void Awake()
	{
		gameObject.FindChild("AirDrop/Price").GetComponent<exSpriteFont>().text=config.AirDropPrice.ToString();
		gameObject.FindChild("AirStrike/Price").GetComponent<exSpriteFont>().text=config.airStrikePrice.ToString();
		gameObject.FindChild("ArtileryStrike/Price").GetComponent<exSpriteFont>().text=config.ArtilleryStrikePrice.ToString();
		gameObject.FindChild("Banner/Price").GetComponent<exSpriteFont>().text=config.WarbannerPrice.ToString();
		gameObject.FindChild("EMP/Price").GetComponent<exSpriteFont>().text=config.EMPPrice.ToString();
		gameObject.FindChild("EnergyDrain/Price").GetComponent<exSpriteFont>().text=config.EnergyDrainPrice.ToString();
		gameObject.FindChild("OrbitalStrike/Price").GetComponent<exSpriteFont>().text=config.orbitalStrikePrice.ToString();
		gameObject.FindChild("Timeshift/Price").GetComponent<exSpriteFont>().text=config.TimeshiftPrice.ToString();
	}
	
	void Start ()
	{
	
	}
	
	void Update ()
	{
		SelectCheck();
	}
	
	public void SelectCheck()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit))
			{
				if (hit.transform.gameObject.name == "Buy")
				{
					GameObject tempBuy = hit.transform.gameObject.GetParent();
					string price = tempBuy.FindChild("Price").GetComponent<exSpriteFont>().text;
					int priceToInt = int.Parse(price);
					if(!SiriusPrefs.B["Store"+tempBuy.name])
					{
						Requisition.Spend(priceToInt);
						SiriusPrefs.B["Store"+tempBuy.name]=true;
					}
				}
			}
		}
	}
}
