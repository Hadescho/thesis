using UnityEngine;
using System.Collections;

public class BuyTowerBox : SiriusBehaviour 
{
	public static int checkTower;
	static GameObject but1;
	static GameObject but2;
	static GameObject but3;
	static GameObject but4;
	static GameObject but5;
	static int mycheck;
	public static bool blockTowerRange;
	public static bool testLocker;
	
	public static float TowerTypeForRange;
	
	void Start () 
	{
		but1 = GameObject.Find("/Toolbars/TowerToolbar/ButtonTower1");
		but2 = GameObject.Find("/Toolbars/TowerToolbar/ButtonTower2");
		but3 = GameObject.Find("/Toolbars/TowerToolbar/ButtonTower3");
		but4 = GameObject.Find("/Toolbars/TowerToolbar/ButtonAuraTower");
		but5 = GameObject.Find("/Toolbars/TowerToolbar/CannonTower");
		blockTowerRange=false;
		mycheck = -1;
		
	}
	
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			checkTower = Check();
		}
		testLocker = !testLocker;
		
	}
	public static int Check()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		
		
		if(!Physics.Raycast(ray, out hit))
		{
		}
		else
		{
			if(testLocker)
			{
				if (hit.transform.gameObject == but1)
				{
					if (mycheck == 0)
					{
						but1.SetSprite("tower1");
						//BlockTowerSpawn();
						mycheck = -1;
						return 0;
					}
				    mycheck = 0;
					Game.blockTowerSpawn=false;
					RosetteBehaviour.Block ();
					but1.SetSprite("tower1clicked");
					but2.SetSprite("tower2");
					but3.SetSprite("tower3");
					but4.SetSprite("AuraTower");
					but5.SetSprite("CannonTowerButton");
					TowerSpawn.tempCursor.SetActive(false);
					blockTowerRange=true;
				}
				else if (hit.transform.gameObject == but2)
				{
					if (mycheck == 1)
					{
						but2.SetSprite("tower2");
						//BlockTowerSpawn();
						mycheck = -1;
						return 1;
					}
					
					TowerTypeForRange=TowerBehaviour.config.towers["Machinegun"].range;
				    mycheck = 1;
					Game.blockTowerSpawn=false;
					RosetteBehaviour.Block ();
					but2.SetSprite("tower2clicked");
					but1.SetSprite("tower1");
					but3.SetSprite("tower3");
					but4.SetSprite("AuraTower");
					but5.SetSprite("CannonTowerButton");
					TowerSpawn.tempCursor.SetActive(false);
					blockTowerRange=true;
				}
				else if (hit.transform.gameObject == but3)
				{
					if (mycheck == 2)
					{
						but3.SetSprite("tower3");
						//BlockTowerSpawn();
						mycheck = -1;
						return 2;
					}
				    mycheck = 2;
					Game.blockTowerSpawn=false;
					RosetteBehaviour.Block ();
					but3.SetSprite("tower3clicked");
					but1.SetSprite("tower1");
					but2.SetSprite("tower2");
					but4.SetSprite("AuraTower");
					but5.SetSprite("CannonTowerButton");
					TowerSpawn.tempCursor.SetActive(false);
					blockTowerRange=true;
				}
				else if (hit.transform.gameObject == but4)
				{
					if (mycheck == 3)
					{
						but4.SetSprite("AuraTower");
						//BlockTowerSpawn();
						mycheck = -1;
						return 3;
					}
					mycheck = 3;
					//need working utility tower
					TowerTypeForRange=TowerBehaviour.config.towers["Utility Tower"].range;
					Game.blockTowerSpawn=false;
					RosetteBehaviour.Block ();
					but4.SetSprite("AuraTowerClicked");
					but1.SetSprite("tower1");
					but2.SetSprite("tower2");
					but3.SetSprite("tower3");
					but5.SetSprite("CannonTowerButton");
					TowerSpawn.tempCursor.SetActive(false);
					blockTowerRange=true;
				}
				else if (hit.transform.gameObject == but5)
				{
					if (mycheck == 4)
					{
						but5.SetSprite("CannonTowerButton");
						//BlockTowerSpawn();
						mycheck = -1;
						return 4;
					}
					TowerTypeForRange=TowerBehaviour.config.towers["Cannon"].range;
					mycheck = 4;
					Game.blockTowerSpawn=false;
					RosetteBehaviour.Block ();
					but5.SetSprite("CannonTowerClicked");
					but1.SetSprite("tower1");
					but2.SetSprite("tower2");
					but3.SetSprite("tower3");
					but4.SetSprite("AuraTower");
					TowerSpawn.tempCursor.SetActive(false);
					blockTowerRange=true;
				}
				else
				{
					//Game.blockTowerSpawn=true;
					//but2.SetSprite("tower2");
					//but1.SetSprite("tower1");
					//but3.SetSprite("tower3");
					//but4.SetSprite("AuraTower");
					//but5.SetSprite("CannonTowerButton");
					blockTowerRange=false;
				}
			}
			
		}
		return mycheck ;
	}
	
	public static void BlockTowerSpawn()
	{
		Game.blockTowerSpawn=true;
	}
	
	public static void UnblockTowerSpawn()
	{
		Game.blockTowerSpawn=false;
	}
		
}
