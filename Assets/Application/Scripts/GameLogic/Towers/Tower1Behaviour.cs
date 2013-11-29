using UnityEngine;
using System.Collections;

public class Tower1Behaviour : TowerBehaviour {
	
	public new class Config  : TowerBehaviour.Config
	{
		public float radius = 300; 
		public float cooldownMax = 1;
		public float price = Currency.Prices.Tower1Price;
		public float projectileDamage = Game.Towers.Damage.Tower1;
	}
	
	public static new Config config = new Config();
	public bool rotating;
	public bool muzzleBool;
	
	public GameObject target;
	public GameObject muzzle1;
	public GameObject muzzle2;
	
	
	new void Start () 
	{
		base.Start();
		cooldown = 0.0f;
		target = null;
		muzzle1 = gameObject.FindChild("TowerMuzzle1");
		muzzle2 = gameObject.FindChild("TowerMuzzle2");
		currDamage = Game.Towers.Damage.Tower1;
		auraBonusSpeed = 1;
		//type.targetingAirUnits = false;
		
	}

	/// <summary>
	/// Gets the target.
	/// </summary>

	protected GameObject GetTarget ()
	{
		for(int i = 0; i < Enemy.all.Count ; i++)
		{
			if(AirUnitAtack(Enemy.all[i],type))
			{
				Vector3 temp = gameObject.transform.position  - Enemy.all[i].transform.position;
					
				if (temp.magnitude < type.range)
				{
					target = Enemy.all[i];
					return Enemy.all[i];
					//Debug.Log(muzzle1.transform.position);
					//Debug.Log (muzzle2.transform.position);
				}
			}
		}
		
		return null;
	}
	

	void Update () {
		
		
		if (!rotating && target == null)
		{
			GetTarget ();
		}
	
		
		if (target != null)
		{
				Quaternion rotation = gameObject.transform.localRotation;
				Vector3 rotor = rotation.eulerAngles;
				float targetAngle = (target.transform.position - transform.position).GetAngle() - 90;
				float objectAngle = rotor.z;
				float angleToTarget = Mathf.DeltaAngle(targetAngle, objectAngle);
			
				if (Mathf.Abs(angleToTarget) > 0.2f)
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
				}
		}
		
		//Debug.Log((target.transform.position - gameObject.transform.position));
		if (cooldown <= 0 && target != null && !target.GetComponent<Enemy>().isDead){
		
			currDamage = Game.Towers.Damage.Tower1 * auraBonusDamage;
			Vector3 temp = gameObject.transform.position  - target.transform.position;
			
			
			
				
			if (temp.magnitude < type.range && !rotating){
				GameObject rocket = Game.Prototypes.Projectiles.Tower1shot.Clone();
				switch(muzzleBool)
				{
				case true:
					rocket.transform.position = muzzle1.transform.position;
					break;
				case false:
					rocket.transform.position = muzzle2.transform.position;
					break;
				}
				rocket.AddComponent<RocketBehaviour>().target = target;
				rocket.GetComponent<RocketBehaviour>().origin = gameObject;
				rocket.GetComponent<RocketBehaviour>().currDamage = currDamage;
				cooldown = type.cooldownMax * auraBonusSpeed ;
				muzzleBool = !muzzleBool;
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
		Destroy (gameObject);
		
		
	}
	
	public static void FloatingTextSpawn(float valueSpent,GameObject pos)
	{
		FloatingText floatie = Game.Prototypes.FloatingTexts.Cash.Clone(pos.transform.position);
		floatie.SetText("+" + valueSpent.ToString());
		floatie.Speed = new Vector3(0,1.5f,0);	
	}
		
	
}

