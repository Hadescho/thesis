using UnityEngine;
using System.Collections;

public class PauseBehaviour : SiriusBehaviour
{
	private static bool pausePopUpShown;
	
	void Start () 
	{
		pausePopUpShown = false;
		Game.Prototypes.PopUp.pause_pop.FindChild("PauseText").transform.localScale= new Vector3(4.0f , 4.0f , 0.0f);
	}
	
	
	void Update ()
	{
		if(!TowerSpawn.popUpBlockSpawn)
		{
			Check();
			CheckEscape();
		}
	}
	
	public void Check()
	{
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				
				if (hit.transform.gameObject.name == Game.NeutralButtons.pauseButton.name)
				{
					TowerSpawn.tempCursor.SetActive(false);
					if (!pausePopUpShown)
					{
						PausePopUpShow();
						TowerSpawn.popUpBlockSpawn=true;
					}
					else
					{
						PausePopUpHide();
					}
				}
				if (hit.transform.gameObject.name == Game.Prototypes.PopUp.pause_pop.FindChild("RestartButton").name)
				{
					EnemySpawn._wave=0;
					EnemySpawn._creep=0;
					EnemySpawn.currCreep=0;
					HeartStack.lifes=HeartStack.config.Maxlifes;
					//SpawnHearts.spawnHearts();
					//Path.points.Clear();
				}
			
			}
		}
	}
	
	public static  void PausePopUpShow ()
	{
		Game.Prototypes.PopUp.pause_pop.SetActive(true);
		Game.Prototypes.PopUp.pause_pop.transform.animation.Play("WinPop");
		if (Game.TowerToolbar.shown)
		{
			Game.TowerToolbarHide();
			//Game.TowerToolbar.towerToolbar.animation.Play("TowerHide");
		}
		if (Game.SpellToolbar.shown)
		{
			Game.SpellToolbarHide();
			//Game.SpellToolbar.spellToolbar.animation.Play("SpellHide");
		}
		SiriusBehaviour.PauseAll();
		pausePopUpShown = true;
	}
	
	public static void PausePopUpHide ()
	{
		SiriusBehaviour.ResumeAll();
		Game.Prototypes.PopUp.pause_pop.transform.animation.Play("PopBack");
		Game.SpellToolbarShow();
		Game.TowerToolbarShow();
		pausePopUpShown = false;
	}
	
	public static void CheckEscape()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!pausePopUpShown)
			{
				PausePopUpShow();
			}
			else
			{
				Main.Load("Campaign");
			}
		}
	}
}	
