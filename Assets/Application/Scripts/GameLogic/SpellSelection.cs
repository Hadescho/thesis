using UnityEngine;
using System.Collections;

public class SpellSelection : MonoBehaviour {
	
	public class Config
	{
		public float orbitalStrikeEnergyCost=10.0f;
		public float airDropEnergyCost=10.0f;
		public float airStrikeEnergyCost=90.0f;
		public float artilleryEnergyCost=10.0f;
		public float energyDrainEnergyCost=5.0f;
		public float EMPEnergyCost=10.0f;
		public float WarbannerEnergyCost=10.0f;
	}
	
	private static bool _blocked;
	
	public static Config config = new Config();	
	
	void Start ()
	{
		_blocked = false;
	}
	
	
	void Update ()
	{
		BlockTowerSpawn();
		Check();
	}
	
	public void Check()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
		
			if(!Physics.Raycast(ray, out hit))
			{
			}
			else
			{	
				if (!_blocked)
				{
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.OrbitalStrikeIcon.name)
					{
						if(OrbitalStrikeBehaviour.cooldown<0)
						{
							if(Energy.UseEnergy(config.orbitalStrikeEnergyCost))
							{
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.OrbitalStrikeIcon.name).SetSprite(Game.Prototypes.Extras.OrbitalStrikeIcon.name+"Clicked");
								OrbitalStrikeBehaviour.played=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.AirDropIcon.name)
					{
						if(AirDropBehaviour.cooldown<0)
						{
							if(Energy.UseEnergy(config.airDropEnergyCost))
							{
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.AirDropIcon.name).SetSprite(Game.Prototypes.Extras.AirDropIcon.name+"Clicked");
								AirDropBehaviour.played=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.AirStrike.name && !AirStrikeBehaviour.blockSecondCast)
					{
						if(AirStrikeBehaviour.cooldown<0)
						{
							if(Energy.UseEnergy(config.airStrikeEnergyCost))
							{
								
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.AirStrike.name).SetSprite(Game.Prototypes.Extras.AirStrike.name+"Clicked");
								AirStrikeBehaviour.playedAir=true;
								AirStrikeBehaviour.blockSecondCast=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.ArilleryIcon.name)
					{
						if(ArtilleryBehaviour.cooldown<0)
						{
							if(Energy.UseEnergy(config.artilleryEnergyCost))
							{
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.ArilleryIcon.name).SetSprite(Game.Prototypes.Extras.ArilleryIcon.name+"Clicked");
								ArtilleryBehaviour.played=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.EnergyDrainIcon.name)
					{
						if(EnergyDrain.cooldown<0)
						{
							if(Energy.UseEnergy(config.energyDrainEnergyCost))
							{
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.EnergyDrainIcon.name).SetSprite(Game.Prototypes.Extras.EnergyDrainIcon.name+"Clicked");
								EnergyDrain.played=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.EMPicon.name)
					{
						if(EMPBehaviour.cooldown<0)
						{
							if(Energy.UseEnergy(config.EMPEnergyCost))
							{
								Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.EMPicon.name).SetSprite(Game.Prototypes.Extras.EMPicon.name+"Clicked");
								EMPBehaviour.played=true;
								RosetteBehaviour.Block();
								BuyTowerBox.BlockTowerSpawn();
							}
						}
					}
					
				}
			}
		}
		
		//here
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
		
			if(Physics.Raycast(ray, out hit))
			{
				if (hit.transform.gameObject.name == Game.Prototypes.Extras.OrbitalStrikeIcon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.AirDropIcon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.AirStrike.name && !AirStrikeBehaviour.blockSecondCast)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.ArilleryIcon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.EnergyDrainIcon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);		
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.EMPicon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.Prototypes.Extras.WarBannerIcon.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
					}
					if (hit.transform.gameObject.name == Game.TowerToolbar.towerToolbar.name)
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
						TowerSpawn.cursor.SetActive(false);
						//Destroy(TowerSpawn.alphaTower);
						//Destroy(TowerSpawn.lastLowAlphaSprite);
					}
					if (hit.transform.gameObject.name == "PauseButton")
					{
						TowerSpawn.tempCursor.SetActive(false);
						BuyTowerBox.BlockTowerSpawn();
						TowerSpawn.cursor.SetActive(false);
						Destroy(TowerSpawn.alphaTower);
						Destroy(TowerSpawn.lastLowAlphaSprite);
					}
			}
		}
	}
	
	public static void BlockTowerSpawn()
	{
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit))
		{
			if(hit.transform.gameObject.name==Game.SpellToolbar.spellToolbar.name)
			{
				BuyTowerBox.BlockTowerSpawn();
				
			}
		}
	}
	
	public static void Block()
	{
		_blocked = true;
		OrbitalStrikeBehaviour.played=false;
		AirDropBehaviour.played=false;
		AirStrikeBehaviour.playedAir=false;
		ArtilleryBehaviour.played=false;
		EnergyDrain.played=false;
		EMPBehaviour.played=false;
	}
	public static bool IfCast()
	{
		if(!OrbitalStrikeBehaviour.played && !AirDropBehaviour.played && !AirStrikeBehaviour.playedAir && !ArtilleryBehaviour.played && !EnergyDrain.played && !EMPBehaviour.played)
		{
			return true;	
		}
		else
		{
			return false;	
		}
	}
	
	public static void Unblock()
	{
		_blocked = false;
	}
}
