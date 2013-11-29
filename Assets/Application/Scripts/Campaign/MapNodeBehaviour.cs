using UnityEngine;
using System.Collections;

public class MapNodeBehaviour : SiriusBehaviour {
	
	public string redirection; //TODO: After the implementation of multiple levels, I should find a way to deal with the redirection;
	public bool unlocked;
	public bool compleated;
	public GameObject prevLevel;
	public static int loadedLevel;
	public static string LevelName;
	
	
	void Awake()
	{
		
	}
	
	void Start ()
	{
		if (SiriusPrefs.B[gameObject.name + "unlocked"] == true)
		{
			unlocked = true;
			if (SiriusPrefs.B[gameObject.name + "compleated"] == true)
			{
				compleated = true;
			}
			else 
			{
				compleated = false;
			}
		}
		else
		{
			unlocked = false;
		}
		
		redirection = "Game";
		if (!gameObject.name.Contains("-1"))
		{
			prevLevel = gameObject.GetParent();
			if (prevLevel.GetComponent<MapNodeBehaviour>().compleated)
			{
				unlocked = true;
			}
		}
		
		if (!unlocked)
		{
			gameObject.GetComponent<exSprite>().SetSprite("HexagonLocked");
		}
		else if (unlocked && !compleated)
		{
			gameObject.GetComponent<exSprite>().SetSprite("Hexagon");
		}
		else if (compleated)
		{
			gameObject.GetComponent<exSprite>().SetSprite("HexagonClicked");
		}
	}
	
	
	void Update () 
	{
		if (!gameObject.name.Contains("-1"))
		{
			prevLevel = gameObject.GetParent();
			if (prevLevel.GetComponent<MapNodeBehaviour>().compleated)
			{
				unlocked = true;
			}
		}
		
		if (!unlocked)
		{
			gameObject.GetComponent<exSprite>().SetSprite("HexagonLocked");
		}
		else if (unlocked && !compleated)
		{
			gameObject.GetComponent<exSprite>().SetSprite("Hexagon");
		}
		else if (compleated)
		{
			gameObject.GetComponent<exSprite>().SetSprite("HexagonClicked");
		}
	}
	
	public void Redirect()
	{
		if(unlocked)
		{
			MapBehaviour.currentLevel=gameObject.GetChild(0).name;
			loadedLevel=LevelIndex(gameObject.name);
			LevelName="Level"+loadedLevel.ToString();
			Main.Load(redirection);
		}
	}
	
	public void SetUnlocked(GameObject level)
	{
		SiriusPrefs.B[level.GetChild(0).name + "unlocked"] = true;
		unlocked = true;
	}
	
	public void SetCompleated()
	{
		SiriusPrefs.B[gameObject.name + "compleated"] = true;
		compleated = true;
	}
	public int LevelIndex(string name)
	{
		string temp = name.Substring(name.Length-1);
		int i = System.Convert.ToInt32(temp);
		return i -1;
	}
}