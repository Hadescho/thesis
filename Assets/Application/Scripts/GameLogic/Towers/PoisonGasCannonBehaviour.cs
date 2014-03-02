using UnityEngine;
using System.Collections;

public class PoisonGasCannonBehaviour : Tower1Behaviour {


	//public new GameObject muzzle1;
	protected GameObject projectile;
	
	
	new void Start () 
	{
		base.Start();
		cooldown = 0.0f;
		target = null;
		muzzle1 = gameObject.FindChild("TowerMuzzle");
		currDamage = Game.Towers.Damage.Tower1;
		auraBonusSpeed = 1;
//		type.targetingAirUnits = false;
		projectile = Game.Prototypes.Projectiles.CannonTowerShot.Clone();
		projectile.SetZ(10);
//		if (gameObject.name.Contains("Hellfire"))
//		{
//			projectile.AddComponent<HellfireShellBehaviour>();
//		}
//		else if (gameObject.name.Contains("Poison"))
//		{
//			projectile.AddComponent<PoisonShellBehaviour>();
//		}
//		else
//		{
//			projectile.AddComponent<ShellBehaviour>();
//		}
		rotating = false;
		
		
	}

	/// <summary>
	/// Gets the target.
	/// </summary>

	protected new GameObject GetTarget ()
	{
		for(int i = 0; i < Enemy.all.Count ; i++)
		{
			Vector3 temp = gameObject.transform.position  - Enemy.all[i].transform.position;
				
			if (temp.magnitude < type.range && AirUnitAtack(Enemy.all[i],type)){
				target = Enemy.all[i];
				return Enemy.all[i];
			}
		}
		
		return null;
	}

	void Rotate()
	{
		Quaternion rotation = gameObject.transform.localRotation;
		Vector3 rotor = rotation.eulerAngles;
		float targetAngle = (target.transform.position - transform.position).GetAngle () + 90;
		float objectAngle = rotor.z;
		float angleToTarget = Mathf.DeltaAngle (targetAngle, objectAngle);
		if (Mathf.Abs (angleToTarget) > 2f) {
			//rotating = true;
			if (angleToTarget < 0) {
				rotor.z -= 150.0f * Time.deltaTime * timeshift;
			}
			else {
				rotor.z += 150.0f * Time.deltaTime * timeshift;
			}
			rotation.eulerAngles = rotor;
			gameObject.transform.rotation = rotation;
		}
		else {
			rotating = false;
		}
	}	

	void Update () {
		
		if (!rotating && target == null)
		{
			GetTarget ();
		}
	
		
		if (target != null)
		{
				Rotate ();
				
				
		}
		
		
		if (cooldown <= 0 && target != null && !target.GetComponent<Enemy>().isDead){
		
			currDamage = Game.Towers.Damage.Tower1 * auraBonusDamage;
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			
			
			
				
			if (temp.magnitude < type.range && !rotating){
			
				projectile.SetActive(true);
				GameObject rocket = projectile.Clone();
				rocket.transform.position = muzzle1.transform.position;
				rocket.AddComponent<PoisonShellBehaviour>().target = target;
				rocket.GetComponent<PoisonShellBehaviour>().origin = gameObject;
				rocket.GetComponent<PoisonShellBehaviour>().currDamage = currDamage;
				cooldown = type.cooldownMax * auraBonusSpeed ;
				gameObject.GetComponent<exSpriteAnimation>().PlayDefault();
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
//		
//	}
	
	
	public override void Sell()
	{
		float val = Currency.Prices.Tower1Price * 0.75f;
		Currency.GiveCurrency(val);
		FloatingTextSpawn(val,gameObject);
		TowerSpawn.DestroyInPosList(gameObject.transform.position);
		TowerSpawn.RemoveTower(gameObject);
		Destroy (gameObject);
		
		
	}
	
	/*public static void FloatingTextSpawn(float valueSpent,GameObject pos)
	{
		FloatingText floatie = Game.Prototypes.FloatingTexts.Cash.Clone(pos.transform.position);
		floatie.SetText("+" + valueSpent.ToString());
		floatie.Speed = new Vector3(0,1.5f,0);	
	}*/
	
}
