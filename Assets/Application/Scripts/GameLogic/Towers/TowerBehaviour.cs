using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBehaviour : SiriusBehaviour
{
	public class Config
	{
		public Dictionary<string, TowerType> towers =
		new Dictionary<string, TowerType>(){
			{ 
				"Machinegun", 
				new TowerType
				{
					range = 300.0f,
					price = 50.0f,
					Upgrade1 = "",
					Upgrade2 = "Machinegun 2",
					Upgrade3 = "Anti-Air Turret",
					targetingAirUnits = false,
					
					cooldownMax = 0.1f,
					projectileDamage = 5.0f
				}
			},
			{ 
				"Machinegun 2", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Machinegun 3",
					Upgrade2 = "Railgun",
					Upgrade3 = "Plasma",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.1f,
					projectileDamage = 7.5f
				}
			},
			{
				"Machinegun 3", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Twin-Barrel Machinegun",
					Upgrade2 = "",
					Upgrade3 = "",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.1f,
					projectileDamage = 10f
				}
			},
			{
				"Railgun", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Railgun 2",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.8f,
					projectileDamage = 100f
				}
			},
			{
				"Railgun 2", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Railgun 3",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.8f,
					projectileDamage = 200f
				}
			},
			{
				"Railgun 3", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					targetingAirUnits = false,
					
					
					cooldownMax = 0.8f,
					projectileDamage = 300f
				}
			},
			{
				"Plasma", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Plasma 2",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.05f,
					projectileDamage = 4.5f
				}
			},
			{
				"Plasma 2", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Plasma 3",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.05f,
					projectileDamage = 6.5f
				}
			},
			{
				"Plasma 3", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					targetingAirUnits = false,
					
					
					cooldownMax = 0.05f,
					projectileDamage = 10.5f
				}
			},
			{ 
				"Twin-Barrel Machinegun", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					Upgrade1 = "Twin-Barrel Machinegun 2",
					targetingAirUnits = false,
					
					
					cooldownMax = 0.1f,
					projectileDamage = 7.5f
				}
			},
			{ 
				"Twin-Barrel Machinegun 2", 
				new TowerType
				{
					range = 350.0f,
					price = 100.0f,
					targetingAirUnits = false,
					
					
					cooldownMax = 0.1f,
					projectileDamage = 10f
				}
			},
			{ 
				"Anti-Air Turret", 
				new TowerType
				{
					range = 300.0f,
					price = 50.0f,
					Upgrade1 = "Flak Cannon",
					targetingAirUnits = true,
					onlyAirUnits = true,
					
					cooldownMax = 0.1f,
					projectileDamage = 5.0f
				}
			},
			{ 
				"Flak Cannon", 
				new TowerType
				{
					range = 300.0f,
					price = 50.0f,
					Upgrade1 = "Dual Flak Cannon",
					targetingAirUnits = true,
					onlyAirUnits = true,
					
					cooldownMax = 0.1f,
					projectileDamage = 5.0f
				}
			},
			{ 
				"Dual Flak Cannon", 
				new TowerType
				{
					range = 300.0f,
					price = 50.0f,
					Upgrade1 = "Quad Flak Cannon",
					targetingAirUnits = true,
					onlyAirUnits = true,
					
					cooldownMax = 0.1f,
					projectileDamage = 5.0f
				}
			},
			{ 
				"Quad Flak Cannon", 
				new TowerType
				{
					range = 300.0f,
					price = 50.0f,
					targetingAirUnits = true,
					onlyAirUnits = true,
					
					cooldownMax = 0.1f,
					projectileDamage = 5.0f
				}
			},
			{ 
				"Cannon", 
				new TowerType
				{
					range = 250.0f,
					price = 100.0f,
					targetingAirUnits = false,
					Upgrade2 = "Cannon 2",
					Upgrade3 = "Rocket Platform",
					cooldownMax = 1f,
					projectileDamage = 150.0f
				}
			},
			{ 
				"Cannon 2", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade2 = "Hellfire Cannon",
					Upgrade3 = "Poison Gas Cannon",
					cooldownMax = 1f,
					projectileDamage = 150.0f
				}
			},
			{ 
				"Hellfire Cannon", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade1 = "Hellfire Cannon 2",
					cooldownMax = 1f,
					projectileDamage = 200.0f
				}
			},
			{ 
				"Hellfire Cannon 2", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade1 = "Hellfire Cannon 3",
					cooldownMax = 1f,
					projectileDamage = 250.0f
				}
			},
			{ 
				"Hellfire Cannon 3", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,

					cooldownMax = 1f,
					projectileDamage = 350.0f
				}
			},
			{ 
				"Poison Gas Cannon", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					Upgrade1 = "Poison Gas Cannon 2",
					targetingAirUnits = false,

					cooldownMax = 1f,
					projectileDamage = 200.0f
				}
			},
			{ 
				"Poison Gas Cannon 2", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					Upgrade1 = "Poison Gas Cannon 3",
					targetingAirUnits = false,

					cooldownMax = 1f,
					projectileDamage = 250.0f
				}
			},
			{ 
				"Poison Gas Cannon 3", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,

					cooldownMax = 1f,
					projectileDamage = 250.0f
				}
			},
			{ 
				"Rocket Platform", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade2 = "Cryo-rocket Platform",
					Upgrade3 = "Missile Platform",
					cooldownMax = 1f,
					projectileDamage = 150.0f
				}
			},
			{ 
				"Cryo-rocket Platform", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade1 = "Cryo-rocket Platform 2",
					cooldownMax = 1f,
					projectileDamage = 50.0f
				}
			},
			{ 
				"Cryo-rocket Platform 2", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					Upgrade1 = "Cryo-rocket Platform 3",
					cooldownMax = 1f,
					projectileDamage = 100.0f
				}
			},
			{ 
				"Cryo-rocket Platform 3", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = false,
					cooldownMax = 1f,
					projectileDamage = 150.0f
				}
			},
			{ 
				"Missile Platform", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = true,
					Upgrade1 = "Missile Platform 2",
					cooldownMax = 1f,
					projectileDamage = 150.0f
				}
			},
			{ 
				"Missile Platform 2", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = true,
					Upgrade1 = "Missile Platform 3",
					cooldownMax = 1f,
					projectileDamage = 200.0f
				}
			},
			{ 
				"Missile Platform 3", 
				new TowerType
				{
					range = 250.0f,
					price = 150.0f,
					targetingAirUnits = true,
					cooldownMax = 1f,
					projectileDamage = 250.0f
				}
			},
			{ 
				"Utility Tower", 
				new TowerType
				{
					price = 0f,
					range = 0.001f,
					Upgrade2 = "Tesla Tower",
					Upgrade3 = "Aura Tower "

				}
			},
			{ 
				"Aura Tower ", 
				new TowerType
				{
					price = 0f,
					range = 0.001f,
					Upgrade1 = "Damage Booster",
					Upgrade2 = "Fusion Plant",
					Upgrade3 = "Speed Booster"

				}
			},
			{ 
				"Damage Booster", 
				new TowerType
				{
					price = 400f,
					range = 350f,
					Upgrade1 = "Damage Booster 2",
					bonusDamage = 1.5f

				}
			},
			{ 
				"Damage Booster 2", 
				new TowerType
				{
					price = 600f,
					range = 350f,
					Upgrade1 = "Damage Booster 3",
					bonusDamage = 2f

				}
			},
			{ 
				"Damage Booster 3", 
				new TowerType
				{
					price = 800f,
					range = 350f,
					bonusDamage = 2.5f
				}
			},
			{ 
				"Speed Booster", 
				new TowerType
				{
					price = 400f,
					range = 350f,
					Upgrade1 = "Speed Booster 2",
					bonusSpeed = 0.75f

				}
			},
			{ 
				"Speed Booster 2", 
				new TowerType
				{
					price = 600f,
					range = 350f,
					Upgrade1 = "Speed Booster 3",
					bonusSpeed = 0.5f

				}
			},
			{ 
				"Speed Booster 3", 
				new TowerType
				{
					price = 800f,
					range = 350f,
					bonusSpeed = 0.25f

				}
			},
			{ 
				"Fusion Plant", 
				new TowerType
				{
					price = 400f,
					Upgrade1 = "Fusion Plant 2",
					energyRegen = 1,

				}
			},
			{ 
				"Fusion Plant 2", 
				new TowerType
				{
					price = 600f,
					Upgrade1 = "Fusion Plant 3",
					energyRegen = 2,

				}
			},
			{ 
				"Fusion Plant 3", 
				new TowerType
				{
					price = 800f,
					energyRegen = 3,

				}
			},
			{ 
				"Tesla Tower", 
				new TowerType
				{
					price = 200,
					range = 200,
					cooldownMax = 2f,
					projectileDamage = 10,
					chaining = 0,
					
					Upgrade2 = "Shock Tower",
					Upgrade3 = "Lightning Tower"

				}
			},
			{
				"Shock Tower",
				new TowerType
				{
					price = 300,
					range = 250,
					cooldownMax = 3f,
					stunDuration = 0.25f,
					Upgrade1 = "Shock Tower 2",
				}
			},
			{
				"Shock Tower 2",
				new TowerType
				{
					price = 400,
					range = 280,
					cooldownMax = 3f,
					stunDuration = 0.5f,
					Upgrade1 = "Shock Tower 3",
				}
			},
			{
				"Shock Tower 3",
				new TowerType
				{
					price = 600,
					range = 320,
					cooldownMax = 3f,
					stunDuration = 0.75f,
				}
			},
			{
				"Lightning Tower",
				new TowerType
				{
					price = 300,
					range = 150,
					cooldownMax = 1.5f,
					projectileDamage = 300.0f,
					chaining = 1,
					
					Upgrade1 = "Lightning Tower 2"
				}
					
			},
			{
				"Lightning Tower 2",
				new TowerType
				{
					price = 450,
					range = 150,
					cooldownMax = 1.5f,
					projectileDamage = 300.0f,
					chaining = 2,
					
					Upgrade1 = "Lightning Tower 3"
				}
			},
			{
				"Lightning Tower 3",
				new TowerType
				{
					price = 600,
					range = 150,
					cooldownMax = 1.5f,
					projectileDamage = 300.0f,
					chaining = 3,
					
				}
			},
			{
				"MachineGun",
				new TowerType
				{
					range = 270,
					cooldownMax = 0.1f,
					projectileDamage = 600f,
				}
			},
			
		};
	}
		
	public static Config config = new Config();
	
	public TowerType type;
	
	public float cooldown;
	public float currDamage;
	public float auraBonusDamage;
	public float auraBonusSpeed;
	public GameObject rosette;
	public float timeshift;
	public GameObject target;
	
	
	protected virtual void Start () 
	{
		type = config.towers[gameObject.name];
		auraBonusDamage = 1;
		auraBonusSpeed = 1;
		timeshift = 1;
		if(!gameObject.GetPath().Contains("Prototype"))
		{
			TowerSpawn.AddTower(gameObject);
		}
	}
	
	void Update () 
	{
		
	}
	
	public void ShowRosette()
	{
		if(!RosetteBehaviour.blocked)
		{
			if(RosetteBehaviour.instance == null)
			{
				rosette = Game.Prototypes.Rosette.Center.Clone();
				rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
				Game.LastRosette.Setter(rosette);
				
			}
			else
			{
				RosetteBehaviour.instance.DestroyRosette();
				rosette = Game.Prototypes.Rosette.Center.Clone();
				rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
				Game.LastRosette.Setter(rosette);
				
			}
		}
	}
	
	public virtual bool TargetValidation(GameObject enemy)
	{
		if (Game.InRange(gameObject,enemy,type.range)){
			if (!enemy.GetComponent<Enemy>().isFlying && !type.onlyAirUnits)
			{
				return true;
			}
			if (enemy.GetComponent<Enemy>().isFlying && (type.targetingAirUnits || type.onlyAirUnits))
			{
				return true;
			}
		}
		return false;
	}
	
	public static void FloatingTextSpawn(float valueSpent,GameObject pos, char sign = '+')
	{
		if (sign != '-' && sign != '+')
		{
			Debug.LogError("The sign character should be '+' (default) or '-'. Returning.");
			return;
		}

		FloatingText floatie;

		if (sign == '+')
		{
			floatie = Game.Prototypes.FloatingTexts.Cash.Clone(sign + valueSpent.ToString(),pos.transform.position);
		}
		else
		{
			floatie = Game.Prototypes.FloatingTexts.CashSpent.Clone(sign + valueSpent.ToString(),pos.transform.position);
		}

		floatie.Speed = new Vector3(0,1.5f,0);	
	}
	
	public virtual void Sell()
	{
		float val = type.price * 0.75f;
		Currency.GiveCurrency(val);
		FloatingTextSpawn(val,gameObject);
		TowerSpawn.RemoveTower(gameObject);
	}
	
	protected virtual bool AirUnitAtack(GameObject enemy,TowerType config)
	{
		if (!enemy.GetComponent<Enemy>().isFlying)
		{
			return true;
		}
		
		if (enemy.GetComponent<Enemy>().isFlying && type.targetingAirUnits)
		{
			return true;
		}
		
		return false;
	}
	
	
	protected void OnDestroy()
	{
		TowerSpawn.RemoveTower(gameObject);
	}
	
	public virtual void UpgradeTo(string towerName)
	{
		GameObject newTower = GameObjectExt.Find("/Prototypes/NewTowers/" + towerName).Clone();
		if (Currency.Purchase(TowerBehaviour.config.towers[towerName].price))
		{
			newTower.transform.position = gameObject.transform.position;
			newTower.transform.rotation = gameObject.transform.rotation;
			TowerSpawn.AddTower(newTower);
			TowerSpawn.RemoveTower(gameObject);
			Destroy(gameObject);
			FloatingTextSpawn(TowerBehaviour.config.towers[towerName].price,gameObject,'-');
		}
		else
		{
			Destroy(newTower);
			FloatingText floatie = Game.Prototypes.FloatingTexts.CashSpent.Clone("Not enough cash.",gameObject.transform.position);
			floatie.Speed = new Vector3(0,1.5f,0);
		}
	}
}
