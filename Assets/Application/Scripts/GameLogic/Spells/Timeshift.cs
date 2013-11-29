using UnityEngine;
using System.Collections;

public class Timeshift : SiriusBehaviour {
	
	public class Config
	{
		public float range = 300;
		public float coefficient = 0.5f;
		public float time = 5.0f;
		public float energyPrice = 30f;
		public float energyCost=10.0f;
	}
	
	public static bool _active;
	private static Vector3 _pos;
	private static float _timeLeft;
	public static Config config = new Config();
	public static Timeshift instance;
	public static GameObject obj;
	public static GameObject spriteObject;
	
	public static bool timeshiftBool;
	
	void Awake()
	{
		spriteObject = GameObject.Find ("/Cursors/EMPCursor");
		spriteObject.GetComponent<exSprite>().color = new Color(0.2f,0.2f,1);
		Vector2 temp = new Vector2(config.range / 60, config.range / 60);
		spriteObject.GetComponent<exSprite>().scale = temp;
		spriteObject.SetActive(false);
	}
	
	void Start () 
	{
		timeshiftBool=false;
		
	}
	
	void Update () 
	{
		if(timeshiftBool)
		{
			GetTimeshiftCoord();
			MarkSetter();
			
		}
		if(!_active && !timeshiftBool)
		{
			Check();	
		}
		if (_active && _timeLeft > 0)
		{
			foreach(GameObject enemy in Enemy.all)
			{
				if (Game.InRange(gameObject,enemy,config.range))
				{
					enemy.GetComponent<Enemy>().Slow(config.coefficient,0.1f,false);
				}
			}
			_timeLeft -= Time.deltaTime;
		}
		else if (_timeLeft <= 0)
		{
			_active = false;
			Destroy(obj);
		}
	}
	
	public static void Cast(Vector3 position)
	{
		if (_active)
		{
			Destroy(obj);
			_active = false;
			timeshiftBool=false;
		}
		if (Energy.UseEnergy(config.energyPrice)){
			_active = true;
			_timeLeft = config.time;
			obj = new GameObject();
			obj.AddComponent<Timeshift>();
			obj.transform.position = position;
			Game.towerSpawnUpdaterBool = true;
		}
	}
	
	
	public void Check()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
		
			if(!Physics.Raycast(ray, out hit))
			{
			}
			else
			{	
				if (hit.transform.gameObject.name == Game.Prototypes.Extras.Timeshift.name)
				{
					if(Energy.UseEnergy(config.energyCost))
					{
						RosetteBehaviour.Block();
						_active=true;
						Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.Timeshift.name).SetSprite(Game.Prototypes.Extras.Timeshift.name + "Clicked");
						timeshiftBool=true;
					}
				}
			}
		}
	}
	
	public void GetTimeshiftCoord()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Cast(Game.GetMouseCoord());
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.Timeshift.name).SetSprite(Game.Prototypes.Extras.Timeshift.name);
			RosetteBehaviour.Unblock();
			BuyTowerBox.UnblockTowerSpawn();
		}
	}
	private static void MarkSetter()
	{
		Vector3 position = Game.GetMouseCoord();
		spriteObject.SetActive(true);
			
		spriteObject.transform.position = new Vector3(position.x,position.y,-6);
			
		if(Input.GetMouseButtonUp(0))
		{
			Sirius.ExecAfter(config.time, ()=>
			{
				spriteObject.SetActive(false);
			}
			);	
		}
	}
	

	void OnDestroy()
	{
		_active = false;
		Destroy(obj);
	}
}
