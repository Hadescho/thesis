using UnityEngine;
using System.Collections;

public class MapBehaviour : SiriusBehaviour {
	
	bool _spellsShown;
	public static string currentLevel;
	
	void Awake()
	{
		GameObject level11 = GameObject.Find("/Map/Level 1-1");
		//level11.GetComponent<MapNodeBehaviour>().SetUnlocked(level11);
		SetLevelStateUnlocked(level11.name);
	}
	
	// Use this for initialization
	void Start ()
	{
		_spellsShown = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit))
			{
				return;		
			}
			else
			{
				GameObject hitObj = hit.transform.gameObject;
				if (hitObj.name == "Button Menu")
				{
					Main.Load("Menu");
				}
				if (hitObj.name.Contains("Level "))
				{
					hitObj.GetComponent<MapNodeBehaviour>().Redirect();
				}
				if (hitObj.name == "SpellsButton")
				{
					GameObject spellsObject = hitObj.GetParent();
					if (!_spellsShown)
					{
						spellsObject.animation.PlayQueued("SpellMenuIn");
						_spellsShown = true;
					}
					else
					{
						spellsObject.animation.PlayQueued("SpellMenuOut");
						_spellsShown = false;
					}
				}
			}
		}
	}
	
	public static void SetLevelStateUnlocked(string levelName)
	{
		SiriusPrefs.B[levelName + "unlocked"] = true;
		SiriusPrefs.Save ();
	}
	
	public static void SetLevelStateCompleated(string levelName)
	{
		SiriusPrefs.B[levelName + "unlocked"] = true;
		SiriusPrefs.B[levelName + "compleated"] = true;
		SiriusPrefs.Save ();
	}
}
