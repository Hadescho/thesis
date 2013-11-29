using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RosetteBehaviour : SiriusBehaviour {
	
	public class Config
	{
		public float rosetteLife = 3.0f;
	}
	
	public static Config config;
	
	
	public static RosetteBehaviour instance;
	public static bool blocked = false;
	public GameObject owner;
	public float time;
	
	public bool visible;
	public bool jumpFrame;
	
	public GameObject Upgrade1;
	public GameObject Upgrade2;
	public GameObject Upgrade3;
	public GameObject Sell;
	
	public static bool blockShowTowerRange;
	
	void Awake()
	{
		if(blocked == true)
		{
			Destroy (gameObject);
			return;
		}
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		
		instance = this;
		
	}
	
	void Start () 
	{
		config = new Config();
		Vector3 vec = new Vector3(owner.transform.position.x,owner.transform.position.y,-9.0f);
		gameObject.transform.position = vec;
		time = config.rosetteLife;
		blockShowTowerRange=false;
		TowerSpawn.ShowTowerRange(gameObject.transform.position , owner);
		TowerSpawn.tempCursor.SetActive(true);
		Upgrade1 = gameObject.FindChild("Upgrade1");
		Upgrade2 = gameObject.FindChild("Upgrade2");
		Upgrade3 = gameObject.FindChild("Upgrade3");
		Sell = gameObject.FindChild("Sell");
		visible=true;
		jumpFrame=true;
		Repositionate();
		
		
		TowerSpawn.cursor.SetActive(false);
		Destroy(TowerSpawn.lastLowAlphaSprite);
		//Destroy(TowerSpawn.alphaTower);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (time > 0)
		{
			blockShowTowerRange=true;
			time -= Time.deltaTime;
			
		}
		else
		{
			DestroyRosette();
		}
		if(Input.GetMouseButtonUp(0) && !visible)
		{
			DestroyRosette();
			Game.blockTowerSpawn=false;
		}
		if(Input.GetMouseButtonUp(0) && !jumpFrame)
		{
			visible=false;
			Game.blockTowerSpawn=true;
		}
		jumpFrame=false;
		
		
	
	}
	
	public void Repositionate()
	{
		const int reposVal = 100;
		Vector3 repos = gameObject.transform.position;
		if(Upgrade1 != null && !Game.OnScreen(Upgrade1) && Upgrade1.activeSelf)
		{
			repos.y -= reposVal;
		}
		if(Upgrade2 != null && !Game.OnScreen(Upgrade2) && Upgrade2.activeSelf)
		{
			repos.x -= reposVal;
		}
		if(Upgrade3 != null && !Game.OnScreen(Upgrade3) && Upgrade3.activeSelf)
		{
			repos.x += reposVal;
		}
		if(Sell != null && !Game.OnScreen(Sell) && Sell.activeSelf)
		{
			repos.y += reposVal;
		}
		//gameObject.transform.position = repos;
		MoveToBehaviour.Attach(gameObject, repos, 0.1f);
	}
	
	public void DestroyRosette()
	{
		
		Destroy(instance.gameObject);
		Game.LastRosette.Hide();
		TowerSpawn.tempCursor.SetActive(false);
		blockShowTowerRange=false;
		visible=true;
		jumpFrame=true;
		TowerSpawn.alphaTower.SetActive(false);
	}
	void OnDestroy()
	{
		Game.LastRosette.shown = false;
		Game.SetBuildingBlockFalse();
	}
	
	public static bool ToggleBlock()
	{
		return blocked = !blocked;
	}
	
	public static void Block()
	{
		blocked = true;
	}
	
	public static void Unblock()
	{
		blocked = false;
	}
	
}
