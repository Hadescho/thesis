using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	public static Overlay overlay;
	
	void Awake() 
	{
		overlay = Overlay.Find("/Overlay");
		overlay.Alpha = 1.0f;
		overlay.FadeTo(0.0f);
		//Application.LoadLevel("Menu");
		Load ("Menu");
	}
	
	public static void Load(string name)
	{
		overlay.FadeTo(name);
	}
	public static void LoadAdditive(string name)
	{
		Application.LoadLevelAdditive(MapNodeBehaviour.LevelName);
	}
}
