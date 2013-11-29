using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageBoosterBehaviour : TowerBehaviour
{
//	--Consts--
//	private const char damageAura = 'd';
//	private const char speedAura = 's';
//	private const char slowAura = 'l';
//	private const char normal = 'n';
//	--Consts--

	
	private List<GameObject> afectedTowers;
	// Use this for initialization
	
	void Awake()
	{
		afectedTowers = new List<GameObject>();
	}
	new void Start ()
	{
		base.Start();
		
		gameObject.GetComponent<exSprite>().color = new Color(1.0f,0.5f,0.5f);
		InvokeRepeating("DamageAuraAction",Time.time,0.5f);
	}
	
	void Update ()
	{
		
	}
	
	private void DamageAuraAction()
	{		
		foreach( GameObject tower in TowerSpawn.allTowers)
		{
			if (tower != null && (tower.transform.position - gameObject.transform.position).magnitude < type.range
				&& tower.name != "AuraTower" && !tower.GetPath().Contains("Prototypes") && !afectedTowers.Contains(tower))
			{
				afectedTowers.Add(tower);
				tower.GetComponent<TowerBehaviour>().auraBonusDamage = type.bonusDamage;
			}	
		}
	}
	
	protected new void OnDestroy()
	{
		for(int i = 0; i < afectedTowers.Count; i++)
		{
			afectedTowers[i].GetComponent<TowerBehaviour>().auraBonusDamage -= type.bonusDamage;
		}
		
		TowerSpawn.RemoveTower(gameObject);
	}
}