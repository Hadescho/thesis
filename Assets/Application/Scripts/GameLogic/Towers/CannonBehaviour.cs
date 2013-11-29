using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonBehaviour : TowerBehaviour {

//	public new class Config : TowerBehaviour.Config
//	{
//		public float range = 300; 
//		public float cooldownMax = 1;
//		public float price = Currency.Prices.Tower1Price;
//		public float projectileDamage = Game.Towers.Damage.Tower1;
//		public new string Upgrade1 = "";
//		public new string Upgrade2 = "Cannon 2";
//		public new string Upgrade3 = "Rocket Platform";
//	}
//	
//	public static new Config config = new Config();

	public List<GameObject> muzzleList;
	public GameObject target;
	public bool rotating;
	protected GameObject projectile;
	
	
	new void Start () 
	{
		base.Start();
		cooldown = 0.0f;
		target = null;
		for(int i = 0; i < gameObject.GetChildCount(); i++)
		{
			if (gameObject.GetChild(i).name.Contains("Muzzle"))
			{
				muzzleList.Add (gameObject.GetChild(i));
			}
		}
		currDamage = Game.Towers.Damage.Tower1;
		auraBonusSpeed = 1;
		type.targetingAirUnits = false;
		projectile = Game.Prototypes.Projectiles.CannonTowerShot.Clone();
		projectile.SetZ(8);
		if (gameObject.name.Contains("Hellfire"))
		{
			projectile.AddComponent<HellfireShellBehaviour>();
		}
		else if (gameObject.name.Contains("Poison"))
		{
			projectile.AddComponent<PoisonShellBehaviour>();
		}
		else
		{
			projectile.AddComponent<ShellBehaviour>();
		}
		rotating = false;
		
		
	}

	/// <summary>
	/// Gets the target.
	/// </summary>

	protected GameObject GetTarget ()
	{
		for(int i = 0; i < Enemy.all.Count ; i++)
		{		
			if (TargetValidation(Enemy.all[i])){
				target = Enemy.all[i];
				return Enemy.all[i];
			}
		}
		
		return null;
	}

	void  Rotate ()
	{
		Quaternion rotation = gameObject.transform.localRotation;
		Vector3 rotor = rotation.eulerAngles;
		float targetAngle = (target.transform.position - transform.position).GetAngle() + 90;
		float objectAngle = rotor.z;
		float angleToTarget = Mathf.DeltaAngle(targetAngle, objectAngle);
		
		if (Mathf.Abs(angleToTarget) < 170f)
		{
			rotating = true;
		
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
		}
		else
		{
			rotating = false;
		}
	}
	

	void Update () {
		
		if (target == null)
		{
			GetTarget ();
		}
	
		
		if (target != null)
		{
			Rotate ();
				
		}
		
		
		if (cooldown <= 0 && target != null && !target.GetComponent<Enemy>().isDead){
		
			currDamage = type.projectileDamage * auraBonusDamage;
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			
			if (temp.magnitude < type.range && !rotating){
			
				projectile.SetActive(true);
				foreach(GameObject muzzle in muzzleList)
				{
					GameObject rocket = projectile.Clone();
					rocket.transform.position = muzzle.transform.position;
					rocket.GetComponent<ShellBehaviour>().target = target;
					rocket.GetComponent<ShellBehaviour>().origin = gameObject;
					rocket.GetComponent<ShellBehaviour>().currDamage = currDamage;
				}
				cooldown = type.cooldownMax * auraBonusSpeed ;
				
				if (gameObject.GetComponent<exSpriteAnimation>() != null)
				{
					gameObject.GetComponent<exSpriteAnimation>().PlayDefault();
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
		if (target != null && target.GetComponent<Enemy>().isDead)
		{
			target = null;
		}
		
		
	}	
}