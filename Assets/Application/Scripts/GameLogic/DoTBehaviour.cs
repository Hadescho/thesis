using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoTBehaviour : SiriusBehaviour {
	
	
	public class Config
	{
		public float defaultDamage = 35.0f;
		public float defaultLife = 3.0f;
	}
	
	private const float _defaultDamage = 35.0f;
	private const float _defaultLife = 3.0f;
	
	public float currDamage;
	public float currLife;
	
	public static Config config = new Config();
	
	
	void Start () 
	{
		gameObject.GetComponent<exSprite>().color= new Color(0.172f,0.686f,0.364f,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (currLife > 0)
		{
			gameObject.GetComponent<Enemy>().Damage(Mathf.Round(currDamage*Time.deltaTime));
			currLife -= Time.deltaTime;
			
		}
		else
		{
			Destroy(this);
		}
	}
	
	private void Setter(float dmg = _defaultDamage, float life = _defaultLife)
	{
//			DoTBehaviour [] dotBehs = gameObject.GetComponents<DoTBehaviour>();
//			List<DoTBehaviour> list = dotBehs.OfType<DoTBehaviour>().ToList();
//			if(list.Count > 1)
//			{
//				Destroy(this);
//			}
		currDamage = dmg;
		currLife = life;
	}
	
	public static void Set(GameObject target, float dmg = _defaultDamage, float life = _defaultLife)
	{
		if (target.GetComponent<DoTBehaviour>() == null && !target.GetComponent<Enemy>().isDead)
		{
			target.AddComponent<DoTBehaviour>().Setter(dmg,life);
		}
		if (target.GetComponent<DoTBehaviour>() != null)
		{
			target.GetComponent<DoTBehaviour>().currLife = life;
		}
	}
		
	void OnDestroy()
	{
		gameObject.GetComponent<exSprite>().color= new Color(1.0f,1.0f,1.0f,1.0f);
	}
}
