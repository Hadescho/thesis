using UnityEngine;
using System.Collections;

public class AirStrikeBombBehaviour : SiriusBehaviour 
{
	public class Config
	{
		public float AirResistance = 0.9f;
		public float Gravity = 0.07f;
		public float bombDamage=1000.0f;
		public float bombRadius=300.0f;
		public float bombDestroyTime=0.5f;
		public float bombsRandomize = 20.0f;
	}
	
	public static Config config = new Config();
	public Vector3 tragectory;
	private Vector3 scale;
	
	void Start () 
	{
		tragectory=AirStrikeBehaviour.traecory;
		gameObject.transform.position=RandomizedBombPosition();
		RotateBomb();
	}
	

	void Update ()
	{
		DropBomb();
	}
	
	void onDestroy()
	{
		AirStrikeBehaviour.allBombs.Remove(gameObject);
	}
	
	public void DropBomb()
	{
		if(gameObject.transform.position.z<0.0f)
		{
			gameObject.transform.position+=tragectory*Time.deltaTime;
			
			tragectory*=config.AirResistance*Time.deltaTime;
			tragectory.z+=config.Gravity;	
			
			scale=gameObject .transform.localScale;
			scale.x-=0.15f*Time.deltaTime;
			scale.y-=0.15f*Time.deltaTime;
			gameObject .transform.localScale=scale;
		}
		else
		{	
			gameObject.SetZ(-1.0f);
			
			scale.x=9.0f;
			scale.y=9.0f;
			gameObject .transform.localScale=scale;
			gameObject.GetComponent<exSpriteAnimation>().Play("Explosion");
			Game.Explode(gameObject.transform.position , config.bombRadius ,config.bombDamage);
			gameObject.DestroyAfter(config.bombDestroyTime);
			Destroy (this);
		}
		
	}
	public void RotateBomb()
	{
		gameObject.transform.rotation=AirStrikeBehaviour.currPlane.transform.rotation;
	}
	public Vector3 RandomizedBombPosition()
	{
		Vector3 temp=AirStrikeBehaviour.currPlane.transform.position;
		Vector3 pos =  new Vector3(Random.Range(temp.x-config.bombsRandomize*2.0f , temp.x+config.bombsRandomize*2.0f),Random.Range(temp.y-config.bombsRandomize,temp.y+config.bombsRandomize), temp.z);
		return pos;
	}
}
