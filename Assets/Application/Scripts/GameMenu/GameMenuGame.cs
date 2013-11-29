using UnityEngine;
using System.Collections;

public class GameMenuGame : MonoBehaviour 
{
	
	public static class GameMenu
	{
		public static GameObject buttonsLeft;
		public static GameObject buttonsRight;
		public static GameObject playButton;
		public static GameObject storeButton;
		public static GameObject achievmentsButton;
		public static GameObject settingsButton;
		public static GameObject endlessButton;
		public static GameObject campaignButton;
		public static GameObject exitButton;
	}
	
	
	
	void Awake()
	{
		GameMenu.buttonsLeft = GameObject.Find("/Buttons/ButtonsLeft");
		GameMenu.buttonsRight = GameObject.Find("/Buttons/ButtonsRight");
		GameMenu.playButton = GameObject.Find("/Buttons/ButtonsRight/ButtonPlay");
		GameMenu.achievmentsButton = GameObject.Find("/Buttons/ButtonsRight/ButtonAchievements");
		GameMenu.settingsButton = GameObject.Find("/Buttons/ButtonsRight/ButtonSettings");
		GameMenu.storeButton = GameObject.Find("/Buttons/ButtonsRight/ButtonStore");
		GameMenu.campaignButton = GameObject.Find("/Buttons/ButtonsLeft/ButtonCampaign");
		GameMenu.exitButton = GameObject.Find("/Buttons/ButtonExit");
		GameMenu.endlessButton = GameObject.Find ("/Buttons/ButtonsLeft/ButtonEndless");
	
	}
	
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
