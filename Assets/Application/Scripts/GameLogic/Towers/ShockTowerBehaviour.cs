using UnityEngine;
using System.Collections;

public class ShockTowerBehaviour : TowerBehaviour {
	
	
	new void Start ()
	{
		base.Start();
		cooldown = 0;
	}
	
	void Update ()
	{
		if (cooldown < 0)
		{
			Game.Stun(gameObject.transform.position,type.range);
			cooldown = type.cooldownMax;
			Sirius.ExecAfter(type.stunDuration,ExecuteNoStun);
		}
		cooldown -= Time.deltaTime * timeshift;
	}
	
	private void ExecuteNoStun()
	{
		Game.notStun(gameObject.transform.position,type.range);		
	}
	
}
