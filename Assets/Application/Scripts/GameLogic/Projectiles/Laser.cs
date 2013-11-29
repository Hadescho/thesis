using UnityEngine;
using System.Collections;

public class Laser : SiriusBehaviour {
	
	public class Config
	{
		
		public float recoilVar = 10;
		public float hitCircleRadius = 20;
		public float projSpeed = 750;
		public float damage = 3;
	}
	public static Config config;
	
	public GameObject target;
	public GameObject origin;
	Vector3 targetPos; // The position of the target
	public float currDamage;
	//Timeshift
	public float timeshift; // Coefficient
	//private float _timeshiftLife;
	//private bool _timeshiftActive;
	
	
	
	void Start () 
	{
		config = new Config();
		targetPos = target.transform.position;
		//Recoil
		Vector2 recoiler = Random.insideUnitCircle * config.recoilVar;
		targetPos.x += recoiler.x;
		targetPos.y += recoiler.y;
		Game.allProjectiles.Add (gameObject);
		timeshift = 1;
	}
	
	void Update () 
	{
		MoveProj();
		
		//------
		
	}
	
	
	void MoveProj()
	{
		if (target == null)
		{
			if (((gameObject.transform.position - targetPos).magnitude) > config.hitCircleRadius)
			{
				
				Vector3 temp = (targetPos - gameObject.transform.position).normalized;
				
				gameObject.transform.position += temp * Time.deltaTime * config.projSpeed * timeshift;
				
				Quaternion rotation = gameObject.transform.rotation;
				Vector3 rotor = rotation.eulerAngles;
				rotor.z = temp.GetAngle();
				rotation.eulerAngles = rotor;
				gameObject.transform.rotation = rotation;
			}
			else
			{
				Destroy (gameObject);
			}	
		}
		if (target != null)
		{
			if ((gameObject.transform.position - targetPos).magnitude > config.hitCircleRadius)
			{
				//Position
					
					
				Vector3 temp = (targetPos - gameObject.transform.position).normalized;
				gameObject.transform.position += temp * Time.deltaTime * config.projSpeed;
				
				//Rotation
				Quaternion rotation = gameObject.transform.rotation;
				Vector3 rotor = rotation.eulerAngles;
				rotor.z = temp.GetAngle();
				rotation.eulerAngles = rotor;
				gameObject.transform.rotation = rotation;
				
			}
			else
			{
				if(origin != null && origin.name == "Tower 5")
				{
					DoTBehaviour.Set(target);
				}
				target.GetComponent<Enemy>().Damage(currDamage);
				Destroy (gameObject);
			}
		}
	}
	
	public void Timeshift(float val)
	{
		timeshift = val;
		//_timeshiftActive = true;
		//_timeshiftLife = Game.Consts.TimeshiftUpdateProofLife;
	}
	
//	public void TimeshiftActive()
//	{
//		if (_timeshiftActive && _timeshiftLife >= 0)
//		{
//			
//		}
//	}
	
	void OnDestroy()
	{
		Game.allProjectiles.Remove(gameObject);
	}
}