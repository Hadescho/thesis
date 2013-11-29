using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwinBarrelMachinegunBehaviour : Tower2Behaviour {

	
	protected List<GameObject> muzzleList;
	// Use this for initialization
	new void Start () 
	{
		base.Start();
		cooldown = 0;
		target = null;
		muzzleList = new List<GameObject>();
		for(int i = 0; i < gameObject.GetChildCount();i++)
		{
			muzzleList.Add(gameObject.GetChild(i));
		}
		rotating = false;
		currDamage = Game.Towers.Damage.Tower2;
		auraBonusSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
	if (target == null)
		{
			for(int i = 0; i < Enemy.all.Count ; i++)
			{
				Vector3 temp = gameObject.transform.position  - Enemy.all[i].transform.position;
					
				
				if (temp.magnitude < type.range && !Enemy.all[i].GetComponent<Enemy>().isFlying)
				{
					target = Enemy.all[i];
				}
			}
		}
		if (target != null)
		{
				Quaternion rotation = gameObject.transform.localRotation;
				Vector3 rotor = rotation.eulerAngles;
				float targetAngle = (target.transform.position - transform.position).GetAngle() + 180;
				float objectAngle = rotor.z;
				float angleToTarget = Mathf.DeltaAngle(targetAngle, objectAngle);
			
				if (Mathf.Abs(angleToTarget) < 170f)
				{
					
					
					if (angleToTarget < 0 )
					{
						rotor.z -= 150.0f * Time.deltaTime * timeshift;
					}
					else
					{
						rotor.z += 150.0f * Time.deltaTime * timeshift;
					}
					rotation.eulerAngles = rotor;
					gameObject.transform.rotation = rotation;
					rotating = true;
					
				}
				else
				{
					rotating = false;
				}
		}		
		
		if (cooldown <= 0 && target != null && !rotating){
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			currDamage = Game.Towers.Damage.Tower2 * auraBonusDamage;
			
			if (temp.magnitude < type.range)
			{
				foreach(GameObject muzzle in muzzleList)
				{
					GameObject laser = Game.Prototypes.Projectiles.Tower2shot.Clone();
					laser.transform.position = muzzle.transform.position;
					laser.AddComponent<Laser>().target = target;
					laser.GetComponent<Laser>().currDamage = currDamage;
					laser.GetComponent<Laser>().origin = gameObject;
					cooldown = type.cooldownMax * auraBonusSpeed;
				}
			}
			else
			{
				target = null;
			}
		
			
		}
		else
		{
			cooldown -= Time.deltaTime * timeshift;
		}
		
		if(target != null && target.GetComponent<Enemy>().isDead)
		{
			target = null;
		}
	}
}
