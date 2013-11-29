using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MachinegunBehaviour : TowerBehaviour 
{
	public GameObject target;
	public GameObject muzzle;
	public bool rotating;
	//Rosette Vars
	//public GameObject rosette;
	public float rosetteTime;
	public bool rosetteActive;
	protected List<GameObject> muzzleList;
	
	
	new void Start () 
	{
		base.Start();
		cooldown = 0;
		target = null;
		muzzleList = new List<GameObject>();
		for(int i = 0; i < gameObject.GetChildCount();i++)
		{
			if(gameObject.GetChild(i).name.Contains ("Muzzle"))
			{
				muzzleList.Add(gameObject.GetChild(i));
			}
		}
		rotating = false;
		currDamage = Game.Towers.Damage.Tower2;
		auraBonusSpeed = 1;
	}
	

	void Update () {
		
		if (target == null)
		{
			for(int i = 0; i < Enemy.all.Count ; i++)
			{
					
				
				if (TargetValidation(Enemy.all[i]))
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
	
//	public override void ShowRosette()
//	{
//		if(RosetteBehaviour.instance == null)
//		{
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//		else
//		{
//			RosetteBehaviour.instance.DestroyRosette();
//			rosette = Game.Prototypes.Rosette.Center.Clone();
//			rosette.AddComponent<RosetteBehaviour>().owner = gameObject;
//			
//		}
//	}
	
	
//	public override void Sell()
//	{
//		float val = Currency.Prices.Tower2Price * 0.75f;
//		Currency.GiveCurrency(val);
//		FloatingTextSpawn(val,gameObject);
//		TowerSpawn.DestroyInPosList(gameObject.transform.position);
//		Destroy (gameObject);
//		
//	}
	
	public static void FloatingTextSpawn(float valueSpent,GameObject pos)
	{
		FloatingText floatie = Game.Prototypes.FloatingTexts.Cash.Clone(pos.transform.position);
		floatie.SetText("+" + valueSpent.ToString());
		floatie.Speed = new Vector3(0,1.5f,0);
		
	}
}
