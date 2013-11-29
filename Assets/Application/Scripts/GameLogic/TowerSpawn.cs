/***
 Tower indexes:
 0 - Tower 1
 1 - Tower 2
 2 - Tower 3
 3 - Aura Tower
 4 - Cannon Tower
 5 - Shock Tower
 
***/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSpawn : SiriusBehaviour
{
	public static TowerSpawn instance;
	
	private static GameObject background;
	public static GameObject cursor;
	public static List<Vector3> posList;
	public static List<GameObject> towers;
	public static List<GameObject> allTowers;
	public static List<GameObject> allBuildBlockers;
	public static GameObject lastLowAlphaSprite;
	public static bool redCursorBool;
	private static bool _getTowerType;
	
	public static bool popUpBlockSpawn;
	
	public static GameObject alphaTower;
	
	public static GameObject tempCursor;
	
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
		//background = Game.background;
		tempCursor=Game.Cursors.TowerRangeCursor.Clone();
		cursor = Game.cursor;
		posList = new List<Vector3>();
		towers = new List<GameObject>();
		allTowers = new List<GameObject>();
		allBuildBlockers = new List<GameObject>();
		lastLowAlphaSprite = null;
		_getTowerType = true;
		popUpBlockSpawn=false;
		
		towers.Add(Game.Prototypes.Towers.Tower1);
		towers.Add(Game.Prototypes.NewTowers.MachineGun);
		towers.Add(Game.Prototypes.Towers.Tower3);
		towers.Add(Game.Prototypes.NewTowers.UtilityTower);
		towers.Add(Game.Prototypes.NewTowers.Cannon);
		towers.Add(Game.Prototypes.Towers.ShockTower);
		
		
		//Building blockers
		foreach(GameObject gameObj in GameObject.FindObjectsOfType(typeof(GameObject)))
		{
			if (gameObj.name == "BuildBlocker")
			{
				Vector3 temp = gameObj.transform.position;
				
				posList.Add(temp);
				allBuildBlockers.Add(gameObj);
				
				/*posList.Add(new Vector3(temp.x - 16, temp.y , temp.z));
				posList.Add(new Vector3(temp.x + 16, temp.y , temp.z));
				posList.Add(new Vector3(temp.x, temp.y  + 16 , temp.z));
				posList.Add(new Vector3(temp.x, temp.y  - 16 , temp.z));*/
				
			}
		}
	}

	public static void Updater ()
	{
		if (BuyTowerBox.checkTower == -1)
		{
			return;
		}
		if(Input.GetMouseButton(0) && !Game.blockTowerSpawn && !RosetteBehaviour.blockShowTowerRange && !BuyTowerBox.blockTowerRange && !popUpBlockSpawn) 
		{
			tempCursor.SetActive(true);
			ShowCursor();
			ShowTowerRange(Game.GetMouseCoord() , BuyTowerBox.TowerTypeForRange);
			ShowLowAlphaSprite();	
		}
		if (Input.GetMouseButtonUp(0) && !Game.blockTowerSpawn && !popUpBlockSpawn) 
		{
			tempCursor.SetActive(false);
			cursor.SetActive(false);
			Destroy(alphaTower);
			Destroy(lastLowAlphaSprite);
			_getTowerType = true;
			SpawnCoord();
			//Spawn( GetMouseCoord(), towers[BuyTowerBox.checkTower]);
			RosetteBehaviour.Unblock();
			//BuyTowerBox.BlockTowerSpawn();
		}
	}
	
	
	void Update ()
	{
		//Updater ();

	}
	
	
	
	public static GameObject Grid(GameObject go, float z = -1.0f)
	{
		go.transform.position = new Vector3(((Mathf.Round(go.transform.position.x/32.0f)*32.0f)),((Mathf.Round(go.transform.position.y/32.0f)*32.0f)),-1.0f);
		return go;
	}
	
	public static Vector3 Grid(Vector3 vec,float z = -1.0f)
	{
		vec = new Vector3(((Mathf.Round(vec.x/32.0f)*32.0f)),((Mathf.Round(vec.y/32.0f)*32.0f)),z);
		return vec;
	}
	
	public static void SpawnCoord()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		
		if(!Physics.Raycast(ray, out hit))
		{
			return;
		}
		else
		{
			if(hit.transform.gameObject.name == Game.background.name 
				&& Currency.Purchase(towers[BuyTowerBox.checkTower].GetComponent<TowerBehaviour>().type.price))
			{
				Spawn(hit.point , towers[BuyTowerBox.checkTower]);
			}
		}
	}
	
	
	public static void Spawn(Vector3 pos , GameObject tower)
	{
		
		Vector3 vec;
		bool breaker = true;
		GameObject tow;
		
		vec = Grid(pos);
		vec.z = -2 + vec.y / 1000;
	
		for (int i = 0; i < posList.Count; i++)
		{
			if (Mathf.Abs(posList[i].x - vec.x) <= 32 && Mathf.Abs(posList[i].y - vec.y) <= 32)
			{
				//Debug.Log ("FAILED AT: " + vec);
				breaker = false;
				break;
			}
		}
		if (breaker)
		{
			switch (BuyTowerBox.checkTower)
			{
			case 0:
				breaker = Currency.Purchase(Currency.Prices.Tower1Price);
				break;
			case 1:
				breaker = Currency.Purchase(Currency.Prices.Tower2Price);
				break;
			case 2:
				breaker = Currency.Purchase(Currency.Prices.Tower3Price);
				break;
			case 3:
				breaker = Currency.Purchase(Currency.Prices.AuraTowerPrice);
				break;
			case 4:
				breaker = Currency.Purchase(Currency.Prices.CannonTowerPrice);
				break;
			case 5:
				breaker = Currency.Purchase(Currency.Prices.ShockTowerPrice);
				break;
			}
		}
		if (breaker)
		{
			posList.Add(vec);
			tow = tower.Clone();
			tow.transform.position = vec;
			if(BuyTowerBox.checkTower==0)
			{
				TowerBehaviour.FloatingTextSpawn(Currency.Prices.Tower1Price,tow,'-');
				tow.AddComponent<Tower1Behaviour>();
			}
			else if(BuyTowerBox.checkTower==1)
			{
				TowerBehaviour.FloatingTextSpawn(Currency.Prices.Tower2Price,tow,'-');
				//tow.AddComponent<Tower2Behaviour>();
			}
			else if(BuyTowerBox.checkTower==2)
			{
				TowerBehaviour.FloatingTextSpawn(Currency.Prices.Tower3Price,tow,'-');
				tow.AddComponent<Tower3Behaviour>();
			}
			else if(BuyTowerBox.checkTower==3)
			{
				TowerBehaviour.FloatingTextSpawn(Currency.Prices.AuraTowerPrice,tow,'-');
				//tow.AddComponent<AuraTower>();
			}
			else if(BuyTowerBox.checkTower == 4)
			{
				TowerBehaviour.FloatingTextSpawn(Currency.Prices.CannonTowerPrice,tow,'-');
				//tow.AddComponent<CannonTower>();
			}
			//Debug.Log (pos);
			allTowers.Add(tow);
			_getTowerType = true;
			//Game.blockTowerSpawn=true;
		}
	}
		
		
	public static void ShowCursor()		
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		redCursorBool = false;
		
		if(!Physics.Raycast(ray, out hit))
		{
			return;
		}
		else
		{
			//if(hit.transform.gameObject != null && hit.transform.gameObject.name == "Background")
			//if(hit.transform.gameObject == Game.background)
			//{
				Vector3 pos = hit.point;
				Vector3 vec;
				
				vec = Grid(pos);
				cursor.SetActive(true);
				cursor.transform.position = vec;
				
				for (int i = 0; i < posList.Count; i++)
				{
					if (Mathf.Abs(posList[i].x - vec.x) <= 32 && Mathf.Abs(posList[i].y - vec.y) <= 32)
					{
						redCursorBool = true;
						break;
					}
				}
				
				if (redCursorBool)
				{
					cursor.SetSprite("RedCursor");
				}
				else
				{
					cursor.SetSprite("Cursor");
				}
			
				
	
			//}
		}
	}

	
	public static void ShowLowAlphaSprite()
	{
		if (lastLowAlphaSprite != null)
		{
			Destroy(lastLowAlphaSprite);
		}
		if(alphaTower!=null)
		{
			alphaTower.SetActive(true);
		}
		if (_getTowerType)
		{
			switch (BuyTowerBox.checkTower)
			{
			case 0:
				alphaTower = Game.Prototypes.Towers.Tower1.Clone();
				break;
			case 1:
				alphaTower = Game.Prototypes.Towers.Tower2.Clone();
				break;
			case 2:
				alphaTower = Game.Prototypes.Towers.Tower3.Clone();
				break;
			case 3:
				alphaTower = Game.Prototypes.Towers.AuraTower.Clone();
				break;
			case 4:
				alphaTower = Game.Prototypes.Towers.CannonTower.Clone();
				break;
			default:
				alphaTower = null;
				break;
			}
			
			alphaTower.GetComponent<exSprite>().color = new Color(1,1,1,0.5f);
			alphaTower.name = "AlphaTower";
			Destroy(alphaTower.GetComponent<BoxCollider>());
			_getTowerType = false;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		redCursorBool = false;
		
		if(!Physics.Raycast(ray, out hit))
		{
			return;
		}
		else
		{
			//if(hit.transform.gameObject == background)
			//if(hit.transform.gameObject == Game.background)
			//{
				Vector3 pos = hit.point;
				Vector3 vec;
				
				vec = Grid(pos);
				
				alphaTower.transform.position = vec;
				lastLowAlphaSprite = alphaTower.Clone ();
				
			//}
		}
		
	}
	
	public static void FloatingTextSpawn(float valueSpent,GameObject pos)
	{
		//FloatingText floatie = Game.Prototypes.FloatingTexts.CashSpent.Clone("-" + valueSpent.ToString(),pos.transform.position);
	}
	
	public static void DestroyInPosList(Vector3 posVec)
	{
		posList.Remove(posVec);
	}
	
	public static void ShowTowerRange(Vector3 currPos , GameObject tower)
	{
		tempCursor.transform.position=Grid(currPos);
		
		//Scaling
		Vector3 scale = tempCursor.transform.localScale;
		scale.x = tower.GetComponent<TowerBehaviour>().type.range/(tempCursor.GetComponent<exSprite>().width/2);
		scale.y = tower.GetComponent<TowerBehaviour>().type.range/(tempCursor.GetComponent<exSprite>().height/2);
		tempCursor.transform.localScale = scale;
		
	}
	public static void ShowTowerRange(Vector3 currPos , float range)
	{
		tempCursor.transform.position=Grid(currPos);
		
		//Scaling
		Vector3 scale = tempCursor.transform.localScale;
		scale.x = range/(tempCursor.GetComponent<exSprite>().width/2);
		scale.y = range/(tempCursor.GetComponent<exSprite>().height/2);
		tempCursor.transform.localScale = scale;
		
	}
	
	public static void AddTower(GameObject tower)
	{
		allTowers.Add(tower);
		posList.Add (tower.transform.position);
	}
	
	public static void RemoveTower(GameObject tower)
	{
		if (allTowers.Contains(tower))
		{
			allTowers.Remove(tower);
		}
		else
		{
			//Debug.LogError ("TowerSpawn.RemoveTower: For some weird reason the tower you want to remove is not in the towers array");
		}
		if (posList.Contains(tower.transform.position))
		{
			posList.Remove(tower.transform.position);
		}
		else
		{
			//Debug.LogError ("TowerSpawn.RemoveTower: For some weird reason the tower you want to remove is not in the posList array");
		}
		Destroy(tower);
	}
}
