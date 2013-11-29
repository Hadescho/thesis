using UnityEngine;
using System.Collections;

public class ArtilleryBehaviour : MonoBehaviour
{

	public Vector3 artilleryPos;
	public bool artilleryStrike;
	public bool artilleryPlayed;
	public float hitTimer;
	public GameObject hit;
	public int hitCounter;
	public int destroyCounter;
	public static bool played;
	
	public GameObject tempCursor; 
	
	public class Config
	{
		public GameObject  [] hits= new GameObject [40];
		public float hitTime=0.15f;
		public float artilleryRadius=100.0f;
		public float artilleryHitDamage=100.0f;
		public float artilleryHitRadius=30.0f;
		public float hideExplosionTime=0.6f;
		public float artilleryCursorDestroyTime=0.1f;
		
		public float artilleryStrikeCooldown = 10.0f;
	}
	
	public static Config config = new Config();
	
	public static float cooldown;
	
	void Start ()
	{
		tempCursor = Game.Prototypes.Extras.ArtilleryStrikeCursor.Clone();
		for(int i = 0 ; i<config.hits.Length ; i++)
		{
			hit=Game.Prototypes.Extras.Explosion.Clone();
			hit.SetActive(false);
			config.hits[i]=hit;
		}
		
		hitCounter=0;
		destroyCounter=0;
		
		artilleryStrike=false;
		artilleryPlayed=false;
		played=false;
		
		
		hitTimer=config.hitTime;
		
		cooldown= -1.0f;
	}
	
	
	void Update ()
	{
		Game.DropSpellCooldown(ref cooldown);
		if(!artilleryStrike && played)
		{
			//Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.ArilleryIcon.name).SetSprite("ArtileryStrike");
			GetArtilleryStrikeCoord();
			cooldown=config.artilleryStrikeCooldown;
		}
		if(artilleryPlayed)
		{
			ArtilleyStrikeBarrage();
			RosetteBehaviour.Unblock();
		}
	}
	
	
	public void ArtilleyStrikeBarrage()
	{
		if(hitTimer>0)
		{
			hitTimer-=Time.deltaTime;
			if(hitCounter==config.hits.Length)
			{
				artilleryPlayed=false;
				artilleryStrike=false;
				played=false;
				hitCounter=0;
				Game.SpellToolbar.spellToolbar.FindChild(Game.Prototypes.Extras.ArilleryIcon.name).SetSprite(Game.Prototypes.Extras.ArilleryIcon.name);
			}
			//ShowArtilleryCursor(artilleryPos);
		}
		else
		{
			config.hits[hitCounter].SetActive(true);
			config.hits[hitCounter].GetComponent<exSpriteAnimation>().Play("Explosion");
			Game.Explode(config.hits[hitCounter].transform.position , config.artilleryHitRadius , config.artilleryHitDamage);
			Sirius.ExecAfter(config.hideExplosionTime ,SetActiveFalse);
			//config.hits[hitCounter].DestroyAfter(0.5f);
			hitCounter++;
			hitTimer=config.hitTime;
			//ShowArtilleryCursor(artilleryPos);
		}
		
	}
	
	public Vector3 RandomHit()
	{
		Vector3 randomHit;
		randomHit=new Vector3 (Random.Range(artilleryPos.x-config.artilleryRadius , artilleryPos.x+config.artilleryRadius),Random.Range(artilleryPos.y-config.artilleryRadius , artilleryPos.y+config.artilleryRadius) , -5.0f);
		return randomHit ;
	}
	
	public void SetActiveFalse()
	{
		config.hits[destroyCounter].SetActive(false);
		destroyCounter++;
		if(destroyCounter==config.hits.Length)
		{
			destroyCounter=0;	
		}
	}
	
	public void GetArtilleryStrikeCoord()
	{
		if (Input.GetMouseButtonUp(0))
		{
			artilleryPos=Game.GetMouseCoord();
			artilleryStrike=true;
			artilleryPlayed=true;
			for(int i = 0; i<config.hits.Length ; i++)
			{
				config.hits[i].transform.position=RandomHit();	
			}
			tempCursor.SetActive(false);
			BuyTowerBox.UnblockTowerSpawn();
		}
		if(Input.GetMouseButton(0))
		{
			tempCursor.SetActive(true);
			ShowArtilleryCursor(Game.GetMouseCoord());
		}
	}
	
	public void ShowArtilleryCursor(Vector3 vec)
	{
		
		tempCursor.transform.position=vec;
		
		//Scaling
		Vector3 scale = tempCursor.transform.localScale;
		scale.x = config.artilleryRadius/(tempCursor.GetComponent<exSprite>().width/2) ;
		scale.y = config.artilleryRadius/(tempCursor.GetComponent<exSprite>().height/2) ;
		
		tempCursor.transform.localScale = scale;
		
	}
}
