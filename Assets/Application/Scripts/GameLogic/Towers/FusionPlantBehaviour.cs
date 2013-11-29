using UnityEngine;
using System.Collections;

public class FusionPlantBehaviour : TowerBehaviour {

	new void Start () 
	{
		base.Start();
		InvokeRepeating("Action",Time.time,type.cooldownMax);
	}
	
	private void Action()
	{
		Energy.GiveEnergy(type.energyRegen);
	}
}