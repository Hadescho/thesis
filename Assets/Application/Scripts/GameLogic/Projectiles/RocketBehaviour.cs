using UnityEngine;
using System.Collections;

public class RocketBehaviour : SiriusBehaviour {
	
	public class Config
	{
		public float hitCircleRadius = 30;
		public float projectileSpeed = 300;
		public float projectileDamage = Game.Towers.Damage.Tower1;
		public float explosionRadius = 120;
	}
	
	
	Vector3 lastPos;
	Vector3 lastCheck;
	public GameObject target;
	public GameObject origin;
	public static Config config = new Config();
	public float currDamage;
	public float timeshift;
	public bool attackFlying;
	
	void Start () 
	{
		Game.allProjectiles.Add (gameObject);
	}
	
	void Update () 
	{
		MoveProj();
	}

	void Rotate (Vector3 temp)
	{
		Quaternion rotation = gameObject.transform.rotation;
		Vector3 rotor = rotation.eulerAngles;
		rotor.z = temp.GetAngle();
		rotation.eulerAngles = rotor;
		gameObject.transform.rotation = rotation;
	}
	
	
	protected void MoveProj()
	{	
		Vector3 temp;
		if (target != null)
		{
			temp = (target.transform.position - gameObject.transform.position).normalized;
			lastPos = target.transform.position;
		}
		else
		{
			temp = (lastPos - gameObject.transform.position).normalized;
		}
			
		gameObject.transform.position += temp * Time.deltaTime * config.projectileSpeed;
		Rotate (temp);
		
		if (((gameObject.transform.position - lastPos).magnitude) <= config.hitCircleRadius)
		{
			Explode ();
		}
		if (gameObject.transform.position == lastCheck)
		{
			Explode ();	
		}
		lastCheck = gameObject.transform.position;
		
	}
	
	public virtual void Explode()
	{
		Vector3 explosionCenter = gameObject.transform.position;
		for(int i = 0; i < Enemy.all.Count; i++)
		{
			if ((Enemy.all[i].transform.position - explosionCenter).magnitude < config.explosionRadius && (attackFlying || !Enemy.all[i].GetComponent<Enemy>().isFlying ))
			{
				Enemy.all[i].GetComponent<Enemy>().Damage(currDamage);
			}
		}
		Destroy (gameObject);
	}
	
	void OnDestroy()
	{
		Game.allProjectiles.Remove(gameObject);
	}
}
