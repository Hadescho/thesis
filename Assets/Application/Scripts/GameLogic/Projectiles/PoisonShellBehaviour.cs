using UnityEngine;
using System.Collections;

public class PoisonShellBehaviour : ShellBehaviour {
	
	public new class Config : ShellBehaviour.Config
	{
		public float dotDamage = 13f;
		public float dotTime = 5f;
	}
	public static new Config config = new Config();
	
	void Start()
	{
		positionCalculated = false;
	}
	
	void Update()
	{
		if (!positionCalculated && target != null)
		{
			temp = (target.transform.position - gameObject.transform.position).normalized;
			tempObj = new GameObject();
			tempObj.transform.position = target.transform.position;
			positionCalculated = true;
		}
		if (positionCalculated)
		{
			MoveProj();
		}
	}
	
	public override void Explode()
	{
		Vector3 explosionCenter = gameObject.transform.position;
		foreach(GameObject enemy in Enemy.all)
		{
			if ((enemy.transform.position - explosionCenter).magnitude < config.explosionRadius && !enemy.GetComponent<Enemy>().isFlying)
			{
				enemy.GetComponent<Enemy>().Damage(currDamage);
				DoTBehaviour.Set(enemy);
			}
		}
		Destroy (gameObject);
	}
}
