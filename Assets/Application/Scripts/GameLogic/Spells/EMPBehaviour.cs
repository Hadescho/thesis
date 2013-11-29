using UnityEngine;
using System.Collections;

public class EMPBehaviour : MonoBehaviour {
	
	public Vector3 empPos;
	public bool stuned;
	public bool noStuned;
	public static bool played;
	public static GameObject spriteObject;
	
	public float timer;
	public class Config
	{
		public float radius=150.0f;
		public float stunTimer=3.0f;
		public float energyCost=10.0f;
		public float EMPcooldown=10.0f;
	}
	
	public static Config config = new Config();	
	public static float cooldown;
	
	void Awake()
	{
		spriteObject = GameObject.Find ("/Cursors/TimeshiftCursor");
		spriteObject.GetComponent<exSprite>().color = new Color(0.2f,1f,0.8f);
		Vector2 temp = new Vector2(config.radius / 60, config.radius / 60);
		spriteObject.GetComponent<exSprite>().scale = temp;
		spriteObject.SetActive(false);
	}
	
	void Start ()
	{
		stuned=false;
		noStuned=false;
		cooldown=-1.0f;
	}
	

	void Update () 
	{
		Game.DropSpellCooldown(ref cooldown);
		if(played)
		{
			cooldown=config.EMPcooldown;
			GetEMPStrikeCoord();
			MarkSetter();
		}
		if(stuned)
		{
			Stun();
			timer=config.stunTimer;
		}
		if(noStuned)
		{
			if(timer>0.0f)
			{
				timer-=Time.deltaTime;
			}
			else
			{
				Game.notStun(empPos , config.radius );
				spriteObject.SetActive(false);
				noStuned=false;
				Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.EMPicon.name).SetSprite(Game.Prototypes.Extras.EMPicon.name);
				
			}
		}
		if (!played)
		{
			//spriteObject.SetActive(false);
		}
	}
	
	public void Stun()
	{
		Game.Stun (empPos , config.radius );
		stuned=false;
		noStuned=true;
	}
	
	public void GetEMPStrikeCoord()
	{
		if (Input.GetMouseButtonUp(0))
		{
			empPos=Game.GetMouseCoord();	
			stuned=true;
			played=false;
			RosetteBehaviour.Unblock();
			BuyTowerBox.UnblockTowerSpawn();
			
		}
		if(Input.GetMouseButton(0))
		{
			spriteObject.SetActive(true);
			MarkSetter();
		}
	}
	private static void MarkSetter()
	{
		Vector3 position = Game.GetMouseCoord();
		spriteObject.transform.position = new Vector3(position.x,position.y,-3);
	}
}

