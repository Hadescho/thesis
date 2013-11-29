using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeedBoosterBehaviour : TowerBehaviour
{
//	--Consts--
//	private const char damageAura = 'd';
//	private const char speedAura = 's';
//	private const char slowAura = 'l';
//	private const char normal = 'n';
//	--Consts--
	
	public float current_range;
	public float range;
	
	private List<GameObject> afectedTowers;
	// Use this for initialization
	
	void Awake()
	{
		afectedTowers = new List<GameObject>();
	}
	
	new void Start ()
	{
		base.Start();
		range = type.range;
		gameObject.GetComponent<exSprite>().color = new Color(0.5f,1.0f,0.5f);
		InvokeRepeating("SpeedAuraAction",Time.time,1f);
	}
	
	void Update ()
	{
		
	}
	
	public void SpeedAuraAction()
	{		
		foreach( GameObject tower in TowerSpawn.allTowers)
		{
			if ((tower.transform.position - gameObject.transform.position).magnitude < type.range && tower.name != "AuraTower" 
				&& !tower.GetPath().Contains("Prototypes") && !afectedTowers.Contains(tower) && !tower.name.Contains("Booster"))
			{
				afectedTowers.Add(tower);
				tower.GetComponent<TowerBehaviour>().auraBonusSpeed = type.bonusSpeed;
			}
		}	
	}
	
//	public void UpgradeToSpeedAura()
//	{
//		
////		//TODO: Change sprite to Speed Aura sprite
////		if(type == normal)
////		{
////			type = speedAura;
////			gameObject.GetComponent<exSprite>().color = new Color(0.5f,1.0f,0.5f);
////			foreach( GameObject tower in TowerSpawn.allTowers)
////			{
////				if ((tower.transform.position - gameObject.transform.position).magnitude < type.range && tower.name != "AuraTower" && !tower.GetPath().Contains("Prototypes"))
////				{
////					//Debug.Log (tower);
////					afectedTowers.Add(tower);
////					tower.GetComponent<TowerBehaviour>().auraBonusSpeed = type.bonusSpeed;
////				}
////			}	
////		}
////		else
////		{
////			Debug.LogError("You can't upgrade the aura tower for second time");
////			return;
////		}
//	}
//	
//	public void UpgradeToSlowAura()
//	{
////		if (type == normal)
////		{
////			gameObject.GetComponent<exSprite>().color = new Color(0.5f,0.5f,1.0f);
////			type = slowAura;
////		}
//	}
//	
//	public void SlowAuraAction()
//	{
////		foreach(GameObject enemy in Enemy.all)
////		{
////			if (Game.InRange(enemy,gameObject,type.range) && AirUnitAtack(enemy, config))
////			{
////				enemy.GetComponent<Enemy>().Slow(type.slowPercent,0.1f);
////			}
////		}
//	}
//	
//
//	void Debugger()
//	{
////		Debug.Log("AuraTower Deugger Begin:");
////		foreach(GameObject tow in TowerSpawn.allTowers)
////		{
////			Debug.Log(tow);
////		}
////		Debug.Log ("AuraTower Debugger End ---");
//	}
//	
//	/*public void ShowRosette()
//	{
//		if(RosetteBehaviour.instance == null)
//		{
//			Debug.Log ("vliza v instance == null");
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//		else
//		{
//			Debug.Log ("vliza v instance != null");
//			RosetteBehaviour.instance.DestroyRosette();
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//	}*/
	
	protected new void OnDestroy()
	{
		for(int i = 0; i < afectedTowers.Count; i++)
		{
			afectedTowers[i].GetComponent<TowerBehaviour>().auraBonusSpeed = 1;
		}
		TowerSpawn.RemoveTower(gameObject);
	}
}

