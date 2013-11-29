using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellPositioning : MonoBehaviour {

	public static List<GameObject> positionedSpells;
	public static List<GameObject> drawedSpells;
	public static List<Vector3> positionCoordinates;
	//bool full;
	public static bool takenCoords=false;
	
	void Awake()
	{
		//SiriusPrefs.B["isSpellsSelected"]=false;
		positionCoordinates = new List<Vector3>();
	}
	
	void Start () 
	{
		//SetUnbuyedSpells();
		positionedSpells = new List<GameObject>();
		drawedSpells = new List<GameObject>();
		
		/*for(int i = 0 ; i<Campaign.Prototypes.SpellsHolder.transform.childCount ; i++)
		{
			Vector3 vec =Campaign.Prototypes.SpellsHolder.GetChild(i).transform.position;
			positionCoordinates.Add(vec);
		}*/
		
		//full=false;
		takenCoords=false;
		
		//SiriusPrefs.B["isSpellsSelected"]=false;
		
		if(SiriusPrefs.B["isSpellsSelected"])
		{
			TakeCoords();
			//TakeTempCoords();
			positionedSpells.Clear();
			InitialAddInSelectedSpells();
			//positionedSpells.Clear();
		}
		
	}
	

	void Update ()
	{
	
	}
	
	public static void Position( string t)
	{
		
		if(!takenCoords)
		{
			TakeCoords();
		}
		else
		{
			TakeTempCoords();
		}
		GameObject spell = Campaign.Prototypes.SpellFrame.FindChild(t).Clone();	
		spell.transform.position=positionCoordinates[SiriusPrefs.I[spell.name]];
		spell.transform.parent = Campaign.Prototypes.SpellsHolder.transform;
			
	}
	public static void DePosition( string t)
	{
		Destroy(Campaign.Prototypes.SpellsHolder.FindChild(t));
	}
	
	public static void ClearList()
	{
		for(int i = 0 ; i<drawedSpells.Count ; i++)
		{
			Destroy(drawedSpells[i]);
		}
		drawedSpells.Clear();
	}
	public static void TakeCoords()
	{
		if(!takenCoords)
		{
			positionCoordinates.Add(Campaign.Prototypes.SpellsHolder.FindChild("Slot0").transform.position);
			positionCoordinates.Add(Campaign.Prototypes.SpellsHolder.FindChild("Slot1").transform.position);
			positionCoordinates.Add(Campaign.Prototypes.SpellsHolder.FindChild("Slot2").transform.position);
			takenCoords=true;
		}
	}
	public static void TakeTempCoords()
	{
		//if(!SiriusPrefs.B["isSpellsPositionTaken"])
		//{
			positionCoordinates[0]=Campaign.Prototypes.SpellsHolder.FindChild("Slot0").transform.position;
			positionCoordinates[1]=Campaign.Prototypes.SpellsHolder.FindChild("Slot1").transform.position;
			positionCoordinates[2]=Campaign.Prototypes.SpellsHolder.FindChild("Slot2").transform.position;
		//}
	}
	public void InitialAddInSelectedSpells()
	{
		for(int i = 0 ; i<3 ; i++)
		{
			string temp = "Spell"+i.ToString();	
			
			temp=SiriusPrefs.S[temp];
			//Debug.Log(temp + i );
			if(temp=="empty")
			{
				continue;
			}
			GameObject spell = Campaign.Prototypes.SpellFrame.FindChild(temp).Clone();
				
			//positionedSpells.Add(spell);
			spell.transform.position=positionCoordinates[i];
			SpellSelectionBehaviour.selectedSpells.Add(spell.name);
			Campaign.Prototypes.SpellFrame.FindChild(spell.name).SetSprite(spell.name + "Clicked");
			spell.transform.parent = Campaign.Prototypes.SpellsHolder.transform;
			
			
		}
		//Position();
	}
	public static void CheckAndDestroy(string name)
	{
		for (int i = 0; i < positionedSpells.Count ; i++) 
		{
			if(positionedSpells[i].name == name)
			{
				Destroy(positionedSpells[i]);
			}
		}
	}
	public static void SetUnbuyedSpells()
	{
		for( int i = 0 ; i<Campaign.Prototypes.SpellFrame.GetChildCount() ; i++)
		{
			if(!SiriusPrefs.B["Store"+Campaign.Prototypes.SpellFrame.GetChild(i).name])
			{
				Campaign.Prototypes.SpellFrame.GetChild(i).SetSprite(Campaign.Prototypes.SpellFrame.GetChild(i).name);
			}
		}
	}
	
}
