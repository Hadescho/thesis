using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellSelectionBehaviour : MonoBehaviour {
	
	public class Config
	{
		public int maxSpells=3;
	}
	
	public static Config config = new Config();
	
	public static List<string> selectedSpells;
	public bool picked;
	
	void Awake()
	{
		//SiriusPrefs.B["isSpellsSelected"]=false;
		if(!SiriusPrefs.B["isSpellsSelected"])
		{
			SiriusPrefs.S["Spell0"]="empty";
			SiriusPrefs.S["Spell1"]="empty";
			SiriusPrefs.S["Spell2"]="empty";
		}

	}
	
	void Start () 
	{
		selectedSpells = new List<string>();
		picked=true;
		PrepareSpellPop();
	}
	
	void Update ()
	{
		DeselectCheck();
		SelectCheck();	
		picked=true;
	}
	
	
	
	public static void PrepareSpellPop()
	{
		Campaign.Prototypes.SpellFrame.AddComponent<UIFlow>();
		Campaign.Prototypes.SpellFrame.GetComponent<UIFlow>().VerticalDistance=80.0f;
		Campaign.Prototypes.SpellFrame.GetComponent<UIFlow>().HorizontalDistance=0.0f;
		Campaign.Prototypes.SpellFrame.GetComponent<UIFlow>().Alignment=UI.Alignment.TopLeft;
		Campaign.Prototypes.SpellFrame.GetComponent<UIFlow>().Rows=4;
		Campaign.Prototypes.SpellFrame.GetComponent<UIFlow>().Columns=3;
		//zazaz
	}
	
	
	public void SelectCheck()
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
				if (hit.transform.gameObject == Campaign.Prototypes.OrbitalStrikeIcon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.OrbitalStrikeIcon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.OrbitalStrikeIcon.name))
					{
						
						AddInSirusPrefs(Campaign.Prototypes.OrbitalStrikeIcon.name);
						
						Campaign.Prototypes.OrbitalStrikeIcon.SetSprite(Campaign.Prototypes.OrbitalStrikeIcon.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.OrbitalStrikeIcon.name);
			
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.AirDropIcon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.AirDropIcon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.AirDropIcon.name ))
					{
						
						AddInSirusPrefs(Campaign.Prototypes.AirDropIcon.name);
						
						Campaign.Prototypes.AirDropIcon.SetSprite(Campaign.Prototypes.AirDropIcon.name+"Clicked");	
						selectedSpells.Add(Campaign.Prototypes.AirDropIcon.name);
						
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.AirStrike && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.AirStrike.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.AirStrike.name))
					{
						
						Debug.Log("massage");
						AddInSirusPrefs(Campaign.Prototypes.AirStrike.name);
						
						
						Campaign.Prototypes.AirStrike.SetSprite(Campaign.Prototypes.AirStrike.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.AirStrike.name);
						
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.ArilleryIcon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.ArilleryIcon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.ArilleryIcon.name))
					{
						
						AddInSirusPrefs(Campaign.Prototypes.ArilleryIcon.name);
						
						Campaign.Prototypes.ArilleryIcon.SetSprite(Campaign.Prototypes.ArilleryIcon.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.ArilleryIcon.name);
						
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.EnergyDrainIcon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.EnergyDrainIcon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.EnergyDrainIcon.name))
					{
						
						AddInSirusPrefs(Campaign.Prototypes.EnergyDrainIcon.name);
						
						Campaign.Prototypes.EnergyDrainIcon.SetSprite(Campaign.Prototypes.EnergyDrainIcon.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.EnergyDrainIcon.name);
						
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.EMPicon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.EMPicon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.EMPicon.name))
					{	
						
						AddInSirusPrefs(Campaign.Prototypes.EMPicon.name);
						
						Campaign.Prototypes.EMPicon.SetSprite(Campaign.Prototypes.EMPicon.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.EMPicon.name);
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.WarBannerIcon && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.WarBannerIcon.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.WarBannerIcon.name))
					{
						
						AddInSirusPrefs(Campaign.Prototypes.WarBannerIcon.name);
						
						Campaign.Prototypes.WarBannerIcon.SetSprite(Campaign.Prototypes.WarBannerIcon.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.WarBannerIcon.name);
						
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.Timeshift && picked && SiriusPrefs.B["Store"+Campaign.Prototypes.Timeshift.name])
				{
					if(selectedSpells.Count<3 && !selectedSpells.Contains(Campaign.Prototypes.Timeshift.name))
					{

						AddInSirusPrefs(Campaign.Prototypes.Timeshift.name);
						
						Campaign.Prototypes.Timeshift.SetSprite(Campaign.Prototypes.Timeshift.name+"Clicked");
						selectedSpells.Add(Campaign.Prototypes.Timeshift.name);
						
					}
				}
				

			}
			
			if(selectedSpells.Count>0)
			{
				SiriusPrefs.B["isSpellsSelected"]=true;
			}
		}
	}
	
	public void DeselectCheck()
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
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.OrbitalStrikeIcon.name))
				{
					
					if(selectedSpells.Contains(Campaign.Prototypes.OrbitalStrikeIcon.name))
					{
						
						Campaign.Prototypes.OrbitalStrikeIcon.SetSprite(Campaign.Prototypes.OrbitalStrikeIcon.name);
						selectedSpells.Remove(Campaign.Prototypes.OrbitalStrikeIcon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.OrbitalStrikeIcon.name);
						
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.AirDropIcon.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.AirDropIcon.name))
					{
						
						Campaign.Prototypes.AirDropIcon.SetSprite(Campaign.Prototypes.AirDropIcon.name);	
						
						selectedSpells.Remove(Campaign.Prototypes.AirDropIcon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.AirDropIcon.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.AirStrike.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.AirStrike.name))
					{
						Campaign.Prototypes.AirStrike.SetSprite(Campaign.Prototypes.AirStrike.name);
						selectedSpells.Remove(Campaign.Prototypes.AirStrike.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.AirStrike.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.ArilleryIcon.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.ArilleryIcon.name))
					{	
						Campaign.Prototypes.ArilleryIcon.SetSprite(Campaign.Prototypes.ArilleryIcon.name);
						selectedSpells.Remove(Campaign.Prototypes.ArilleryIcon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.ArilleryIcon.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.EnergyDrainIcon.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.EnergyDrainIcon.name))
					{
						Campaign.Prototypes.EnergyDrainIcon.SetSprite(Campaign.Prototypes.EnergyDrainIcon.name);
						selectedSpells.Remove(Campaign.Prototypes.EnergyDrainIcon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.EnergyDrainIcon.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.EMPicon.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.EMPicon.name))
					{	
						Campaign.Prototypes.EMPicon.SetSprite(Campaign.Prototypes.EMPicon.name);
						selectedSpells.Remove(Campaign.Prototypes.EMPicon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.EMPicon.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.WarBannerIcon.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.WarBannerIcon.name))
					{
						Campaign.Prototypes.WarBannerIcon.SetSprite(Campaign.Prototypes.WarBannerIcon.name);
						selectedSpells.Remove(Campaign.Prototypes.WarBannerIcon.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.WarBannerIcon.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}
				if (hit.transform.gameObject == Campaign.Prototypes.SpellsHolder.FindChild(Campaign.Prototypes.Timeshift.name))
				{
					if(selectedSpells.Contains(Campaign.Prototypes.Timeshift.name))
					{
						Campaign.Prototypes.Timeshift.SetSprite(Campaign.Prototypes.Timeshift.name);
						selectedSpells.Remove(Campaign.Prototypes.Timeshift.name);
						RemoveFromSirusPrefs(Campaign.Prototypes.Timeshift.name);
						
						picked = false;
						SiriusPrefs.B["isSpellsPositionTaken"]=true;
					}
				}

			}
		}
	}
	
	public void RemoveFromSirusPrefs(string t)
	{
		for(int i =0 ; i<3 ; i++)
		{
			if(SiriusPrefs.S["Spell"+i.ToString()]==t)
			{
				SpellPositioning.DePosition(t);
				SiriusPrefs.S["Spell"+i.ToString()]="empty";
				return;
			}
		}
	}
	public void AddInSirusPrefs(string t)
	{
		for(int i =0 ; i<3 ; i++)
		{
			if(SiriusPrefs.S["Spell"+i.ToString()]=="empty")
			{
				SiriusPrefs.S["Spell"+i.ToString()]=t;
				SiriusPrefs.I[t]=i;
				SpellPositioning.Position(t);
				return;
			}
		}
	}
	
	public void RemoveFromList(string name)
	{
		for(int i = 0 ; i<SpellPositioning.positionedSpells.Count ; i++)
		{
			if(SpellPositioning.positionedSpells[i].name==name)
			{
				Destroy(SpellPositioning.positionedSpells[i]);
				SpellPositioning.positionedSpells.Remove(SpellPositioning.positionedSpells[i]);
			}
		}
	}
	
}
