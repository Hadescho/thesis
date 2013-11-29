using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warbanner : SiriusBehaviour {
	public class Config
	{
		public float range = 150;
		public float bonusAuraDamage = 1f;
		public float bonusAuraSpeed = 0.5f;
		public float time = 7.0f;
		public float cost = 50f;
	}
	public static Config config = new Config();
	
	public static bool activeWar;
	public float timeLeft;
	public static GameObject banner;
	public static GameObject spriteObject;
	
	
	void Awake()
	{
		activeWar = false;
	}
	
	void Start () 
	{
		
	}
	
	void Update () 
	{		
		if (timeLeft > 0 )
		{
			
			timeLeft -= Time.deltaTime;
		}
		else
		{
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.WarBannerIcon.name).SetSprite(Game.Prototypes.Extras.WarBannerIcon.name);
			Debuff();
			spriteObject.SetActive(false);
			Destroy(gameObject);
			
		}
	}
	
	private void Debuff()
	{
		foreach(GameObject tower in TowerSpawn.allTowers)
		{
			if (Game.InRange(banner,tower,config.range))
			{
				tower.GetComponent<TowerBehaviour>().auraBonusDamage -= config.bonusAuraDamage;
				tower.GetComponent<TowerBehaviour>().auraBonusSpeed  += config.bonusAuraSpeed;
			}
		}
	}
	
	public static void Cast(Vector3 position)
	{
		if (!activeWar && Energy.UseEnergy(config.cost))
		{
			position.z = -2 + position.y / 1000;
			//banner = Game.Prototypes.Extras.WarBanner.Clone();
			banner.transform.position = position;;
			if(banner.GetComponent<Warbanner>()== null)
			{
				banner.AddComponent<Warbanner>().timeLeft = config.time;
			}
			else
			{
				banner.GetComponent<Warbanner>().timeLeft = config.time;
			}
			activeWar = false;
			//Sirius.ExecAfter(Time.deltaTime,Game.SetBuildingBlockFalse);
		}
	}

	public static void MarkSetter()
	{
		if(spriteObject == null)
		{
			spriteObject = Game.Cursors.TowerRangeCursor.Clone();
			spriteObject.GetComponent<exSprite>().color = new Color(0.2f,1f,0.8f);
			Vector2 temp = new Vector2(config.range / (spriteObject.GetComponent<exSprite>().width/2), config.range / (spriteObject.GetComponent<exSprite>().height/2));
			spriteObject.GetComponent<exSprite>().scale = temp;
			spriteObject.SetActive(false);	
		}
		if(!spriteObject.activeSelf)
		{
			spriteObject.SetActive(true);	
		}
		Vector3 position = Game.GetMouseCoord();
		spriteObject.transform.position = new Vector3(position.x,position.y,-4);

	}
	
	public static void GiveDamage()
	{
		foreach(GameObject tower in TowerSpawn.allTowers)
		{
			if (Game.InRange(banner,tower,config.range))
			{
				tower.GetComponent<TowerBehaviour>().auraBonusDamage += config.bonusAuraDamage;
				tower.GetComponent<TowerBehaviour>().auraBonusSpeed  -= config.bonusAuraSpeed;
			}
			Sirius.ExecAfter(0.2f,()=>
			{
				if (RosetteBehaviour.blocked)
				{
					Debug.Log ("Unblocking rosette from Warbanner");
					RosetteBehaviour.Unblock();
					BuyTowerBox.UnblockTowerSpawn();
				}
			}
			);
		}
	}

}
