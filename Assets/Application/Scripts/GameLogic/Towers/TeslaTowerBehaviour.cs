using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeslaTowerBehaviour : TowerBehaviour {
	
	public static List<GameObject> lightingList;
	
	public bool firing;
	public bool inRange;
	
	
	public GameObject target;
	public GameObject lighting;
	public GameObject lighting2;
	
	//Rosette Vars
	//public GameObject rosette;
	
	new void Start () 
	{
		base.Start();
		cooldown = 0;
		target = null;
		currDamage = type.projectileDamage;
		auraBonusSpeed = 1;
		auraBonusDamage = 1;
		lightingList = new List<GameObject>();
	}
	
	void Update () {
//		if (target == null)
//		{
//			for (int i = 0; i < Enemy.all.Count; i++)
//			{
//				if ((gameObject.transform.position - Enemy.all[i].transform.position).magnitude <= type.range && AirUnitAtack(Enemy.all[i],type) && cooldown <= 0)
//				{
//					target = Enemy.all[i];
//					currDamage = type.projectileDamage * auraBonusDamage;
//					lighting = Spawn(gameObject,target);
//					cooldown = type.cooldownMax;
//					break;
//				}
//			}
//		}
//		else
//		{
//			if ((gameObject.transform.position - target.transform.position).magnitude > type.range)
//			{
//				Destroy(lighting);
//				target = null;
//			}
//		}
		if (target == null)
		{
			target = GetTarget();
		}
		if(target != null && Game.InRange(gameObject,target,type.range))
		{
			if (cooldown <= 0)
			{
				Spawn(gameObject,target);
				cooldown = type.cooldownMax;
			}	
		}
		else
		{
			target = null;
		}
		
		cooldown -= Time.deltaTime;
	}
	
	protected GameObject GetTarget()
	{
		foreach(GameObject enemy in Enemy.all)
		{
			if (TargetValidation(enemy) && Game.InRange(gameObject,enemy,type.range))
			{
				return enemy;
			}
		}
		return null;
	}

	
	public static GameObject Spawn(GameObject origin, GameObject target)
	{
		//Clone
		GameObject lightingSpawn = Game.Prototypes.Projectiles.Tower3shot.Clone();
		
		lightingSpawn.SetParent(origin);
		//Add component
		lightingSpawn.AddComponent<LightningBehaviour>().target = target;
		lightingSpawn.GetComponent<LightningBehaviour>().origin = origin;
		if (origin.GetComponent<TowerBehaviour>() != null)
		{
			lightingSpawn.GetComponent<LightningBehaviour>().currDamage = origin.GetComponent<TowerBehaviour>().currDamage;
		}
		lightingList.Add(lightingSpawn);
		
		return lightingSpawn;
	}
	
//	protected virtual GameObject Spawn2(GameObject origin, GameObject target, GameObject target2)
//	{
//		bool notBreaker = true;
//		GameObject lightingSpawn = null;
//		foreach(GameObject ligh in lightingList)
//		{
//			if(ligh.GetComponent<LightningBehaviour>().origin == target)
//			{
//				notBreaker = false;
//			}
//		}
//		
//		if (notBreaker)
//		{
//			//Clone
//			lightingSpawn = Game.Prototypes.Projectiles.Tower3shot.Clone();
//			
//			
//			//Add component
//			lightingSpawn.AddComponent<LightningBehaviour>().target = target2;
//			lightingSpawn.GetComponent<LightningBehaviour>().origin = target;
//			lightingSpawn.GetComponent<LightningBehaviour>().currDamage = origin.GetComponent<Tower3Behaviour>().currDamage;
//			lightingSpawn.GetComponent<LightningBehaviour>().secondLighting = true;
//			lightingList.Add (lightingSpawn);
//		}
//		return lightingSpawn;
//	}
	
	/*public void ShowRosette()
	{
		if(RosetteBehaviour.instance == null)
		{
			rosette = Game.Prototypes.Rosette.Center.Clone();
			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
			
		}
		else
		{
			RosetteBehaviour.instance.DestroyRosette();
			rosette = Game.Prototypes.Rosette.Center.Clone();
			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
			
		}
	}*/
	
	
	public override void Sell()
	{
		float val = Currency.Prices.Tower3Price * 0.75f;
		Currency.GiveCurrency(val);
		FloatingTextSpawn(val,gameObject,'-');
		Destroy (gameObject);
		TowerSpawn.DestroyInPosList(gameObject.transform.position);
		Destroy(lighting);
			
	}
	
//	public static void FloatingTextSpawn(float valueSpent,GameObject pos)
//	{
//		FloatingText floatie = Game.Prototypes.FloatingTexts.Cash.Clone(pos.transform.position);
//		floatie.SetText("+" + valueSpent.ToString());
//		floatie.Speed = new Vector3(0,1.5f,0);
//		
//	}
	
	protected new void OnDestroy()
	{
		TowerSpawn.RemoveTower(gameObject);
	
	}
}
