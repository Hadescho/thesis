using UnityEngine;
using System.Collections;

public class CryoRocketBehaviour : RocketBehaviour {

	// Use this for initialization
	
	public new class Config : RocketBehaviour.Config
	{
		public float slowTime = 1.5f;
		public float slowEfficiency = 0.5f;
	}
	public new static Config config = new Config();
	public float currentExplosionRadius;
	
	void Start () 
	{
		Game.allProjectiles.Add (gameObject);
		currentExplosionRadius = config.explosionRadius;
	}
	
	// Update is called once per frame
	void Update () {
		MoveProj();
	}
	
	public override void Explode()
	{
		Vector3 explosionCenter = gameObject.transform.position;
		for(int i = 0; i < Enemy.all.Count; i++)
		{
			if ((Enemy.all[i].transform.position - explosionCenter).magnitude < currentExplosionRadius  && !Enemy.all[i].GetComponent<Enemy>().isFlying )
			{
				Enemy.all[i].GetComponent<Enemy>().Damage(currDamage);
				Enemy.all[i].GetComponent<Enemy>().Slow(config.slowEfficiency,config.slowTime);
			}
		}
		Destroy (gameObject);
	}
}