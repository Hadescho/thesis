using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirStrikeBehaviour : SiriusBehaviour
{
	public class Config
	{
		public float airPlaneSpeed=300;
		public float bombTimer=0.25f;
		public float energyCost=10.0f;
		public float spawnBombTime=0.25f;
		public float airStrikeCooldown=15.0f;
	}
	
	public static Config config = new Config();
	public static bool playedAir;
	public static bool blockSecondCast;
	public static bool blockTowerSpawn;
	
	private static Vector3 start;
	private static Vector3 end;
	
	private bool startBombarding;
	private bool calcCoord;
	private bool skipInitialFrame;
	
	public float _dist;
	public float _moveDist;
	public static Vector3 traecory;
	
	public static GameObject currPlane;
	public GameObject stroke;
	public GameObject cursor ;
	
	GameObject currBomb;
	public static List<GameObject> allBombs=new List<GameObject>();
	
	public static float cooldown;
	
	void Awake()
	{
		
	}
	
	void Start ()
	{
		playedAir=false;
		blockSecondCast=false;
		skipInitialFrame=false;
		
		startBombarding=false;
		calcCoord=false;
		
		currPlane=Game.Prototypes.Extras.AirPlanel.Clone();
		stroke=Game.Prototypes.Extras.AirStrikeStroke.Clone ();
		stroke.transform.position = new Vector3(stroke.transform.position.x,stroke.transform.position.y,1);
		cursor= Game.Prototypes.Extras.AirStrikeCursor.Clone();
		cursor.transform.position = new Vector3(cursor.transform.position.x,cursor.transform.position.y,1);
		
		cooldown=-1.0f;
	}
	
	
	void Update () 
	{
		Game.DropSpellCooldown(ref cooldown);
		
		if(playedAir && skipInitialFrame)
		{
			cooldown=config.airStrikeCooldown;
			GetAirStrikeCoord();
			skipInitialFrame=false;
		
		}
		if(calcCoord)
		{
			CalcAirStrikeCoord();
			calcCoord=false;
		}
		if(startBombarding )
		{
			MovePlane();
			SpawnBombs();
		}
		if(playedAir)
		{
			skipInitialFrame=true;
		}
	}

	public void MovePlane()
	{
		if(_dist>0.0f)
		{
			currPlane.transform.position+=traecory*Time.deltaTime;
			_dist-=_moveDist*Time.deltaTime;
		}	
		else
		{
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.AirStrike.name).SetSprite(Game.Prototypes.Extras.AirStrike.name);
			startBombarding=false;
			currPlane.SetActive(false);
			blockSecondCast=false;
		}
	}
	
	
	public void GetAirStrikeCoord()
	{
		if (Input.GetMouseButtonDown(0))
		{
			start=Game.GetMouseCoord();
			Debug .Log (start);
		}
		if (Input.GetMouseButton(0))
		{
			DrawTrajectory(Game.GetMouseCoord());
		}
		if (Input.GetMouseButtonUp(0))
		{
			
			end=Game.GetMouseCoord();
			end.x+=0.1f;
			end.y+=0.1f;
			//DrawTrajectory(end);
			Debug .Log (end);
			calcCoord=true;
			playedAir=false;
			RosetteBehaviour.Unblock();
			BuyTowerBox.UnblockTowerSpawn();
			
			
			cursor.SetZ(0.09f);
			stroke.SetZ(0.09f);
		}	
	}
	
	
	public void CalcAirStrikeCoord()
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
		
		currPlane.transform.position=start;
		currPlane.SetActive(true);
		RotatePlane(start,end);
		
		_dist=(end-start).magnitude;
		
		traecory.Normalize();
		traecory*=config.airPlaneSpeed;
		
		_moveDist=traecory.magnitude;

		startBombarding=true;
		calcCoord=false;
		
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
	
	
	public void RotatePlane(Vector3 start , Vector3 end)
	{
		currPlane.transform.rotation = Quaternion.Euler(0.0f, 0.0f,(end-start).GetAngle()-90.0f);
		// (end1-start1).GetAngle()   Vector3.Angle(start, end)
		//currPlane.transform.rotation = Quaternion.FromToRotation(start1,end1);
	}
	public void RotateStroke(Vector3 start , Vector3 end)
	{
		stroke.transform.rotation = Quaternion.Euler(0.0f, 0.0f,(end-start).GetAngle());

	}
	public void RotateStrokeHead(GameObject head , Vector3 start , Vector3 end)
	{
		head.transform.rotation = Quaternion.Euler(0.0f, 0.0f,(end-start).GetAngle());

	}
	
	public void SpawnBombs()
	{
		if(config.bombTimer>0)
		{
			config.bombTimer -=Time.deltaTime;
		}
		else
		{
			currBomb=Game.Prototypes .Extras.Bomb.Clone();
			currBomb.AddComponent<AirStrikeBombBehaviour>();
			allBombs .Add (currBomb);
			config .bombTimer =config.spawnBombTime;
		}
	}
	public void DrawTrajectory(Vector3 endCursor)
	{
		Vector3 curStart=start;	
		RotateStroke(curStart , endCursor);
		
		//spawn and rotate stroke
		cursor.transform.position=endCursor;
		RotateStrokeHead(cursor , curStart , endCursor);
		cursor.SetZ(-0.09f);		
		stroke.transform.position=curStart;
		stroke.SetZ(-0.09f);
		RotateStroke(curStart , endCursor);
		
		//scale
		Vector3 temp = curStart - endCursor;
		Vector3 scale = stroke.transform.localScale;
		scale.x = temp.magnitude/250 ;
		stroke.transform.localScale = scale;
	}
}
