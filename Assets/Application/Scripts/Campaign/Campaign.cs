using UnityEngine;
using System.Collections;

public class Campaign : MonoBehaviour {
	
	public static class Prototypes
	{
		public static GameObject SpellFrame;
		public static GameObject SpellButton;
		public static GameObject Spells;
		public static GameObject SpellsHolder;
		
		public static GameObject AirStrike;
		public static GameObject OrbitalStrikeIcon;
		public static GameObject ArilleryIcon;
		public static GameObject WarBannerIcon;
		public static GameObject EMPicon;
		public static GameObject Timeshift;
		public static GameObject AirDropIcon;
		public static GameObject EnergyDrainIcon;
		
	}
	
	
	void Awake()
	{
		Prototypes.SpellFrame = GameObject.Find("/Spell/SpellsFrame");
		Prototypes.SpellButton = GameObject.Find("/Spell/SpellsButton");
		Prototypes.Spells = GameObject.Find("/Spell");
		Prototypes.SpellsHolder = GameObject.Find("/Spell/SpellsHolder");
		
		Prototypes.AirStrike = GameObject.Find("/Spell/SpellsFrame/AirStrike");
		Prototypes.OrbitalStrikeIcon = GameObject.Find("/Spell/SpellsFrame/OrbitalStrike");
		Prototypes.ArilleryIcon = GameObject.Find("/Spell/SpellsFrame/ArtileryStrike");
		Prototypes.WarBannerIcon = GameObject.Find("/Spell/SpellsFrame/Banner");
		Prototypes.EMPicon = GameObject.Find("/Spell/SpellsFrame/EMP");
		Prototypes.AirDropIcon = GameObject.Find("/Spell/SpellsFrame/AirDrop");
		Prototypes.Timeshift = GameObject.Find("/Spell/SpellsFrame/Timeshift");
		Prototypes.EnergyDrainIcon = GameObject.Find("/Spell/SpellsFrame/EnergyDrain");
	}
	
	void Start ()
	{
	
	}
	

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Main.Load("Menu");
		}
	}
}
