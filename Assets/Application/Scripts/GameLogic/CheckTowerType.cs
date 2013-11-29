using UnityEngine;
using System.Collections;

public class CheckTowerType : MonoBehaviour 
{
	
	public static int towerType;
	
	void Start () 
	{
	
	}
	
	
	void Update ()
	{
		Check();
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
				
				if (hit.transform.gameObject.name == Game.Prototypes.Towers.Tower1.name)
				{
					//BuyTowerBox.BlockTowerSpawn();
					towerType=0;
				}
				else if (hit.transform.gameObject.name == Game.Prototypes.Towers.Tower2.name)
				{
					//BuyTowerBox.BlockTowerSpawn();
					towerType=1;
					
				}
				else if (hit.transform.gameObject.name == Game.Prototypes.Towers.Tower3.name)
				{
					//BuyTowerBox.BlockTowerSpawn();
					towerType=2;
				}
				else if (hit.transform.gameObject.name == Game.Prototypes.Towers.AuraTower.name)
				{
					//BuyTowerBox.BlockTowerSpawn();
					towerType=3;
				}
				else if (SpellSelection.IfCast())
				{
					BuyTowerBox.UnblockTowerSpawn();
					towerType=BuyTowerBox.checkTower;
				}
			}
		}
	}
}
