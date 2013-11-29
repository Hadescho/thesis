using UnityEngine;
using System.Collections;

public class GameMenuBehaviour : MonoBehaviour 
{
	private bool _LeftButtonsShown;
	
	void Start () 
	{
		_LeftButtonsShown = false;
	}
	
	
	void Update () 
	{
		CheckButtons();
	}
	
	void CheckButtons()
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
				GameObject hitObj = hit.transform.gameObject;
				CheckPlay(hitObj);
				CheckStore(hitObj);
				CheckAchievements(hitObj);
				CheckSettings(hitObj);
				CheckCampaign(hitObj);
				CheckEndless(hitObj);
				CheckExit(hitObj);
			}
		}
	}
	void CheckPlay(GameObject hitObj)
	{
		if (hitObj.name == GameMenuGame.GameMenu.playButton.name)
		{
			if (!_LeftButtonsShown)
			{
				GameMenuGame.GameMenu.buttonsLeft.animation.Play("LeftMenuButtonsShow");
				_LeftButtonsShown = true;
			}
			else
			{
				GameMenuGame.GameMenu.buttonsLeft.animation.Play("LeftMenuButtonsHide");
				_LeftButtonsShown = false;
			}
		}
	}
	void CheckStore(GameObject hitObj)
	{	
		if (hitObj.name == GameMenuGame.GameMenu.storeButton.name)
		{
			Main.Load("Store"); 
		}
	}
	void CheckAchievements(GameObject hitObj)
	{
		if (hitObj.name == GameMenuGame.GameMenu.achievmentsButton.name)
		{
			Main.Load("Achievements"); 
		}
	}
	void CheckSettings(GameObject hitObj)
	{
		if (hitObj.name == GameMenuGame.GameMenu.settingsButton.name)
		{
			Main.Load("Settings");   
		}
	}
	void CheckCampaign(GameObject hitObj)
	{	
		if (hitObj.name == GameMenuGame.GameMenu.campaignButton.name)
		{
			Main.Load("Campaign"); 
		}
	}
	
	void CheckExit(GameObject hitObj)
	{
		if(hitObj.name == GameMenuGame.GameMenu.exitButton.name)
		{
			Application.Quit();
			if (SystemInfo.deviceType == DeviceType.Desktop)
			{
				//SiriusPrefs.DeleteAll();
				Debug.Log ("ASDASDASDASDASDASDASDASDASDAS");
			}
		}
	}
	
	void CheckEndless(GameObject hitObj)
	{
		if (hitObj.name == GameMenuGame.GameMenu.endlessButton.name)
		{
			//Application.LoadLevel("Endless");
			Debug.Log ("Still not availale.");
		}
	}
	
}
