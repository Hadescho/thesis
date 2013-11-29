using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingBehaviour : SiriusBehaviour {
	
	public class Config
	{
		public float range = 280;
		public float animWidth = 100;
		public float damage = 200f;
		public float life = 0.75f;
		public float attTime = 0.2f;
	}
	
	public static Config config = new Config();
	public float currDamage;
	public float currAttTime;
	public bool secondLighting;
	public GameObject firstLighting;
	public float timeshift;
	
	
	public GameObject target;
	public GameObject origin;
	
	
	protected void Awake () 
	{
		currAttTime = config.attTime;
		Game.allProjectiles.Add (gameObject);
	}
	
	void Start()
	{
		if (!secondLighting)
		{
			firstLighting = null;
		}
	}
	
	protected virtual void Update () 
	{
		if (target == origin)
		{
			Destroy(gameObject);
		}
		
		if (target != null && origin != null){
			if (Mathf.Abs((origin.transform.position - target.transform.position).magnitude) < config.range)
			{
				Vector3 temp = origin.transform.position - target.transform.position;
				//Scaling
				//Vector3 scale = gameObject.transform.localScale;
				gameObject.GetComponent<exSprite>().scale = new Vector2(temp.magnitude / config.animWidth,1);
					//.x = temp.magnitude / config.animWidth;
				//transform.localScale = scale;
				
				//Rotating
				Quaternion rotation = gameObject.transform.rotation;
				Vector3 rotor = rotation.eulerAngles;
				rotor.z = temp.GetAngle();
				rotation.eulerAngles = rotor;
				gameObject.transform.rotation = rotation;
				
				//Positioning
				gameObject.transform.position = (origin.transform.position + target.transform.position)/2;
				
				//Damage
//				if (origin.name == "Tower 6")
//				{
//					target.GetComponent<Enemy>().Slow(0.5f,5f);
//				}
				OnDamage();
					
			}
			else
			{
				Destroy(gameObject);
			}
		}
		else
		{
			Destroy(gameObject);
		}
		if(secondLighting || origin.GetComponent<Enemy>() != null)
		{
			if(!secondLighting && (target.GetComponent<Enemy>().isDead || origin.GetComponent<Enemy>().isDead))
			{
				Destroy (gameObject);
			}
		}
		if(secondLighting && firstLighting == null)
		{
			Destroy (gameObject);
		}
		
		
	
	}
	
	protected virtual void OnDamage()
	{
		if (currAttTime <= 0.0f)
		{
			target.GetComponent<Enemy>().Damage(Mathf.Round(currDamage * Time.deltaTime * (1/config.attTime)));
			currAttTime = config.attTime;
		}
		currAttTime -= Time.deltaTime * (timeshift + 1);
	}
	
	void OnDestroy()
	{
		
		//TowerBehaviour beh = origin.GetComponent<TowerBehaviour>();
	//	beh.GetType().GetField("lightingList").GetValue(new List<GameObject>());
		
//		if (origin.GetComponent<TeslaTowerBehaviour>() != null)
//		{
//			origin.GetComponent<TeslaTowerBehaviour>().firing = false;
//		}
		Game.allProjectiles.Remove(gameObject);
	}
}