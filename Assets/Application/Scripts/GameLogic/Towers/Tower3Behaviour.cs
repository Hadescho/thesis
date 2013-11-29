using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower3Behaviour : TowerBehaviour {
	
	public static List<GameObject> lightingList;
	
	public new class Config : TowerBehaviour.Config
	{
		public float radius = 150f; 
		public float cooldownMax = 0.75f;
		public float price = Currency.Prices.Tower3Price;
	}
	
	public static new Config config = new Config();
	
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
		currDamage = Game.Towers.Damage.Tower3;
		auraBonusSpeed = 1;
		lightingList = new List<GameObject>();
	}
	
	void Update () {
		if (target == null)
		{
			for (int i = 0; i < Enemy.all.Count; i++)
			{
				if ((gameObject.transform.position - Enemy.all[i].transform.position).magnitude <= type.range && AirUnitAtack(Enemy.all[i],type))
				{
					target = Enemy.all[i];
					currDamage = Game.Towers.Damage.Tower3 * auraBonusDamage;
					lighting = Spawn(gameObject,target);
					break;
				}
			}
		}
		if (target != null)
		{
			if ((gameObject.transform.position - target.transform.position).magnitude > type.range)
			{
				Destroy(lighting);
				target = null;
			}
		}
	}
	
	protected virtual GameObject Spawn(GameObject origin, GameObject target)
	{
		
		//Clone
		GameObject lightingSpawn = Game.Prototypes.Projectiles.Tower3shot.Clone();
		
		
		//Add component
		lightingSpawn.AddComponent<LightningBehaviour>().target = target;
		lightingSpawn.GetComponent<LightningBehaviour>().origin = origin;
		lightingSpawn.GetComponent<LightningBehaviour>().currDamage = origin.GetComponent<Tower3Behaviour>().currDamage;
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
		if (lighting != null)
		{
			Destroy (lighting);
		}
		if (lighting2 != null)
		{
			Destroy(lighting2);
		}
		TowerSpawn.RemoveTower(gameObject);
	
	}
}