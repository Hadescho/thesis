using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningBehaviour : SiriusBehaviour {
	
	public class Config
	{
		public float range = 150;
		public float animWidth = 100;
		public float damage = 200f;
		public float life = 0.25f;
		public float chainTimer = 0.125f;
	}
	
	public static Config config = new Config();
	public float currDamage;
	public float timeshift;
	public float chaining;
	
	
	public GameObject target;
	public GameObject origin;
	public List<GameObject> targetList = new List<GameObject>();
	
	protected void Awake () 
	{
		Game.allProjectiles.Add (gameObject);
	}
	
	void Start()
	{
		Debug.Log("Start(): " + gameObject.GetPath());
		OnDamage();
		Destroy(gameObject, config.life);
		targetList.Add(target);
		if (origin.GetComponent<TowerBehaviour>() != null)
		{
			chaining = origin.GetComponent<TowerBehaviour>().type.chaining;
		}
		Visualise ();
		Invoke("Chain",config.chainTimer);
	}

	void Visualise ()
	{
		if (target != null && origin != null)
		{
			Vector3 temp = origin.transform.position - target.transform.position;
			//Scaling
			gameObject.GetComponent<exSprite>().scale = new Vector2(temp.magnitude / config.animWidth,1);
		
			
			//Rotating
			Quaternion rotation = gameObject.transform.rotation;
			Vector3 rotor = rotation.eulerAngles;
			rotor.z = temp.GetAngle();
			rotation.eulerAngles = rotor;
			gameObject.transform.rotation = rotation;
			
			//Positioning
			gameObject.transform.position = (origin.transform.position + target.transform.position)/2;	
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	protected virtual void Update () 
	{
		
		Visualise ();
		
		
	
	}
	
	protected virtual void OnDamage()
	{
		target.GetComponent<Enemy>().Damage(Mathf.Round(currDamage));
	}
	
	protected void Chain()
	{
		if (chaining > 0)
			{
			foreach(GameObject enemy in Enemy.all)
			{
				if (Game.InRange(target,enemy,config.range) && !targetList.Contains(enemy))
				{
					GameObject newLightning = TeslaTowerBehaviour.Spawn(target,enemy);
					newLightning.GetComponent<LightningBehaviour>().chaining = chaining - 1;
					newLightning.GetComponent<LightningBehaviour>().currDamage = currDamage;
					newLightning.GetComponent<LightningBehaviour>().targetList = targetList;
					break;
				}
			}
		}
	}
	
	void OnDestroy()
	{
		Game.allProjectiles.Remove(gameObject);
	}
}