using System;
using System.Collections.Generic;

[Serializable]
public class TowerType
{
	public float range;
	public float price;
	public string Upgrade1 = "";
	public string Upgrade2 = "";
	public string Upgrade3 = "";
	public bool targetingAirUnits = false;
	public bool onlyAirUnits = false;
	public int requisitionPrice = 10;
	
	public float cooldownMax = 0.1f;
	public float projectileDamage;
	
	
	public float areaOfEffect = 120;
	
	public float bonusDamage = 2f;
	public float bonusSpeed = 0.5f;
	
	public float stunDuration = 0.75f;
	
	public float energyRegen;
	
	public float chaining = 0;
}