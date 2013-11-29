using UnityEngine;
using System.Collections;

public class AirDropBehaviour : SiriusBehaviour {
	
	public class Config
	{
		public float airPlaneSpeed=300;
		public float AirResistance = 0.9f;
		public float Gravity = 0.14f;
		public float turretTime=10.0f;
		public float energyCost=10.0f;
		public float prachuteScale=0.15f;
		public float airDropCooldown=15.0f;
		
	}
	
	public static Config config = new Config();
	
	public Vector3 start;
	public Vector3 end;
	private Vector3 dropPos;
	
	private Vector3 scale;
	
	GameObject currPlane;
	GameObject turret;
	GameObject parachute;
	
	public static bool played;
	private bool getAirDropCoord;
	private bool calcCoord;
	private bool dropTurret;
	private bool dropAffirmative;
	private bool fallAffirmative;
	private bool skipInitialFrame;
	
	public float _dist;
	public float _distToMark;
	public float _neededDist;
	public float _moveDist;
	
	public int towerNumber;
	
	public static Vector3 traecory;	
	public Vector3 tragectory;
	
	public static GameObject spriteObject;
	
	public static float cooldown;
	
	void Start ()
	{
		TowerSpawn.instance.enabled = false;
		currPlane=Game.Prototypes.Extras.AirPlanel.Clone();
		turret=Game.Prototypes.Towers.AirDropTower.Clone();
		parachute=Game.Prototypes.Extras.Parachute.Clone();
		spriteObject = GameObject.Find ("/Cursors/ArtilleryStrikeCursor").Clone();
		spriteObject.SetActive(false);
		
		_neededDist=0.0f;
		
		calcCoord=false;
		dropTurret=false;
		fallAffirmative=false;
		getAirDropCoord=true;
		dropAffirmative=true;
		skipInitialFrame=false;
		
		cooldown= -1.0f;
	}
	
	
	void Update ()
	{
		DropCooldown();
		if(getAirDropCoord && !TowerSpawn.redCursorBool && played && skipInitialFrame)
		{
			cooldown=config.airDropCooldown;
			GetAirDropCoord();
			skipInitialFrame=false;
		}
		if(calcCoord)
		{
			CalcAirDropCoord();
		}
		if(dropTurret)
		{
			spriteObject.SetActive(false);
			MovePlane();
			SpawnDropTower();
		}
		if(fallAffirmative)
		{
			DropTower();
			//MovePlane();
		}
		if(played)
		{
			skipInitialFrame=true;	
		}
		
	}
	
	public static void DropCooldown()
	{
		if(cooldown>0)
		{
			cooldown-=Time.deltaTime;
		}
	}
	
	public void DropTower()
	{
		if(parachute.transform.position.z<0.0f)
		{
			parachute.transform.position+=tragectory*Time.deltaTime;
			
			tragectory*=config.AirResistance*Time.deltaTime;
			tragectory.z+=config.Gravity;	
			
			scale=parachute .transform.localScale;
			scale.x-=config.prachuteScale*Time.deltaTime;
			scale.y-=config.prachuteScale*Time.deltaTime;
			parachute.transform.localScale=scale;
		}
		else
		{	
			turret.SetZ(-1.0f);	
			turret.AddComponent<AirDropTowerBehaviour>();
			turret.transform.position=dropPos;
			TowerSpawn.posList.Add(dropPos);
			towerNumber=TowerSpawn.allTowers.Count-1;
			
			parachute .transform.localScale=scale;
			Sirius.ExecAfter(config.turretTime , DestroyTurret);
			fallAffirmative=false;
		}
	}
	

	public void SpawnDropTower()
	{
		if(_neededDist>_distToMark && dropAffirmative)
		{
			parachute.transform.position=dropPos;
			fallAffirmative=true;
			//TowerSpawn.Spawn(dropPos ,turret);
			dropAffirmative=false;
		}
	}
	
	public void MovePlane()
	{
		if(_dist>0.0f)
		{
			currPlane.transform.position+=traecory*Time.deltaTime;
			_dist-=_moveDist*Time.deltaTime;
			_neededDist+=_moveDist*Time.deltaTime;
		}	
		else
		{
			dropTurret=false;
			getAirDropCoord=true;
			dropAffirmative=false;
			
			currPlane.SetActive(false);
			played=false;
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.AirDropIcon.name).SetSprite(Game.Prototypes.Extras.AirDropIcon.name);
			
		}
	}
	
	
	public void GetAirDropCoord()
	{
		//temp
		if(Input.GetMouseButton(0))
		{
			MarkSetter();
		}
		//
		if (Input.GetMouseButtonUp(0))
		{
			start=Game.GetMouseCoord();
			foreach (GameObject blocker in TowerSpawn.allBuildBlockers)
			{
				if (Game.InRange(blocker,start,64))
				{
					played = false;
					spriteObject.SetActive(false);
					cooldown=-1.0f;
					return;
					
				}
			}
			foreach (GameObject tower in TowerSpawn.allTowers)
			{
				if(Game.InRange(tower,start,64))
				{
					played = false;
					spriteObject.SetActive(false);
					cooldown=-1.0f;
					return;
					
				}
			}
			dropPos=start;
			end=RandomEnd();
			
			calcCoord=true;
			getAirDropCoord=false;
		}	
	}
	
	
	public void CalcAirDropCoord()
	{
		Vector3 vec;
		vec=end - start;
		
		vec.Normalize();
		while(!CheckBorder(start))
		{
			start-=vec;
		}
		while(!CheckBorder(end))
		{
			end+=vec;
		}
		vec=end-start;
		traecory =vec;	
		
		start.z = -25.0f;
		
		currPlane.SetActive(true);
		currPlane.transform.position=start;
		RotatePlane(start,end);
		
		_dist=Distance2D(end, start);
		_distToMark=Distance2D(dropPos, start);
		
		traecory.Normalize();
		traecory*=config.airPlaneSpeed;
		
		tragectory=traecory;
		
		_moveDist=traecory.magnitude;
		
		dropTurret=true;
		dropAffirmative=true;
		calcCoord=false;
		RosetteBehaviour.Unblock();
		BuyTowerBox.UnblockTowerSpawn();

	}
	
	public void DestroyTurret()
	{
		TowerSpawn.DestroyInPosList(dropPos);
		turret.SetActive(false);
	}
	
	public bool CheckBorder(Vector3 currVec)
	{
		if(currVec.x>650.0f || currVec.x<-650.0f || currVec.y>360.0f|| currVec.y<-360.0f)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
	
	public Vector3 RandomEnd()
	{
		Vector3 randomHit;
		randomHit=new Vector3 (Random.Range(start.x-100, start.x+100),Random.Range(start.y-100 ,start.y+100) , -5.0f);
		return randomHit ;
	}
	
	
	public void RotatePlane(Vector3 start , Vector3 end)
	{
		currPlane.transform.rotation = Quaternion.Euler(0.0f, 0.0f,(end-start).GetAngle()-90.0f);
	}
	
	
	public float Distance2D(Vector3 start , Vector3 end)
	{
		float x ,y;
		float distance;
		
		x=end.x-start.x;
		y=end.y-start.y;
		
		distance=Mathf.Sqrt(x*x + y*y);
		
		return distance;
	}

	private static void MarkSetter()
	{
		Vector3 position = Game.GetMouseCoord();	
		spriteObject.SetActive(true);	
		spriteObject.transform.position = new Vector3(position.x,position.y,-6);
	}
}
