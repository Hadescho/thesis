using UnityEngine;
using System.Collections;

public class OrbitalStrikeBehaviour : MonoBehaviour {

	public GameObject laser;
	public static bool played;
	public Vector3 strikePos;
	private bool _orbitalStrike;
	private float _timer;
	private bool _dealDamage;
	
	public class Config
	{
		public float energyCost=10.0f;
		public float orbitalRadius=50.0f;
		public float orbitalDamage=1000.0f;
		public float laserHideTime=-3.0f;
		
		public float orbitalStrikeCooldown=10.0f;
	}
	
	public static Config config = new Config();	
	public static float cooldown;
	
	void Start () 
	{
		played = false ;
		_orbitalStrike=false;
		_timer= config.laserHideTime;
		_dealDamage = false;
		cooldown=-1.0f;
	}
	
	
	void Update () 
	{
		Game.DropSpellCooldown(ref cooldown);
		if(played)
		{
			GetOrbitalStrikeCoord();
			cooldown=config.orbitalStrikeCooldown;
		}
		if(_orbitalStrike)
		{
			OrbitalStrikeFire();
		}
		if (_dealDamage)
		{
			Vector3 damagePos = laser.transform.position;
			damagePos.y += 20;
			DealDamage(damagePos,config.orbitalRadius);
		}
	}
	
	
	
	public void OrbitalStrikeFire()
	{
		if(_timer>0)
		{
			laser.SetActive(false);
			_orbitalStrike=false;
			_dealDamage=false;
			Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.OrbitalStrikeIcon.name).SetSprite(Game.Prototypes.Extras.OrbitalStrikeIcon.name);
		}
		else
		{
			_timer+=Time.deltaTime;
		}
	}
	
		
	public void GetOrbitalStrikeCoord()
	{
		if (Input.GetMouseButtonUp(0))
		{
			strikePos=Game.GetMouseCoord();
			
			_orbitalStrike=true;
			played=false;
			
			laser=Game.Prototypes .Extras .OrbitalStrike.Clone ();
			strikePos.y-=50.0f;
			
			laser.transform .position =strikePos;
			Debug.Log(laser.transform .position + "POSSITION");
			_timer= config.laserHideTime;
			
			Game.Explode(laser.transform.position , config.orbitalRadius , config.orbitalDamage);
			//DealDamage(laser.transform.position, config.orbitalRadius);
			RosetteBehaviour.Unblock();
			
			BuyTowerBox.UnblockTowerSpawn();
			laser.SetActive(true);
			
			//scale and rotate of the animation
			Vector3 tempscale= laser.transform.localScale;
			tempscale.x*=20;
			tempscale.y*=3;
			laser.transform.localScale=tempscale;
			
			laser.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
			
			Vector3 tempPos = laser.transform.position;
			tempPos.y+=750.0f;
			laser.transform.position=tempPos;
			
			laser.GetComponent<exSpriteAnimation>().Play("LaserAnimation");
			
			_dealDamage = true;
		}
	}
	
	public static void DealDamage(Vector3 pos, float radius)
	{
		foreach(GameObject enemy in Enemy.all)
		{
			Vector3 delta = enemy.transform.position - pos;
			float dist = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);	
			if (dist < radius)
			{
				float distRad = dist/radius;
				if (distRad < 0.2)
				{
					enemy.GetComponent<Enemy>().Damage(config.orbitalDamage * Time.deltaTime * 0.2f);
					
				}
				else if (distRad > 0.8)
				{
					enemy.GetComponent<Enemy>().Damage(config.orbitalDamage * Time.deltaTime);
				}
				else
				{
					enemy.GetComponent<Enemy>().Damage(config.orbitalDamage * distRad * Time.deltaTime);
				}
			}
		}
	}
}
