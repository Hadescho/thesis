using UnityEngine;
using System.Collections;

public class HellfireArea : MonoBehaviour {
	
	public class Config
	{
		public float range = 68;
		public float maxLife = 17f;
		public float damage = 30f;
	}
	public static Config config = new Config();
	
	private float _Life;
	
	// Use this for initialization
	void Start () 
	{
		_Life = config.maxLife;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_Life > 0)
		{
			foreach(GameObject enemy in Enemy.all)
			{
				if (Game.InRange(enemy,gameObject,config.range) && !enemy.GetComponent<Enemy>().isFlying)
				{
					enemy.GetComponent<Enemy>().Damage(config.damage * Time.deltaTime);
					
				}
			}
			_Life -= Time.deltaTime;
		}
		else
		{
			Destroy (gameObject);
		}
	
	}
	
	public static void Spawn(Vector3 position)
	{
		GameObject obj = Game.Prototypes.Extras.FireAoE.Clone();
		obj.GetComponent<exSpriteAnimation>().Play("Fire",config.maxLife);
		obj.AddComponent<HellfireArea>();
		obj.transform.position = TowerSpawn.Grid(position,-0.1f);
		GameObject nearestBlocker = null;
		float nearestBlockerMagnitude = 13370;
		foreach(GameObject blocker in TowerSpawn.allBuildBlockers)
		{
			if ((blocker.transform.position - obj.transform.position).magnitude < 16)
			{
				Debug.Log ("Na mqsto");
				nearestBlocker = null;
				break;
			}
			else
			{
				if((blocker.transform.position - obj.transform.position).magnitude < nearestBlockerMagnitude)
				{
					nearestBlocker = blocker;
					nearestBlockerMagnitude = (blocker.transform.position - obj.transform.position).magnitude;
				}
			}
			
		}
		
		if (nearestBlocker != null)
		{
			obj.transform.position = new Vector3(nearestBlocker.transform.position.x,nearestBlocker.transform.position.y,-0.1f);
		}
	}
}
