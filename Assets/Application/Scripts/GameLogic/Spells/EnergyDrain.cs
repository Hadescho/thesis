using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnergyDrain : SiriusBehaviour {
	
	public class Config
	{
		public float coeff = 0.01f;
		public float maxLife = 12.0f; 
		public float range = 250f;
		
		public float EnergyDrainCooldown=15.0f;
	}
	
	public static Config config = new Config();
	public static List<GameObject> inRangeCreeps;
	public static bool localActive;
	public static bool played;
	public static float cooldown;
	public static Vector3 pos;
	
	
	public static GameObject spriteObject;
	//---
	
	public float life;
	public bool localPlay;
	public bool isCoordTaken;
	public bool energyPlay;
	
	void Awake()
	{
		inRangeCreeps = new List<GameObject>();
	}
	
	void Start () 
	{
		life = config.maxLife;
		cooldown=-1.0f;
		localPlay=false;
		isCoordTaken=false;
		energyPlay=false;
		
		spriteObject = EMPBehaviour.spriteObject.Clone();
		spriteObject.GetComponent<exSprite>().color = new Color(0.2f,1f,0.8f);
		Vector2 temp = new Vector2(config.range / 60, config.range / 60);
		spriteObject.GetComponent<exSprite>().scale = temp;
		spriteObject.SetActive(false);
	}
	
	void Update () 
	{
		Game.DropSpellCooldown(ref cooldown);
		
		if(localPlay)
		{
			GetTimeshiftCoord();
			cooldown=config.EnergyDrainCooldown;
		}
		if(played)
		{
			localPlay=true;
			energyPlay=true;

		}
		if (life > 0 && energyPlay)
		{
			
			foreach(GameObject enemy in Enemy.all)
			{
				//RangeChecks
				if (!inRangeCreeps.Contains(enemy) && Game.InRange(enemy,pos,config.range))
				{
					inRangeCreeps.Add(enemy);
				}
				else if (!Game.InRange(enemy,pos,config.range) && inRangeCreeps.Contains(enemy))
				{
					inRangeCreeps.Remove(enemy);
				}
			}
			/*
			foreach(GameObject enemy in inRangeCreeps)
			{
				Debug.Log(inRangeCreeps.Count);
				if (enemy.GetComponent<Enemy>().isDead)
				{
					float temp = 500 * config.coeff;
					Debug.Log("zzzzzzzzz");
					Energy.GiveEnergy(temp);
					Energy.FloatingTextSpawn(temp,enemy.transform.position);
					inRangeCreeps.Remove(enemy);
				}
			}
			*/
			life -= Time.deltaTime;
		}
		else if(energyPlay && life < 0)
		{
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.EnergyDrainIcon.name).SetSprite(Game.Prototypes.Extras.EnergyDrainIcon.name);
			spriteObject.SetActive(false);
			played=false;
			energyPlay=false;
			Debug.Log(isCoordTaken);
			isCoordTaken=false;
			localPlay=false;
			inRangeCreeps.Clear();
			//Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.EnergyDrainIcon.name).SetSprite("Drain");
		}
	}

	public static void GiveEnergyFromEnergyDrain(GameObject enemy)
	{
		float temp = 500 * config.coeff;
		Energy.GiveEnergy(temp);
		Energy.FloatingTextSpawn(temp,enemy.transform.position);
		inRangeCreeps.Remove(enemy);
	}
	
	void OnDestroy()
	{

	}
	
	public void GetTimeshiftCoord()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if(!isCoordTaken)
			{
				//Game.Prototypes.Extras .EnergyDrainIcon.SetSprite("Drain");
				RosetteBehaviour.Unblock();
				BuyTowerBox.UnblockTowerSpawn();
				pos=Game.GetMouseCoord();
				life = config.maxLife;
				isCoordTaken=true;
			}
		}
		if(Input.GetMouseButton(0))
		{
			if(!isCoordTaken)
			{
				spriteObject.SetActive(true);
				MarkSetter();
			}
		}
	}
	
	public void MarkSetter()
	{
		Vector3 position = Game.GetMouseCoord();
		spriteObject.transform.position = new Vector3(position.x,position.y,-3);
			
	}
}
