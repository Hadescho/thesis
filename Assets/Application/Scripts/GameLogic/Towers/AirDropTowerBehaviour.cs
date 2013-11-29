using UnityEngine;
using System.Collections;

public class AirDropTowerBehaviour : TowerBehaviour 
{	
	public new Config config = new Config();
	
	
	public GameObject target;
	public GameObject muzzle;
	public bool rotating;
	
	
	new void Start () 
	{
		base.Start();
		cooldown=0;
		target = null;
		muzzle = gameObject.FindChild("TowerMuzzle");
		rotating = false;
		currDamage = type.projectileDamage;
	}

	void GetTarget ()
	{
		for(int i = 0; i < Enemy.all.Count ; i++)
		{
			if (TargetValidation(Enemy.all[i]))
			{
				target = Enemy.all[i];
			}
		}
	}
	

	void Update () {
		
		if (target == null)
		{
			GetTarget ();
		}
		else
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
						rotor.z -= 150.0f * Time.deltaTime ;
					}
					else
					{
						rotor.z += 150.0f * Time.deltaTime ;
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
		
		if (cooldown<0 && target != null && !rotating)
		{
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			
			if (temp.magnitude < type.range)
			{
				GameObject laser = Game.Prototypes.Projectiles.Tower2shot.Clone ();
				laser.transform.position = muzzle.transform.position;
				laser.SetZ(-10.0f);
				laser.AddComponent<Laser>().target = target;
				laser.GetComponent<Laser>().currDamage = currDamage;
				laser.GetComponent<Laser>().origin = gameObject;
				cooldown = type.cooldownMax;
			}
			else
			{
				target = null;
			}
		
			
		}
		else
		{
			cooldown -= Time.deltaTime ;
		}
		
		if(target != null && target.GetComponent<Enemy>().isDead)
		{
			target = null;
		}
		
		

	}
	
}
